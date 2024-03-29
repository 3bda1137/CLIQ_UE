using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CLIQ_UE.Controllers
{
	public class EditController : Controller
	{
		private readonly IEditRepository editRepository;
		private readonly IWebHostEnvironment webHostEnvironment;
		public EditController(IEditRepository _editRepository, IWebHostEnvironment _webHostEnvironment)
		{
			editRepository = _editRepository;
			webHostEnvironment = _webHostEnvironment;
		}
		public IActionResult EditBio()
		{
			return View("EditBio");
		}
		//[HttpPost]
		//public IActionResult New(EditBio bio)
		//{
		//    EditBioAndUploadImageViewModel BioVm = new EditBioAndUploadImageViewModel();
		//    if (ModelState.IsValid == true)
		//    {

		//        BioVm.FirstName = bio.FirstName;
		//        BioVm.LastName = bio.LastName;
		//        BioVm.UserName = bio.USerName;
		//        BioVm.PhoneNum = bio.PhoneNum;
		//        BioVm.Location = bio.Location;
		//        BioVm.Bio = bio.Bio;
		//        BioVm.DateOfBirth = bio.DateOfBirth;
		//        BioVm.PublicBirthDate = bio.PublicBirthDate;
		//        BioVm.Image = bio.ImagePath;
		//        if (BioVm.Image != null && BioVm.Image.Length > 0)
		//        {
		//            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "");
		//            string uniqueFileName = Path.GetRandomFileName() + "_" + BioVm.Image.FileName;
		//            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
		//            using (var fileStream = new FileStream(filePath, FileMode.Create))
		//            {
		//                BioVm.Image.CopyTo(fileStream);
		//            }
		//        }
		//        editRepository.insert(bio);
		//        editRepository.Save(bio);
		//        return RedirectToAction("Index", "Home");
		//    }
		//    return RedirectToAction("Index", "Home");
		//}
		[HttpPost]
		public IActionResult EditBio(EditBioAndUploadImageViewModel BioVM)
		{
			if (ModelState.IsValid == true)
			{
				var editBio = new EditBio()
				{
					FirstName = BioVM.FirstName,
					LastName = BioVM.LastName,
					Bio = BioVM.Bio,
					USerName = BioVM.UserName,
					PhoneNum = BioVM.PhoneNum,
					Location = BioVM.Location,
					DateOfBirth = BioVM.DateOfBirth,
					PublicBirthDate = BioVM.PublicBirthDate,
				};

				if (BioVM.Image != null && BioVM.Image.Length > 0)
				{
					string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "");
					string uniqueFileName = Path.GetRandomFileName() + "_" + BioVM.Image.FileName;
					string filePath = Path.Combine(uploadsFolder, uniqueFileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						BioVM.Image.CopyTo(fileStream);
					}
				}
				editRepository.insert(editBio);
				editRepository.Save(editBio);
				return RedirectToAction("Index", "Home");
			}
			return View("EditBio", BioVM);
		}
	}
}