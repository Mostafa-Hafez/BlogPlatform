

namespace BloggingPlatform.Models
{
    public class Follower
    {
        
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        [NotMapped]
        public virtual User user { get; set; }
        public int? FollowerID { get; set; }
        [ForeignKey("FollowerID")]
        [NotMapped]
        public  User Followers { get; set; }
    }
}
