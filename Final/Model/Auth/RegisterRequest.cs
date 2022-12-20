using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Final.Model.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string? firstName { get; set; }

        [Required]
        public string? lastName { get; set; }

        [Required]
        public string? userName { get; set; }

        [Required]
        public string? email { get; set; }
        [Required]
        public string? UserRole { get; set; }

        [Required]
        public string? password { get; set; }


        // [JsonIgnore]
        //public string passwordHash { get; set; }
    }
}
