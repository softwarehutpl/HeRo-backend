using Common.ServiceRegistrationAttributes;
using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    [ScopedRegistrationWithInterface]
    public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
    {
        private DataContext _dataContext;

        public CandidateRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public Candidate GetCandidate(int id)
        {
            Candidate? candidate = _dataContext.Candidates.Where(c => c.Id == id)
                .Include(c => c.Tech)
                .Include(c => c.Recruiter)
                .FirstOrDefault();

            return candidate;
        }
    }
}