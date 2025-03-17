using BloodsyncAPI.DTOs.BloodGroupsDTO;
using BloodsyncAPI.DTOs.HospitalsDTO;

namespace BloodsyncAPI.DTOs.InventorysDTO
{
    public class InventoryReadDTO
    {
        public Guid InventoryId { get; set; }
        public string InventoryName { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid BloodGroupId { get; set; }
        public Guid HospitalId { get; set; }
        public BloodGroupReadDTO? BloodGroup { get; set;}
        public HospitalReadDTO? Hospital { get; set; }
    }
}
