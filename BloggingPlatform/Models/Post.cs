
namespace BloggingPlatform.Models
{
    public class Post
    {
        [Key]
        public int P_Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Creation_Date { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public  List<Comment> Comments { get; set; }
    }
}
