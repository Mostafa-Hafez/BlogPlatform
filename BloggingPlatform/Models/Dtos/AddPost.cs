namespace BloggingPlatform.Models.Dtos
{
    public class AddPost
    {
        public required int PosterId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime Creation_Date { get; set; }
    }
}
