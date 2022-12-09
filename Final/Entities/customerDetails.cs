using Final.Entities;
using System;
using System.ComponentModel.DataAnnotations;

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
    public DateOnly customerDob { get; set; }
    [Required]
    public string customerPassword { get; set; }
    [Required]
    public string customerMobile { get; set; }
    [Required]
    public string customerAddress { get; set; }

    public orderDetails customerOrders { get; set; }
}
