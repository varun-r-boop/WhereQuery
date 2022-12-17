using Final.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;

public class Customer
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string? firstName { get; set; }
    [Required]
    public string? lastName { get; set; }
    [Required]
    public string? email { get; set; }
    [Required]
    public string? userName { get; set; }

    public string ? CustomerMobileNumber { get; set; }

    public string? Address { get; set; }
    public Role userRole { get; set; }

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

}