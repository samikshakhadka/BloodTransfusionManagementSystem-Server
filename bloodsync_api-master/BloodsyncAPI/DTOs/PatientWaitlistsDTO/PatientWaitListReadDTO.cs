using BloodsyncAPI.DTOs.HospitalsDTO;
using BloodsyncAPI.DTOs.HospitalsDTOs;
using BloodsyncAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.DTOs.PatientWaitlistsDTO
{
    public class PatientWaitListReadDTO
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Municipality { get; set; }
        public int WardNo { get; set; }
        public string PhoneNumber { get; set; }
        public string? Remarks { get; set; }
        public BloodGroupsDTO.BloodGroupReadDTO? BloodGroup { get; set; }
        public HospitalShortReadDTO? Hospital { get; set; }
    }
}
