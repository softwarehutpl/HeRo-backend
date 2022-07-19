using Data.Entities;
using Data.IRepositories;

namespace Data.RepositoriesMockup
{
    public class InterviewRepositoryMockup : IInterviewRepository
    {
        public Interview Add(Interview entity)
        {
            throw new NotImplementedException();
        }

        public Interview AddAndSaveChanges(Interview entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Interview> entity)
        {
            throw new NotImplementedException();
        }

        public void AddRangeAndSaveChanges(IEnumerable<Interview> entity)
        {
            throw new NotImplementedException();
        }

        public void Attach(Interview entity)
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

        public IQueryable<Interview> GetAll()
        {
            throw new NotImplementedException();
        }

        public Interview GetById(int id)
        {
            Interview interview = new Interview
            {
                Id = id,
                Date = DateTime.Now,
                CandidateId = 1,
                WorkerId = 1,
                Type = "HR"
            };

            return interview;
        }

        public Interview GetInterview(int id)
        {
            if (id == 3)
            {
                Candidate candidate = new Candidate
                {
                    Id = 1,
                    Name = "John",
                    LastName = "Teslaw",
                    Status = "NEW",
                    PhoneNumber = "555-33-22",
                    Email = "JohnT@gmail.com",
                    ApplicationDate = new DateTime(2022, 7, 10),
                };

                User user = new User
                {
                    Id = 1,
                    Email = "Worker@gmail.com"
                };

                Interview interview = new Interview
                {
                    Id = id,
                    Date = new DateTime(2022, 7, 20),
                    CandidateId = 1,
                    WorkerId = 1,
                    Type = "HR",
                    Candidate = candidate,
                    User = user
                };

                return interview;
            }

            return null;
        }

        public void Remove(Interview entity)
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

        public void RemoveRange(IEnumerable<Interview> entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRangeAndSaveChanges(IEnumerable<Interview> entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Interview entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateAndSaveChanges(Interview entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRangeAndSaveChanges(IEnumerable<Interview> entities)
        {
            throw new NotImplementedException();
        }
    }
}