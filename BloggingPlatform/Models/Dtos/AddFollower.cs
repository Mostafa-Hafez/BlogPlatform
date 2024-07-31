namespace BloggingPlatform.Models.Dtos
{
    public class AddFollower
    {
        [Display(Name = "User Name")]
        [Required]
        [MaxLength(50)]
        public string User_Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
      
        public int followerId { get; set; }
    }
}
