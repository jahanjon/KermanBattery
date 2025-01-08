using KermanBattery.Farmework.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepresentativePanel.Application.Contract.Seller;
using RepresentativePanel.Application.Dto;
using RepresentativePanel.Domain.Core;
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
            var phoneNumber = GetSellerPhoneNumber().Value;
            var result = await sellerService.GetSellerData(phoneNumber);

            if (result == null)
            {
                return Result<DashboardDto>.Failure(ResultInfo.SellerPhoneNumberNotFound);
            }

            return Result<DashboardDto>.Success(ResultInfo.OperationSuccess, result.Value);
        }

        private Result<string> GetSellerPhoneNumber()
        {
            var claims = User.Claims.ToList();
            var phoneNumber = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            return Result<string>.Success(ResultInfo.OperationSuccess, phoneNumber);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Result<DashboardDto>> PersonalInformation()
        {
            var phoneNumber = GetSellerPhoneNumber().Value;
            var result = await sellerService.UpdateProfileAsync(phoneNumber);

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

            var phoneNumber = GetSellerPhoneNumber().Value;
            dashboardDto.PhoneNumber = phoneNumber;
            var result = await sellerService.UpdateAndInsertProfile(dashboardDto);

            return result;
        }


    }
}
