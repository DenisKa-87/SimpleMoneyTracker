using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class SignInDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(16), MinLength(8)]
        public string Password { get; set; }
        [Required]
        [StringLength(16), MinLength(3)]
        public string Appeal { get; set; }



    }
}
