using Microsoft.AspNetCore.Mvc;

namespace CLIQ_UE.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
