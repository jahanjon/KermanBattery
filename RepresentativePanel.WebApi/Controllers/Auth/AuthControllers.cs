using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepresentativePanel.Application.Contract.Auth;
using RepresentativePanel.Application.Contract.Auth.Dto;
using RepresentativePanel.Domain.Core;
using System.Security.Claims;

namespace RepresentativePanel.WebApi.Controllers.Auth
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthDataService authDataService;
        private readonly IConfiguration configuration;
        private readonly ISellerLoginService sellerLoginService;

        public AuthController(IAuthDataService authDataService, IConfiguration configuration, ISellerLoginService sellerLoginService)
        {
            this.authDataService = authDataService;
            this.configuration = configuration;
            this.sellerLoginService = sellerLoginService;
        }

        [HttpPost]
        public async Task<Result<TokenResultDto>> Login([FromBody] LoginDto loginDto)
        {
            var ipAddress = loginDto.IPAddress;
            var jwtKey = configuration["TokenKey"];
            var result = await authDataService.Login(loginDto, jwtKey);
            if (result.ResultCode == 200)
            {
                await sellerLoginService.RecordLoginAsync(loginDto.PhoneNumber, ipAddress);
            }
            return result;
        }
        [HttpPost]
        public async Task<Result<string>> GetVerificationCode([FromBody] GetverificationCodeDto dto)
        {
            var result = await authDataService.GetVerificationCode(dto);
            return result;
        }

        [HttpPost]
        public async Task<Result<string>> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var result = await authDataService.ChangePassword(dto);
            return result;
        }
        private Result<string> GetSellerPhoneNumber()
        {
            var claims = User.Claims.ToList();
            var phoneNumber = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            return Result<string>.Success(200, "Successfully fetched the phone number", phoneNumber);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Result<string>> LogOut()
        {

            var phoneNumber = GetSellerPhoneNumber()?.Value;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return Result<string>.Failure(-400, "Phone number not found");
            }

            await sellerLoginService.RecordLogoutAsync(phoneNumber);
            return Result<string>.Success(200, "Logout successful");
        }

    }

}
