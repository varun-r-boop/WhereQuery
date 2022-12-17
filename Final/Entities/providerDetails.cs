using System.ComponentModel.DataAnnotations;

namespace Final.Entities
{
    public class providerDetails
    {
        [Key]
        public  Guid providerId { get; set; }
        public string? providerName { get; set; }
        public string? providerEmail { get; set; }
        public string? providerMobile { get; set; }
        public string? providerPassword  { get; set; }

        public providerServices[] providerService { get; set; }
        public string? location { get; set; }
        public string? description { get; set; }

    }
}
