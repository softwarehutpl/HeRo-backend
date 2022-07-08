namespace Services.DTOs.User
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string UserStatus { get; set; }
        public string RoleName { get; set; }

        public UserDTO(string email, string userStatus, string roleName)
        {
            Email = email;
            UserStatus = userStatus;
            RoleName = roleName;
        }
    }
}
