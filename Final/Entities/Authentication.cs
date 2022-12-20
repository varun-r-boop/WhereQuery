using System.ComponentModel.DataAnnotations;

namespace Final.Entities
{
    public class Authentication
    {
        [Key]
        public Guid AuthId { get; set; }

        public string UserName { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        public string UserRole { get; set; } = null!;
        //Password Reset

        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public bool isDeleted { get; set; }

        public string? passwordHash { get; set; }

        //public orderDetails customerOrders { get; set; }

        //OTP verification

        public string? OTP { get; set; }
        public DateTime? OTPExpiresAt { get; set; }

        //public byte[] rowVersion { get; set; }

        //Verification
        public DateTime? verifiedAt { get; set; }
        public DateTime? passwordResetAt { get; set; }
        public bool isVerified { get; set; }

        public virtual Customer? customer { get; set; }
        public virtual providerDetails? providerDetails { get; set; }
    }
}
