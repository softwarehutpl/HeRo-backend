using Common.ServiceRegistrationAttributes;
using Data.DTOs.User;
using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    [ScopedRegistrationWithInterface]
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private DataContext _dataContext;

        public UserRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public User? GetUserByEmail(string mail)
        {
            var result = _dataContext.Users.Where(x => x.Email == mail).FirstOrDefault();
            return result;
        }

        public IQueryable<User> GetAllUsers()
        {
            var result = _dataContext.Users;
            return result;
        }

        public string GetUserEmail(int id)
        {
            var result = _dataContext.Users.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();

            return result;
        }

        public string? GetUserPassword(string email)
        {
            var result = _dataContext.Users.Where(x => x.Email == email).Select(x => x.Password).FirstOrDefault();
            return result;
        }

        public string GetUserRoleByEmail(string email)
        {
            var result = _dataContext.Users.Where(x => x.Email == email).Select(x => x.RoleName).FirstOrDefault();
            return result;
        }

        public Guid? GetUserGuidByEmail(string email)
        {
            var result = _dataContext.Users.Where(x => x.Email == email).Select(x => x.PasswordRecoveryGuid).FirstOrDefault();
            return result;
        }

        public bool CheckIfUserExist(string email)
        {
            var result = _dataContext.Users.Any(x => x.Email == email);
            return result;
        }

        public void ChangeUserPasswordByEmail(string email, string password)
        {
            var user = GetUserByEmail(email);
            user.Password = password;

            _dataContext.Users.Update(user);
            _dataContext.SaveChanges();
        }

        public EmailServiceDTO? GetUserEmailServiceCredentials(int id)
        {
            EmailServiceDTO? result = DataContext.Users.Where(x => x.Id == id).Include(x => x.SmtpServers).Select(x => new EmailServiceDTO
            {
                FullName = x.FullName,
                userSmtpData = x.SmtpServers.Where(x => x.UserId == id).FirstOrDefault()
            }).FirstOrDefault();

            return result;
        }

        public void SetUserMailBoxData(EmailServiceDTO dto)
        {
            SmtpServer smtp = dto.userSmtpData;
            DataContext.SmtpServers.Add(smtp);
            DataContext.SaveChanges();
        }
    }
}