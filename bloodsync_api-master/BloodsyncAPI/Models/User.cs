
namespace BloodsyncAPI.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string? IsVerified { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public DateTime  DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid HospitalId { get; set; }
        public Guid UserTypeId { get; set; }
        public Guid DonorId { get; set; }
        public UserType UserType { get; set; }  
        public Hospital Hospital { get; set; }
        public Donor Donor { get; set; }

    }
}
