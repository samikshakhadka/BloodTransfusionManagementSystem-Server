using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;


namespace BloodsyncAPI.Models
{
    public class Donor
    {
        [Key]
        public Guid DonorId { get; set; }
        public Guid BloodGroupId { get; set; }
        public Guid? HospitalId { get; set; }
        public DateTime? LastDonated { get; set; }
        public string? DonorName { get; set; }
        public string? FatherName { get; set; }
        public string Province {  get; set; }
        public string District { get; set; }
        public string Municipality { get; set; }
        public int WardNo { get; set; }
        public string? Location { get; set; }
        public string PhoneNumber { get; set; }
        public string EmergencyContact { get; set; }
        public DateTime? LastLocationUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime DateOfBirth { get; set; }
        //For getting names of Bloodgroup, Hospital ,etc.
        public BloodGroup? BloodGroup { get; set; }
        public Hospital? Hospital { get; set; }

    }
}
