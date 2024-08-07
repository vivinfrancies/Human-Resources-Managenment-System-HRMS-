using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    public class HolidayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
