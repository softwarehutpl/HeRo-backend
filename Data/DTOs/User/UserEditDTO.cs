namespace Data.DTOs.User
{
    public class UserEditDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserStatus { get; set; }

        public string RoleName { get; set; }

        public UserEditDTO(int id, string name, string surname, string userStatus, string roleName)
        {
            Id = id;
            Name = name;
            Surname = surname;
            UserStatus = userStatus;
            RoleName = roleName;
        }
    }
}