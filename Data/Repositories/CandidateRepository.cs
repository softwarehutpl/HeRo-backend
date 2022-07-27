using Common.ServiceRegistrationAttributes;
using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    [ScopedRegistrationWithInterface]
    public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(DataContext context) : base(context)
        {
        }

        public Candidate GetCandidate(int id)
        {
            Candidate? candidate = DataContext.Candidates.Where(c => c.Id == id)
                .Include(c => c.Tech)
                .Include(c => c.Recruiter)
                .FirstOrDefault();

            return candidate;
        }
    }
}