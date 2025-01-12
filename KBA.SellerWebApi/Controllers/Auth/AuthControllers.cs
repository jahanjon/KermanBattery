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
            if (result.ResultCode == result.ResultCode)
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
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Result<bool>> ChangeRoleToAdminKP()
        {
            var getUserId = GetSellerUserId();
            if (getUserId == 0)
            {
                return Result<bool>.Failure(ResultInfo.SellerNotFound);
            }
            var result = await authDataService.ChangeRoleToAdminKP(getUserId);
            return Result<bool>.Success(ResultInfo.OperationSuccess);

        }

        //[HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> ChangeRoleToUserKP(string nn)
        //{
        //    var x = await _db.Users.FirstOrDefaultAsync(x => x.NationalNumber == nn);
        //    x.Role = Domain.Role.Admin;
        //    await _db.SaveChangesAsync();
        //    return Ok("KP... Why are you...? mista..");
    }
}


