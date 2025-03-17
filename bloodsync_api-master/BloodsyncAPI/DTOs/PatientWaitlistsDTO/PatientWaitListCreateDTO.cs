using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.DTOs.PatientWaitlistsDTO
{
    public class PatientWaitListCreateDTO
    {

        public string PatientName { get; set; }
        public Guid BloodGroupId { get; set; }
        public Guid? HospitalId { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Municipality { get; set; }
        public string PhoneNumber { get; set; }
        public int WardNo { get; set; }
        public string? Remarks { get; set; }

    }
}
