﻿namespace Services.DTOs.User
{
    public class UserEditDTO
    {
        public int? Id { get; set; }

        public string Email { get; set; }

        public string UserStatus { get; set; }

        public string RoleName { get; set; }

        public UserEditDTO(int? id, string email, string userStatus, string roleName)
        {
            Id = id;
            Email = email;
            UserStatus = userStatus;
            RoleName = roleName;
        }
    }
}
