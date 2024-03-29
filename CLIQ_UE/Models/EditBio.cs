using System.ComponentModel.DataAnnotations;

namespace CLIQ_UE.Models
{
    // [Table("Edit Bio")]
    public class EditBio
    {


        public string FirstName { get; set; }


        public string LastName { get; set; }
        [Key]
        public string USerName { get; set; }
        public string PhoneNum { get; set; }

        public string Location { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool PublicBirthDate { get; set; }
        public String Bio { get; set; }

        public String? ImagePath { get; set; }

    }
}
