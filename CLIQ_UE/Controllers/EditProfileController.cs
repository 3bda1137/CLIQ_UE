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
        private readonly IUserServices userServices;
        private readonly IEditUserServices editUserServices;

        public EditProfileController(IEditUserServices editUserServices, UserManager<ApplicationUser> userManager, IUserServices userServices)
        {
            this.editUserServices = editUserServices;
            this.userManager = userManager;
            this.userServices = userServices;
        }

        public IActionResult EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            EditProfileViewModel viewModel = editUserServices.GetUserViewModelById(userId);
            return View("EditProfile", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel userViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = editUserServices.GetById(userId);
            if (ModelState.IsValid)
            {
                bool result = await userManager.CheckPasswordAsync(user, userViewModel.CurrentPassword);
                if (result)
                {
                    editUserServices.UpdateProfile(userViewModel, userId);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("CurrentPassword", "Current Password Not Valid");
            }
            return View("EditProfile", userViewModel);
        }


        public async Task<IActionResult> CompleteProfile()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser applicationUser = await userManager.FindByIdAsync(userId);
            CompleteProfileViewModel completeProfileViewModel = userServices.MapAppUserToViewModel(applicationUser);
            return View("CompleteProfile", completeProfileViewModel);
        }
        [HttpPost]
        public IActionResult CompleteProfile(CompleteProfileViewModel completeProfileViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid == true)
            {
                if (completeProfileViewModel.Image != null && completeProfileViewModel.Image.Length > 0)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(completeProfileViewModel.Image.FileName);

                    // Path to save the image
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                    var filePath = Path.Combine(uploads, uniqueFileName);

                    // Save the uploaded image
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        completeProfileViewModel.Image.CopyTo(fileStream);
                    }

                    // Set the new filename in the bio object
                    completeProfileViewModel.ProfileImageName = uniqueFileName;
                }

                //edit ApplicationUser
                editUserServices.UpdateBio(completeProfileViewModel, userId);
                return RedirectToAction("Index", "HomePage");
            }
            return View("EditBio", completeProfileViewModel);
        }
    }
}
