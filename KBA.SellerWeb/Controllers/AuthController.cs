using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Auth.Dto;
using Microsoft.AspNetCore.Mvc;

namespace RepresentativePanel.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await ApiService.PostData<Result<TokenResultDto>>(configuration["GlobalSettings:ApiUrl"], "Auth/Login", loginDto);

            if (result.ResultCode == result.ResultCode && result.Value != null)
            {

                Response.Cookies.Append("AuthToken", result.Value.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return Json(new
                {
                    resultCode = result.ResultCode,
                    resultMessage = result.ResultMessage,
                    token = result.Value.Token
                });
            }
            else
            {
                return Json(new
                {
                    resultCode = result.ResultCode,
                    resultMessage = result.ResultMessage
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetVerificationCode([FromBody] GetverificationCodeDto dto)
        {
            var result = await ApiService.PostData<Result<string>>(
                configuration["GlobalSettings:ApiUrl"],
                "Auth/GetVerificationCode",
                dto
            );

            if (result.ResultCode == result.ResultCode)
            {
                return Json(new
                {
                    resultCode = result.ResultCode,
                    resultMessage = result.ResultMessage,
                    Result = result

                });
            }
            else
            {
                ViewBag.ErrorMessage = result.ResultMessage;
                return View("ForgotPassword");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var result = await ApiService.PostData<Result<string>>(
                configuration["GlobalSettings:ApiUrl"],
                "Auth/ChangePassword",
                dto
            );

            if (result.ResultCode == result.ResultCode)
            {
                TempData["Message"] = result.ResultMessage;
                return Json(new
                {
                    resultCode = result.ResultCode,
                    resultMessage = result.ResultMessage,

                });
            }
            else
            {
                ViewBag.ErrorMessage = result.ResultMessage;
                return View("EnterVerificationCode");
            }
        }
        [HttpPost]
        public async Task<IActionResult> LogOutAsync()
        {

            if (!Request.Cookies.ContainsKey("AuthToken"))
            {
                return Json(new
                {
                    resultCode = -401,
                    resultMessage = "User is not logged in"
                });
            }

            var authToken = Request.Cookies["AuthToken"];
            var result = await ApiService.GetData<Result<string>>(configuration["GlobalSettings:ApiUrl"], "Auth/LogOut", authToken);

            if (result.ResultCode == result.ResultCode)
            {

                Response.Cookies.Delete("AuthToken");
                return Json(new
                {
                    resultCode = result.ResultCode,
                    resultMessage = result.ResultMessage
                });
            }

            return Json(new
            {
                resultCode = result.ResultCode,
                resultMessage = result.ResultMessage ?? "Logout failed"
            });
        }
        [HttpPost]
        public async Task<IActionResult> ChangeAdmin()
        {
            var authToken = Request.Cookies["AuthToken"];

            var result = await ApiService.GetData<Result<bool>>(
                configuration["GlobalSettings:ApiUrl"],
                "Auth/ChangeRoleToAdminKP",
                authToken
            );

            if (result.ResultCode == 200)
            {
                TempData["Message"] = "نقش کاربر با موفقیت به ادمین تغییر یافت.";
            }
            else
            {
                TempData["Message"] = "خطا در تغییر نقش کاربر.";
            }

            return Json(new
            {
                resultCode = result.ResultCode,
                resultMessage = result.ResultMessage,
            });
        }


    }
}
