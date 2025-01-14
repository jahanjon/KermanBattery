using KBA.Farmework.Core;
using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Product.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace KBA.SellerWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration configuration;

        public ProductController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> BatteryList()
        {
            var authToken = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(authToken))
            {
                return RedirectToAction("Index", "Home");
            }


            var result = await ApiService.GetData<Result<List<BatteryViewModel>>>(
                configuration["GlobalSettings:ApiUrl"],
                "Product/GetListBattery",
                authToken
            );

            if (result.Value != null)
            {
                return View(result.Value);
            }
            else
            {
                ViewBag.ErrorMessage = result.ResultMessage;
                return View("Error");
            }
        }

    }
}
