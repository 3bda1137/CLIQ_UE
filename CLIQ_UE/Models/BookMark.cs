using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLIQ_UE.Models
{
    public class BookMark
    {
        public String Id { get; set; }
        [ForeignKey("Post")]
        public String PostID { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public String UserID { get; set; }
        public ICollection<Profile> profiles { get; set; }

    }
}
