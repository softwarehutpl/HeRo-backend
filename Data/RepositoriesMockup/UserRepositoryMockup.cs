using Data.Entities;
using Data.IRepositories;

namespace Data.RepositoriesMockup
{
    internal class UserRepositoryMockup : IUserRepository
    {
        public User Add(User entity)
        {
            throw new NotImplementedException();
        }

        public User AddAndSaveChanges(User entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<User> entity)
        {
            throw new NotImplementedException();
        }

        public void AddRangeAndSaveChanges(IEnumerable<User> entity)
        {
            throw new NotImplementedException();
        }

        public void Attach(User entity)
        {
            throw new NotImplementedException();
        }

        public void ChangeUserPasswordByEmail(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfUserExist(string email)
        {
            throw new NotImplementedException();
        }

        public void DetectChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string mail)
        {
            throw new NotImplementedException();
        }

        public string GetUserEmail(int id)
        {
            throw new NotImplementedException();
        }

        public Guid GetUserGuidByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public string GetUserPassword(string email)
        {
            throw new NotImplementedException();
        }

        public string GetUserRoleByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveByIdAndSaveChanges(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<User> entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRangeAndSaveChanges(IEnumerable<User> entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateAndSaveChanges(User entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRangeAndSaveChanges(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}