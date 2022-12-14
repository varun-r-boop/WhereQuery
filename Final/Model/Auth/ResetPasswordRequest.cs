using System.ComponentModel.DataAnnotations;

namespace Final.Model.Auth
{
    public class ResetPasswordRequest
    {
        public string? Otp{ get; set; }
        [Required]
        public string? password { get; set; }
        [Required]
        [Compare("password")]
        public string?  confirmPassword{ get; set; }
    }
}
