using System.ComponentModel.DataAnnotations;

namespace CLIQ_UE.ViewModels
{
    public class CreatePostViewModel
    {



        [Required(ErrorMessage = "Post text is required")]
        [MaxLength(500, ErrorMessage = "Post text cannot exceed 500 characters")]
        public string postContent { get; set; }

        public string ProfilePictureUrl { get; set; }
        public string privacyValue { set; get; }


        public List<string>? PostImages { get; set; }
        public List<string>? Videos { get; set; }

    }
}
