using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLIQ_UE.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly IUserServices userServices;

		public AccountController(UserManager<ApplicationUser> _UserManager, SignInManager<ApplicationUser> _SignInManager, IUserServices _UserServices)
		{
			userManager = _UserManager;
			signInManager = _SignInManager;
			userServices = _UserServices;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser applicationUser = userServices.MapRegisterViewModelToAppUser(registerViewModel);
				IdentityResult result = await userManager.CreateAsync(applicationUser, registerViewModel.Password);
				if (result.Succeeded)
				{
					signInManager.SignInAsync(applicationUser, false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
			return View(registerViewModel);
		}

		public IActionResult Login()
		{

			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = await userManager.FindByEmailAsync(loginViewModel.Email);
				if (user != null)
				{
					bool isFound = await userManager.CheckPasswordAsync(user, loginViewModel.Password);
					if (isFound)
					{
						signInManager.SignInAsync(user, true);
						return RedirectToAction("Index", "Home");
					}
					ModelState.AddModelError("", "UserName or Password Not valid");
				}
			}
			return View(loginViewModel);
		}
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Login", "Account");
		}
	}
}
