using System.ComponentModel.DataAnnotations;

namespace Final.Model.Auth
{
    public class AuthenticateRequest
    {
        [Required]
        public string? email { get; set; }

        [Required]
        public string? password { get; set; }
    }
}
