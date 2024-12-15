using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "نام کاربری اجباری است.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رمز عبور اجباری است.")]
        [StringLength(20, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
