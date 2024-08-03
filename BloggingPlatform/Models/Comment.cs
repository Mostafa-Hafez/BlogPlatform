

namespace BloggingPlatform.Models
{
    public class Comment
    {
        [Key]
        public int com_Id { get; set; }
        [Required]
        public string com_text { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Com_time { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public  Post post { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public  User User { get; set; }
    }
}
