using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordServices _salesRecordService;

        public SalesRecordsController(SalesRecordServices saleRecordServices)
        {
            _salesRecordService = saleRecordServices;   
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? initial, DateTime? final)
        {
            if (!initial.HasValue)
            {
                //PRECISA MEXER NO ANO
                initial = new DateTime(2018, 1, 1);
            }
            if (!final.HasValue)
            {
                final = DateTime.Now;
            }
            ViewData["minDate"] = initial.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = final.Value.ToString("yyyy-MM-dd");

            var sales = await _salesRecordService.FindByDateAsync(initial, final);

            return View(sales);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? initial, DateTime? final)
        {
            if (!initial.HasValue)
            {
                //PRECISA MEXER NO ANO
                initial = new DateTime(2018, 1, 1);
            }
            if (!final.HasValue)
            {
                final = DateTime.Now;
            }
            ViewData["minDate"] = initial.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = final.Value.ToString("yyyy-MM-dd");

            var sales = await _salesRecordService.FindByDateGroupingAsync(initial, final);

            return View(sales);
        }
    }
}
