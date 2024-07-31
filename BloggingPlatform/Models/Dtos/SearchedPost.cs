namespace BloggingPlatform.Models.Dtos
{
    public class SearchedPost
    {
        public int PosterId { get; set; }
        public string PosterName { get; set; }
        public string PosterEmail { get; set; }
        public int PostId { get; set; }
        public  string Title { get; set; }
        public  string Content { get; set; }
        public DateTime Creation_Date { get; set; }
    }
}
