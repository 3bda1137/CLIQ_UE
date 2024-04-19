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
                ApplicationUser ExistingUser = await userManager.FindByEmailAsync(registerViewModel.Email);
                if (ExistingUser == null)
                {
                    string code = GenerateRandomCode.GetCode();
                    TempData["code"] = code;
                    TempData["email"] = registerViewModel.Email;
                    TempData["password"] = registerViewModel.Password;
                    string body = FormatEmail.CreateDesignForConfirmEmail(code);
                    SendEmail sendEmail = new SendEmail(configuration);
                    await sendEmail.SendEmailAsync(registerViewModel.Email, body);
                    return RedirectToAction("ConfirmEmail", "Account", new { Email = registerViewModel.Email });



                }
                else
                {
                    ModelState.AddModelError(registerViewModel.Email, "Email already exist");
                }
            }
            return View(registerViewModel);
        }

        public IActionResult ConfirmEmail(string Email)
        {
            ViewBag.email = Email;
            return View("ConfirmEmail2");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmailAsync(ConfirmEmailViewModel confirmEmailViewModel)
        {
            if (ModelState.IsValid)
            {
                if (TempData["code"] != null)
                {
                    if (TempData["code"].ToString() == confirmEmailViewModel.Code)
                    {
                        RegisterViewModel registerViewModel = new RegisterViewModel();
                        registerViewModel.Password = TempData["password"].ToString();
                        registerViewModel.Email = TempData["email"].ToString();
                        ApplicationUser applicationUser = userServices.MapRegisterViewModelToAppUser(registerViewModel);
                        IdentityResult result = await userManager.CreateAsync(applicationUser, registerViewModel.Password);
                        if (result.Succeeded)
                        {
                            await signInManager.SignInAsync(applicationUser, false);
                            return Json(new { success = true });
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);

                            }
                        }
                    }
                    else
                    {
                        TempData.Keep("password");
                        TempData.Keep("email");
                        TempData.Keep("code");
                        ModelState.AddModelError("code", "code is not valid");
                    }
                }
            }
            //return View(confirmEmailViewModel);
            return Json(new { success = false, code = 0, message = "Code is incorrect" });
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
                    var urlForResetPassword = Url.Action("ResetPassword", "Account", null, protocol: Request.Scheme);
                    string body = FormatEmail.CreateDesignForForgotPassword(urlForResetPassword, forgotPasswordViewModel.Email, token);
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

        [HttpPost]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"].ToString();
                string token = TempData["token"].ToString();
                if (email == null || token == null)
                    return RedirectToAction("Login", "Account");


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