using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;



namespace CLIQ_UE.ViewModels
{
    public class EditProfileViewModelcs
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public DateTime? BirthDate { get; set; }

        public string? Language {  get; set; }
        public string? Gender { get; set; }
        [DataType(DataType.Password)]
        public string? currentPassword { get; set; }
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword")]
        public string? ConfirmPassword { get; set; }


    }
}
