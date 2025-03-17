
namespace BloodsyncAPI.DTOs.UsersDTO
{
    public class UsersCreateDTO
    {
        
        public string Password { get; set; }
        public string Email { get; set; }
        
        public Guid UserTypeId { get; set; }
        public Guid HospitalId { get; set; }

    }
}
