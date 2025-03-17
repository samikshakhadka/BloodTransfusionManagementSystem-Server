namespace BloodsyncAPI.DTOs.UsersDTO
{
    public class PasswordResetDTO
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
    }
}

