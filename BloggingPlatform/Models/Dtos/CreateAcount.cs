namespace BloggingPlatform.Models.Dtos
{
    public class CreateAcount
    {
        [Display(Name = "User Name")]
        [Required]
        [MaxLength(50)]
        public string User_Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
