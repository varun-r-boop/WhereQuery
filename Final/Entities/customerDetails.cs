using Final.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class customerDetails
{
    [Key]
    [Required]
    public Guid customerId  { get; set; }
    [Required]
    public string customerName { get; set; }
    [Required]
    public string customerEmail { get; set; }
    [Required]
    [Column(TypeName = "date")]
    public DateTime customerDob { get; set; }
    [Required]
    public string customerPassword { get; set; }
    [Required]
    public string customerMobile { get; set; }
    [Required]
    public string customerAddress { get; set; }

    public orderDetails customerOrders { get; set; }
}
