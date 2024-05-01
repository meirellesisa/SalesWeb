using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService; 
        }
        public IActionResult Index()
        {
            var listSeller = _sellerService.FindAll();
            return View(listSeller);
        }
    }
}
