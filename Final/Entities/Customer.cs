using Final.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Customer
{
    [Key]
    [Required]
    public Guid Id  { get; set; }
    [Required]
    public string firstName { get; set; }
    [Required]
    public string lastName { get; set; }
    [Required]
    public string email { get; set; }
    [Required]
    public string userName { get; set; }

   // [JsonIgnore]
   // public string passwordHash { get; set; }

    //public orderDetails customerOrders { get; set; }
}
