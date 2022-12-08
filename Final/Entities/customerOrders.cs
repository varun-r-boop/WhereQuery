using System.ComponentModel.DataAnnotations;

namespace Final.Entities
{
    public class customerOrders
    {
        [Key]
        public int ordersId { get; set; }

        public ICollection<customerDetails> customerDetails { get; set; }
        
    }
}
