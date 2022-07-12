using Data.Entities;

namespace Data.Repositories
{
    public class CandidateRepository : BaseRepository<Candidate>
    {
        public CandidateRepository(DataContext context) : base(context)
        {

        }

        public Candidate? GetCandidateById(int id)
        {
            Candidate? result = GetById(id);
            return result;
        }


        public int AddCandidate(Candidate candidate) 
        {
            AddAndSaveChanges(candidate);
            return 1;
        }

        public int UpdateCandidate(Candidate candidate)
        {
            try
            {
                UpdateAndSaveChanges(candidate);
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public void RemoveCandidate(int id) 
        { 
            //tego chyba nawet nie będzie
        }
    }
}
