namespace BloodsyncAPI.DTOs.HospitalsDTOs
{
    public class HospitalShortReadDTO
    { 
        public Guid HospitalId { get; set; }
        public string HospitalName { get; set; }

        public string HospitalAddress { get; set; }
    }
}
