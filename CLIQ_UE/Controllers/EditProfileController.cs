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
                //edit ApplicationUser
                editUserServices.CompleteProfile(completeProfileViewModel, userId);
                return RedirectToAction("Index", "HomePage");
            }
            return View("EditBio", completeProfileViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassowrdViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ApplicationUser applicationUser = await userManager.FindByIdAsync(userId);

                if (applicationUser != null)
                {
                    bool isPasswordCorrect = await userManager.CheckPasswordAsync(applicationUser, changePasswordViewModel.CurrentPassword);
                    //current password is correct
                    if (isPasswordCorrect)
                    {
                        //current password and new password are differant
                        if (changePasswordViewModel.CurrentPassword != changePasswordViewModel.NewPassword)
                        {
                            var result = await userManager.ChangePasswordAsync(applicationUser, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);

                            //Change Password successfully
                            if (result.Succeeded)
                            {
                                return Json(new { success = true, code = "succeeded", message = "Password changed successfully" });
                            }
                            //error when change password
                            else
                            {
                                return Json(new { success = false, code = "failed", message = "Failed to change password" });
                            }
                        }
                        //current password = new password
                        else
                        {
                            return Json(new { success = false, code = 0, message = "Current Password and New Password are equal" });

                        }
                    }
                    //Current password is incorrect
                    else
                    {
                        return Json(new { success = false,code = -1, message = "Current password is incorrect" });
                    }
                }
            }

            // If ModelState is not valid or user not found, return JSON response
            return Json(new { success = false, message = "Invalid ModelState or user not found" });
        }

        public async Task<IActionResult> GetData()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser applicationUser = await userManager.FindByIdAsync(userId);

           ChangeInfoOfUserViewModel change= new ChangeInfoOfUserViewModel();
            change.BirthDate = applicationUser.BirthDate;
            change.Gender = applicationUser.Gender;
            change.LastName = applicationUser.LastName;
            change.FirstName = applicationUser.FirstName;
            change.Country= applicationUser.Country;
            change.Language = applicationUser.Language;
            return Json(change);
        }
        public async Task<IActionResult> UpDataOfUser(ChangeInfoOfUserViewModel changeInfoOfUserViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            editUserServices.UpdateProfile(changeInfoOfUserViewModel, userId);
            return Json(new { success = true, code = "succeeded", message = "Data changed successfully" });
        }

        public async Task<IActionResult> ChangePersonalImage(IFormFile image, string defaultImageSrc)
        {
            
            if (image != null || defaultImageSrc != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ApplicationUser applicationUser = await userManager.FindByIdAsync(userId);
                if (defaultImageSrc != null)
                {
                    applicationUser.PersonalImage = defaultImageSrc;
                }
                else
                {
                    if (image != null && image.Length > 0)
                    {
                        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                        // Path to save the image
                        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                        var filePath = Path.Combine(uploads, uniqueFileName);

                        // Save the uploaded image
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            image.CopyTo(fileStream);
                        }
                        // Set the new filename in the bio object
                        applicationUser.PersonalImage = "/images/"+ uniqueFileName;
                    }
                }
                editUserServices.UpdateUser(applicationUser);

                return Json(new { success = true, code = "succeeded", message = "personal image changed successfully" });
            }
            return Json(new { success = false, code = "failed", message = "select Image" });
        }
        public async Task<IActionResult> ChangeCoverImage(IFormFile image, string defaultImageSrc)
        {
            
            if (image != null || defaultImageSrc != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ApplicationUser applicationUser = await userManager.FindByIdAsync(userId);
                if (defaultImageSrc != null)
                {
                    applicationUser.ProfileImage = defaultImageSrc;
                }
                else
                {
                    if (image != null && image.Length > 0)
                    {
                        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                        // Path to save the image
                        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                        var filePath = Path.Combine(uploads, uniqueFileName);

                        // Save the uploaded image
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            image.CopyTo(fileStream);
                        }
                        // Set the new filename in the bio object
                        applicationUser.ProfileImage = "/images/"+ uniqueFileName;
                    }
                }
                editUserServices.UpdateUser(applicationUser);

                return Json(new { success = true, code = "succeeded", message = "Cover image changed successfully" });
            }
            return Json(new { success = false, code = "failed", message = "select Image" });
        }
    }
}
