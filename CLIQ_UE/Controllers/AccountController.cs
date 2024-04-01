using CLIQ_UE.Helpers;
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
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> _UserManager, SignInManager<ApplicationUser> _SignInManager, IUserServices _UserServices, IConfiguration _Configuration)
        {
            userManager = _UserManager;
            signInManager = _SignInManager;
            userServices = _UserServices;
            configuration = _Configuration;
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
                    await signInManager.SignInAsync(applicationUser, false);
                    return RedirectToAction("Index", "HomePage");
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
                        await signInManager.SignInAsync(user, true);
                        return RedirectToAction("Index", "HomePage");
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

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
                if (user != null)
                {
                    string token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var urlForResetPassword = Url.Action("ResetPassword", "Account", new
                    {
                        email = forgotPasswordViewModel.Email,
                        token = token
                    }, protocol: Request.Scheme);
                    string body = FormatEmail.CreateDesignOfEmail(urlForResetPassword);
                    SendEmail sendEmail = new SendEmail(configuration);
                    await sendEmail.SendEmailAsync(forgotPasswordViewModel.Email, body);

                    return RedirectToAction("ResetMessage", "Account");
                }

            }
            return View(forgotPasswordViewModel);
        }

        public IActionResult ResetMessage()
        {
            return View();
        }

        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"].ToString();
                string token = TempData["token"].ToString();

                ApplicationUser user = await userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, token, resetPasswordViewModel.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Not Valid");
                }

            }
            return View(resetPasswordViewModel);
        }
    }
}