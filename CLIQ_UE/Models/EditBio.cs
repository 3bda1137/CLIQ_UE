using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLIQ_UE.Models
{
   // [Table("Edit Bio")]
    public class EditBio
    {
        //[MaxLength(25, ErrorMessage = "Length must be Less than 25")]
        //[MinLength(3, ErrorMessage = "Length must be More than 3")]

        public string FirstName { get; set; }
        //[MaxLength(25, ErrorMessage = "Length must be Less than 25")]
        //[MinLength(3, ErrorMessage = "Length must be More than 3")]

        public string LastName { get; set; }
        [Key]
        public string USerName { get; set; }
        //[DataType(DataType.PhoneNumber)]
        public string PhoneNum { get; set; }

        public string Location { get; set; }
        //[DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        public bool PublicBirthDate { get; set; }
        public String Bio { get; set; }
        [NotMapped]
        public IFormFile ImagePath { get; set; }

    }
}
