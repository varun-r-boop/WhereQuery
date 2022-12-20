using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Entities
{
    public class providerDetails
    {
        [Key]
        public  Guid ProviderId { get; set; }
        public string? ProviderFirstName { get; set; }
        public string? ProviderLastName { get; set; }
        public string? ProviderEmail { get; set; }
        public string? ProviderMobile { get; set; }
        public string? ProviderPasswordHash  { get; set; }

       
        public string? Location { get; set; }
        public string? Description { get; set; }

        public providerServices[] ProviderService { get; set; }
        [ForeignKey("Provider")]
        public Guid ProviderUserID { get; set; }
        public virtual Authentication Provider { get; set; }

    }
}
