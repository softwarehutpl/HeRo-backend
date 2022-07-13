using Data.Entities;

namespace Data.Repositories
{
    public class CandidateRepository : BaseRepository<Candidate>
    {
        public CandidateRepository(DataContext context) : base(context)
        {

        }
    }
}
