


namespace BloggingPlatform.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        [Display(Name ="User Name")]
        [Required]
        [MaxLength(50)]
        public string User_Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public  List<Follower> Followers { get; set; }
        public  List<Post> Posts { get; set; }

    }
}
