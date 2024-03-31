using System.ComponentModel.DataAnnotations;

namespace CLIQ_UE.ViewModels
{
    public class CreatePostViewModel
    {



        [Required(ErrorMessage = "Post text is required")]
        [MaxLength(500, ErrorMessage = "Post text cannot exceed 500 characters")]
        public string postContent { get; set; }
        public string privacyValue { set; get; }
        public IFormFile? PostImage { get; set; }
        public string? PostDate { set; get; }



    }
}
