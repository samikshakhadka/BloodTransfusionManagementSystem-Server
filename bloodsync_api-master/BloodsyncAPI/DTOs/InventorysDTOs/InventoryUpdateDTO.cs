namespace BloodsyncAPI.DTOs.InventorysDTO
{
    public class InventoryUpdateDTO
    {
        public Guid InventoryId { get; set; }
        public string InventoryName { get; set; }
        public int Quantity { get; set; }
        public Guid BloodGroupId { get; set; }
        public Guid HospitalId { get; set; }
    }
}
