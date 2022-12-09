using System.ComponentModel.DataAnnotations;

namespace Final.Entities
{
    public class providerServices
    {
        [Key]
       public Guid id { get; set; }

       public providerDetails providerDetails { get; set; }

       public serviceList serviceList { get; set; }

    }
}
