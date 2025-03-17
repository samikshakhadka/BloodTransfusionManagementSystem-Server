namespace BloodsyncAPI.DTOs.UsersDTO
{
    public class PasswordChangeDTO
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
