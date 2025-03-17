using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.Models
{
    public class PatientWaitlist
    {
        [Key]
        public Guid PatientId { get; set; }
        public string? PatientName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid HospitalId { get; set; }
        public Guid BloodGroupId { get; set; }
        public string? Remarks { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Municipality { get; set; }
        public int WardNo { get; set; }
        public string PhoneNumber { get; set; }
        public BloodGroup? BloodGroup { get; set; }
        public Hospital? Hospital { get; set; }

    }
}
