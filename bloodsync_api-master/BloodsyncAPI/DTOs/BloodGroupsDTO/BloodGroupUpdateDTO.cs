using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.DTOs.BloodGroupsDTO
{
    public class BloodGroupUpdateDTO
    {
        public Guid BloodGroupId { get; set; }
        public string BloodGroupName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? DateModified { get; set; }
    }
}
