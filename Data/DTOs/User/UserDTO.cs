namespace Data.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserStatus { get; set; }
        public string RoleName { get; set; }

        public UserDTO(int id, string fullName, string email, string userStatus, string roleName)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            UserStatus = userStatus;
            RoleName = roleName;
        }
    }
}