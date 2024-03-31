using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CLIQ_UE.Controllers
{
    public class EditProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

           
        private readonly IEditUserServices editUserServices;

        public EditProfileController(IEditUserServices editUserServices , UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.editUserServices = editUserServices;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult EditProfile()
        {
            return View("EditProfile");
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModelcs userViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = editUserServices.GetById(userId);
            if (ModelState.IsValid)
            {

                user.UserName = userViewModel.UserName;
                user.PasswordHash = userViewModel.currentPassword;
                user.PasswordHash = userViewModel.NewPassword;
                user.PasswordHash = userViewModel.ConfirmPassword;
                user.BirthDate = userViewModel.BirthDate;
                user.Location = userViewModel.Country; 
                user.Gender = userViewModel.Gender;
                user.Language = userViewModel.Language;
                user.Email = userViewModel.Email;

                IdentityResult result =
                    await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View("EditProfile", userViewModel);
        }
        
    }
}
