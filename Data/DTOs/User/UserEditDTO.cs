namespace Data.DTOs.User
{
    public class UserEditDTO
    {
        public int Id { get; set; }

        public string UserStatus { get; set; }

        public string RoleName { get; set; }

        public UserEditDTO(int id, string userStatus, string roleName)
        {
            Id = id;
            UserStatus = userStatus;
            RoleName = roleName;
        }
    }
}
