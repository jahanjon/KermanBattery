using KermanBattery.Farmework.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepresentativePanel.Domain.Core;
using RepresentativePanel.Domain.Enum;
using System.Security.Claims;

namespace KermanBatterySeller.WebApi.Controllers.Shared
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SharedController : ControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public Result<Roles> CheckUserRoleAccess(Roles role)
        {
            var claims = User.Claims.ToList();
            var currentRole = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role)).Value;

            var currentRoleId = (int)Enum.Parse(typeof(Roles), currentRole);
            if (role == Roles.Admin && currentRoleId == (int)Roles.Admin)
            {
                return Result<Roles>.Success(ResultInfo.OperationSuccess, role);
            }
            else if (role == Roles.Seller && currentRoleId == (int)Roles.Seller)
            {
                return Result<Roles>.Success(ResultInfo.OperationSuccess, role);
            }
            else
            {
                return Result<Roles>.Success(ResultInfo.OperationFailed, role);
            }
        }
    }
}
