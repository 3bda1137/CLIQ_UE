using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLIQ_UE.Models
{
    public class LikeCommentVM
    {
        [Required]
        public int CommentId { get; set; }
        [Required]
        public string? ApplicationUserId { get; set; }
    }
}
