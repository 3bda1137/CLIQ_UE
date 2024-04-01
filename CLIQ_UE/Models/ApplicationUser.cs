using Microsoft.AspNetCore.Identity;

namespace CLIQ_UE.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalImage { get; set; }
        public string? Location { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ProfileImage { get; set; }

        public string? Bio { get; set; }

        public bool? PublicBirthDate { get; set; }
        public string? Language { get; set; } // new
        public string? Gender { get; set; } // new
        public string? Country { get; set; } // new
    }
}
