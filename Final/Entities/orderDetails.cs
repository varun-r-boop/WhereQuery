using System.ComponentModel.DataAnnotations;

namespace Final.Entities
{
    public class orderDetails
    {
        [Key]
        public int ordersId { get; set; }
        public DateOnly appointmentDate { get; set; }
        public TimeOnly appointmentTime { get; set; }

        public ICollection<customerDetails> customerDetails { get; set; }
        public ICollection<providerDetails> providerDetails { get; set; }
        public orderStatus orderStatus { get; set; }
        
    }
}
