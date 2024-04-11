using System.ComponentModel.DataAnnotations.Schema;

namespace CLIQ_UE.Models
{
    public class UserLikePost
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public bool isLiked { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
