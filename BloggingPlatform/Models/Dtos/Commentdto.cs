    namespace BloggingPlatform.Models.Dtos
{
    public class Commentdto
    {
        public int com_id { get; set; }
        public int Post_id { get; set; }
        public int user_id { get; set; }

        public string commenterName { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
    }
}
