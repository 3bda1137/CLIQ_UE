using System.ComponentModel.DataAnnotations.Schema;

namespace CLIQ_UE.Models
{
    public class BookMark
    {
        public String Id { get; set; }
        [ForeignKey("Post")]
        public int PostID { get; set; }
        public ICollection<Post> Posts { get; set; }
        public String UserID { get; set; }
        public DateTime saveDate { get; set; }
    }
}
