using System.ComponentModel.DataAnnotations;

namespace Final.Model.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string email { get; set; }
    }
}
