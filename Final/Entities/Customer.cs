using Final.Entities;
using Microsoft.EntityFrameworkCore;
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
    [ForeignKey("customer")]
    public Guid CustomerUserID { get; set; }
    public virtual Authentication customer { get; set; }
    public virtual ProfilePicture? ProfilePicture { get; set; }


}