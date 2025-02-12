﻿using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Auth.Dto;
using KBA.SellerApplication.Contract.Seller;
using Microsoft.AspNetCore.Mvc;


namespace RepresentativePanel.Web.Controllers
{
    public class SellerController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<SellerController> logger;

        public SellerController(IConfiguration configuration, ILogger<SellerController> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<IActionResult> Dashboard()
        {
            var authToken = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(authToken))
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await ApiService.GetData<Result<DashboardDto>>(
                configuration["GlobalSettings:ApiUrl"],
                "Seller/Dashboard",
                authToken
            );

            if (result.ResultCode == result.ResultCode && result.Value != null)
            {
                return View(result.Value);
            }
            else
            {
                ViewBag.ErrorMessage = result.ResultMessage;
                return View("Error");
            }
        }

        public async Task<IActionResult> PersonalInformation()
        {
            var authToken = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(authToken))
            {
                return Json(new
                {
                    resultCode = -401,
                    resultMessage = "توکن یافت نشد"
                });
            }

            var result = await ApiService.GetData<Result<DashboardDto>>(
                configuration["GlobalSettings:ApiUrl"],
                "Seller/PersonalInformation",
                authToken
            );

            return View(result.Value);
        }
        private int GetSellerUserId()
        {
            var claims = User.Claims.ToList();
            var userIdClaim = claims.FirstOrDefault(x => x.Type.Equals("UserId"))?.Value;

            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
        public async Task<IActionResult> UpdateAndInsertProfile(DashboardDto dashboardDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    resultCode = -401,
                    resultMessage = "BadRequest"
                });
            }

            var result = await ApiService.PostData<Result<bool>>(
                configuration["GlobalSettings:ApiUrl"],
                "Seller/UpdateAndInsertProfile",
                dashboardDto,
                Request.Cookies["AuthToken"]
            );

            if (result.Value==false)
            {
                return Json(new
                {
                    resultCode = result.ResultCode,
                    resultMessage = result.ResultMessage
                });
            }

            return Json(new
            {
                resultCode = result.ResultCode,
                resultMessage = result.ResultMessage,
            });
        }
        public async Task<IActionResult> UserActivity()
        {
            var authToken = Request.Cookies["AuthToken"];

            var result = await ApiService.GetData<Result<SellerLoginReportDto>>(
                            configuration["GlobalSettings:ApiUrl"],
                            "Seller/UserActivity",
                            authToken
            );

            if (result == null || result.ResultCode != 200)
            {
                TempData["Message"] = "خطا در بازیابی داده‌ها.";
                return View(new SellerLoginReportDto());
            }

            return View(result.Value);
        }

    }
}