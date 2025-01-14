using KBA.Farmework.Core;
using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Auth;
using KBA.SellerApplication.Contract.Product.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KBA.SellerWebApi.Controllers.Product
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ProductController( IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private int GetSellerUserId()
        {
            var claims = User.Claims.ToList();
            var userIdClaim = claims.FirstOrDefault(x => x.Type.Equals("UserId"))?.Value;

            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Result<List<BatteryViewModel>>> GetListBattery()
        {

            var sellerId = GetSellerUserId();
            if (sellerId == 0)
            {
                return Result<List<BatteryViewModel>>.Failure(ResultInfo.SellerNotFound);
            }


            var batteries = new List<BatteryViewModel>
            {
                  new BatteryViewModel { Id = 1, Name = "باتری A", Type = "لیتیوم-یون", Capacity = 3000, Status = "فعال" },
                  new BatteryViewModel { Id = 2, Name = "باتری B", Type = "نیکل-کادمیوم", Capacity = 2000, Status = "غیرفعال" }
            };

            return Result<List<BatteryViewModel>>.Success(ResultInfo.OperationSuccess, batteries);
        }

    }
}
