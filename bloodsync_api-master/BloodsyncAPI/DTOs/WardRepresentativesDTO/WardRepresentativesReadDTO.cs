using BloodsyncAPI.DTOs.HospitalsDTOs;

namespace BloodsyncAPI.DTOs.WardRepresentativesDTO
{
    public class WardRepresentativesReadDTO
    {
        public string WardRepId { get; set; }
        public string WardRepName { get; set; }
        public int WardNo { get; set; }
        public string PhoneNumber { get; set; }
        public string SecondaryContact { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid HospitalId { get; set; }
        public HospitalShortReadDTO Hospital {  get; set; }
    }
}
