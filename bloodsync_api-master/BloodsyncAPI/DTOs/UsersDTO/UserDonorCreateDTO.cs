using NetTopologySuite.Geometries;

namespace BloodsyncAPI.DTOs.UsersDTO
{
    public class UserDonorCreateDTO
    {
        //For Donor
        public Guid BloodGroupId { get; set; }
        public Guid? HospitalId { get; set; }
        public DateTime? LastDonated { get; set; }
        public string DonorName { get; set; }
        public string FatherName { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Municipality { get; set; }
        public DateTime DateCreated { get; set; }
        public int WardNo { get; set; }
        public string? Location { get; set; }
        public string PhoneNumber { get; set; }
        public string EmergencyContact { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? LastLocationUpdated { get; set; }

        //For User
        public Guid DonorId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid UserTypeId { get; set; }
        public string IsVerified { get; set; }
        

    }
}
