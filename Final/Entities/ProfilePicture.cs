using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Entities
{
    public class ProfilePicture
    {
        [Key]
        public int UserProfilePictureId { get; set; }

       
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey("customerID")]
        public virtual Customer? customer { get; set; }

    }
}
