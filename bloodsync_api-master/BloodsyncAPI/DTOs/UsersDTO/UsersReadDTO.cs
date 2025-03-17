using BloodsyncAPI.DTOs.DonorsDTO;
using BloodsyncAPI.DTOs.HospitalsDTOs;
using BloodsyncAPI.DTOs.UserTypesDTO;

namespace BloodsyncAPI.DTOs.UsersDTO
{
    public class UsersReadDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public Guid DonorId { get; set; }
        public UserTypeReadDTO UserType { get; set; }
        public HospitalShortReadDTO Hospital { get; set; }
        public DonorReadDTO Donor { get; set; }
        public String IsVerified { get; set; }
    }
}