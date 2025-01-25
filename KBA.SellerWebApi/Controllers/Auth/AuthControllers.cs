using KBA.Farmework.Core;
using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Auth;
using KBA.SellerApplication.Contract.Auth.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RepresentativePanel.WebApi.Controllers.Auth
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthDataService authDataService;
        private readonly IConfiguration configuration;
        private readonly ISellerLoginReportService sellerLoginService;

        public AuthController(IAuthDataService authDataService, IConfiguration configuration, ISellerLoginReportService sellerLoginService)
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
            return result;
        }
        [HttpPost]
        public async Task<Result<string>> GetVerificationCode(GetverificationCodeDto dto)
        {
            var result = await authDataService.GetVerificationCode(dto);
            return result;
        }

        [HttpPost]
        public async Task<Result<string>> xdsds(ChangePasswordDto dto)
        {
            var result = await authDataService.ChangePassword(dto);
            return result;
        }
        private int GetSellerUserId()
        {
            var claims = User.Claims.ToList();
            var userIdClaim = claims.FirstOrDefault(x => x.Type.Equals("UserId"))?.Value;

            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Result<string>> LogOut()
        {

            var sellerId = GetSellerUserId();
            if (sellerId == 0)
            {
                return Result<string>.Failure(ResultInfo.SellerNotFound);
            }

            await sellerLoginService.RecordLogoutAsync(sellerId);
            return Result<string>.Success(ResultInfo.LogoutSuccess);
        }
    }
}


