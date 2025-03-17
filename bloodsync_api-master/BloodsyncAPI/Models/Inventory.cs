namespace BloodsyncAPI.Models
{
    public class Inventory
    {
        public Guid InventoryId { get; set; }
        public string? InventoryName { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid BloodGroupId { get; set; }
        public Guid HospitalId { get; set; }
        public BloodGroup? BloodGroup { get; set; }
        public Hospital? Hospital { get; set; }
    }
}