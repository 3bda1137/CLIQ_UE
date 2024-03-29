using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CLIQ_UE.Controllers
{
    public class EditController : Controller
    {
        private readonly IEditUserServices editUserServices;

        public EditController(IEditUserServices editUserServices)
        {
            this.editUserServices = editUserServices;

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
                editUserServices.Update(BioVM, userId);
                return RedirectToAction("Index", "Home");
            }
            return View("EditBio", BioVM);
        }
    }
}