using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.Models
{
    public class BloodGroup
    {
        [Key]
        public Guid BloodGroupId { get; set; }
        public string BloodGroupName { get; set; }
        public DateTime DateCreated { get; set; }   
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

    }
}
