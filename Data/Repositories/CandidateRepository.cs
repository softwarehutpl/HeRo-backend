using Common.ServiceRegistrationAttributes;
using Data.Entities;

namespace Data.Repositories
{
    [ScopedRegistration]
    public class CandidateRepository : BaseRepository<Candidate>
    {
        private DataContext _dataContext;

        public CandidateRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}