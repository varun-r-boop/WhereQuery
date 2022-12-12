using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Entities
{
    public class orderDetails
    {
        [Key]
        public int ordersId { get; set; }

        [Column(TypeName = "date")]
        public DateTime appointmentDate { get; set; }
      //  public DateAndTime appointmentTime { get; set; }

        public ICollection<Customer> customerDetails { get; set; }
        public ICollection<providerDetails> providerDetails { get; set; }
        public orderStatus orderStatus { get; set; }
        
    }
}
