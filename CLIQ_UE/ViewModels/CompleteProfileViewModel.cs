using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLIQ_UE.ViewModels
{
    public class CompleteProfileViewModel
    {
        [MaxLength(25, ErrorMessage = "Length must be less than 25")]
        [MinLength(3, ErrorMessage = "Length must be more than 3")]
        public string FirstName { get; set; }

        [MaxLength(25, ErrorMessage = "Length must be less than 25")]
        [MinLength(3, ErrorMessage = "Length must be more than 3")]
        public string LastName { get; set; }

        [Key]
        public string UserName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNum { get; set; }

        public string Location { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        public bool PublicBirthDate { get; set; }

        public string ProfileImageName { get; set; } = "default";

        public string Bio { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
