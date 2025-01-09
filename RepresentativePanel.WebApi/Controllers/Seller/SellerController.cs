﻿using KermanBattery.Farmework.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepresentativePanel.Application.Contract.Seller;
using RepresentativePanel.Application.Dto;
using RepresentativePanel.Domain.Core;
using RepresentativePanel.Domain.Entity.SellerAgg;
using System.Security.Claims;

namespace RepresentativePanel.WebApi.Controllers.Seller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService sellerService;

        public SellerController(ISellerService sellerService)
        {
            this.sellerService = sellerService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Result<DashboardDto>> Dashboard()
        {
            var sellerId = GetSellerUserId();
            if (sellerId == 0)
            {
                return Result<DashboardDto>.Failure(ResultInfo.SellerNotFound);
            }
            var result = await sellerService.GetSellerData(sellerId);

            if (result == null)
            {
                return Result<DashboardDto>.Failure(ResultInfo.OperationFailed);
            }

            return Result<DashboardDto>.Success(ResultInfo.OperationSuccess, result.Value);
        }


        #region GetUserInToken
        private int GetSellerUserId()
        {
            var claims = User.Claims.ToList();
            var userIdClaim = claims.FirstOrDefault(x => x.Type.Equals("UserId"))?.Value;

            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
        #endregion


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Result<DashboardDto>> PersonalInformation()
        {
            var sellerId = GetSellerUserId();
            if (sellerId == 0)
            {
                return Result<DashboardDto>.Failure(ResultInfo.SellerNotFound);
            }
            var result = await sellerService.UpdateProfileAsync(sellerId);

            if (result == null)
            {
                return Result<DashboardDto>.Failure(ResultInfo.OperationFailed);
            }

            return Result<DashboardDto>.Success(ResultInfo.OperationSuccess, result.Value);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Result<bool>> UpdateAndInsertProfile([FromBody] DashboardDto dashboardDto)
        {
            if (!ModelState.IsValid)
            {
                return Result<bool>.Failure(ResultInfo.NotFound);
            }

            var sellerId = GetSellerUserId();
            if (sellerId == 0)
            {
                return Result<bool>.Failure(ResultInfo.SellerNotFound);
            }
            dashboardDto.SellerId = sellerId;
            var result = await sellerService.UpdateAndInsertProfile(dashboardDto);

            return result;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Result<Seller>> UserActivity([FromBody] int userId)
        {
           
            var result = sellerService.GetSellerData(userId);

        }
    }
}
