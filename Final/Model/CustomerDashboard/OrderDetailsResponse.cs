using Final.Entities;

namespace Final.Model.CustomerDashboard
{
    public class OrderDetailsResponse
    {
        public string? OrderName { get; set; }
        public string? OrderDescription { get; set; }
        public int ? OrderPrice { get; set; }
        public orderStatus orderStatus { get; set; }
    }
}
