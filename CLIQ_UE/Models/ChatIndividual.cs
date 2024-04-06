namespace CLIQ_UE.Models
{
    public class ChatIndividual
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Time { get; set; }
        public string MessageContant { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsImage { get; set; }
        public bool IsReaded { get; set; }
    }
}
