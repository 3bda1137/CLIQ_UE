using System.ComponentModel.DataAnnotations.Schema;

namespace CLIQ_UE.Models
{
    public class UserLikeComment
    {
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
