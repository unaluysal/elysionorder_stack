using ElysionOrder.Application.Services.ReportServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ElysionOrder.UI.Controllers
{

    public class HomeController : Controller
    {

        private readonly IReportService _reportService;

        public HomeController(IReportService reportService)
        {
            _reportService= reportService;
        }
        [Authorize(Roles ="Home_View")]
        public  async Task<IActionResult> Index()
        {
            var report =await _reportService.TodayDashBoardAsync();
          
   

            return View(report);
        }

      public IActionResult Error()
        {

            return View();
        }
    }
}