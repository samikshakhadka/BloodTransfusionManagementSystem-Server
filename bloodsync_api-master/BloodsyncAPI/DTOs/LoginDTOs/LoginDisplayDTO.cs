using BloodsyncAPI.DTOs.UserTypesDTO;


namespace BloodsyncAPI.DTOs.LoginDTOs

{
    public class LoginDisplayDTO
    {
        public Guid UserId { get; set; }
        public Guid HospitalId {  get; set; }
        public UserTypeReadDTO? UserType { get; set; }
    }
}
