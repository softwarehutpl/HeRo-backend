using System.Linq;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data.Repositories
{
    public class UserRepository
    {
        private DataContext _dataContext;

        public UserRepository(DataContext context)
        {
            _dataContext = context;
        }
        public void ChangeUserPassword(string email, string password)
        {
            User user = GetUserByEmail(email);
            user.Password = password;
            _dataContext.Update(user);
            _dataContext.SaveChanges();

        }
        public User GetUserByEmail(string mail)
        {
            var result = _dataContext.Users.Where(x => x.Email == mail).FirstOrDefault();
            return result;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var result = _dataContext.Users.ToList();
            return result;
        }

        public void AddUser(User user) 
        {
            _dataContext.Add(user);
            _dataContext.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            _dataContext.Update(user);
            _dataContext.SaveChanges();
        }

        public void RemoveUser(int id) { }

        public string GetUserEmail(int id)
        {
            var result = _dataContext.Users.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();
            return result;
        }

        public string GetUserPassword(string email)
        {
            var result = _dataContext.Users.Where(x => x.Email == email).Select(x => x.Password).FirstOrDefault();
            return result;
        }

        public string GetUserRoleByEmail(string email)
        {
            var result = _dataContext.Users.Where(x => x.Email == email).Select(x => x.RoleName).FirstOrDefault();
            return result;
        }

        public Guid GetUserGuidByEmail(string email)
        {
            var result = _dataContext.Users.Where(x => x.Email == email).Select(x => x.PasswordRecoveryGuid).FirstOrDefault();
            return result;
        }

        public bool CheckIfUserExist(string email)
        {
            var result = _dataContext.Users.Any(x => x.Email == email);
            return result;
        }

        public void ChangeUserPasswordByEmail(string email,string password)
        {
            var user = GetUserByEmail(email);
            user.Password = password;
            
            _dataContext.Users.Update(user);
            _dataContext.SaveChanges();

        }
    }
}
