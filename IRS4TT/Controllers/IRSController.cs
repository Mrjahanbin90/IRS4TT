using Microsoft.AspNetCore.Mvc;

namespace IRS4TT.Controllers
{
    public class IRSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult PrintClosing()
        {
            return View();
        }

        public IActionResult Assesment()
        {
            return View();
        }

        public IActionResult Notification()
        {
            return View();
        }
        public IActionResult Email()
        {
            return View();
        }
        public IActionResult insurance_offer_analysis()
        {
            return View();
        }
    }
}
