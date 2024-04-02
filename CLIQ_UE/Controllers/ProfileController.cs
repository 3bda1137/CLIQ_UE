using Microsoft.AspNetCore.Mvc;

namespace CLIQ_UE.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
