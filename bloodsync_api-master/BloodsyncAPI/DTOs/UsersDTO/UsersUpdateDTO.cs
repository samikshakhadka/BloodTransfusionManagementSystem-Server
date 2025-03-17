
namespace BloodsyncAPI.DTOs.UsersDTO
{
    public class UsersUpdateDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public Guid HospitalId { get; set; }
        
        public Guid UserTypeId { get; set; }
    }
} 
