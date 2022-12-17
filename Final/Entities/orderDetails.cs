using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Entities
{
    public class orderDetails
    {
        [Key]
        public Guid OrdersId { get; set; }
        public string? OrderName { get; set; }
        public string? OrderDescription { get; set; }
        public int? OrderPrice { get; set; }
        [Column(TypeName = "date")]
        public DateTime appointmentDate { get; set; }
      //  public DateAndTime appointmentTime { get; set; }
        public ICollection<providerDetails>? ProviderDetails { get; set; }
        public orderStatus orderStatus { get; set; }

        [ForeignKey("CustomerId")]
        public Guid Id { get; set; }
        public Customer customer { get; set; }  
    }
}
