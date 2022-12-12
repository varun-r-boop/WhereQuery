using System.ComponentModel.DataAnnotations;

namespace Final.Model.Customers
{
    public class AuthenticateRequest
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }
    }
}
