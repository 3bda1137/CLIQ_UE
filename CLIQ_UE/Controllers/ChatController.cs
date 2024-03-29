using Microsoft.AspNetCore.Mvc;

namespace CLIQ_UE.Controllers
{
	public class ChatController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
