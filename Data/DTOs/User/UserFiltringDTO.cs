namespace Data.DTOs.User
{
    public class UserFiltringDTO
    {
        public string Email { get; set; }
        public string UserStatus { get; set; }
        public string RoleName { get; set; }

        public UserFiltringDTO(string email, string userStatus, string roleName)
        {
            Email = email;
            UserStatus = userStatus;
            RoleName = roleName;
        }
    }
}