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

        public EditProfileController(IEditUserServices editUserServices, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.editUserServices = editUserServices;
            this.userManager = userManager;
            this.signInManager = signInManager;
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


        public IActionResult EditBio()
        {
            return View("EditBio");
        }
        [HttpPost]
        public IActionResult EditBio(EditBioAndUploadImageViewModel BioVM)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid == true)
            {
                if (BioVM.Image != null && BioVM.Image.Length > 0)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(BioVM.Image.FileName);

                    // Path to save the image
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                    var filePath = Path.Combine(uploads, uniqueFileName);

                    // Save the uploaded image
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        BioVM.Image.CopyTo(fileStream);
                    }

                    // Set the new filename in the bio object
                    BioVM.ProfileImageName = uniqueFileName;
                }

                //edit ApplicationUser
                editUserServices.UpdateBio(BioVM, userId);
                return RedirectToAction("Index", "Home");
            }
            return View("EditBio", BioVM);
        }
    }
}
