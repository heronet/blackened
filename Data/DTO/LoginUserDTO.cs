using System.ComponentModel.DataAnnotations;

namespace Data.DTO
{
    public class LoginUserDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}