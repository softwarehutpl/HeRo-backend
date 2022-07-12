using Data.Entities;

namespace Data.Repositories
{
    public class CandidateRepository : BaseRepository<Candidate>
    {
        public CandidateRepository(DataContext context) : base(context)
        {

        }

        public Candidate GetCandidateById(int id) 
        {
            Candidate result = GetById(id);
            return result;
        }

        

       
        public void RemoveCandidate(int id) { 
            //tego chyba nawet nie będzie
        }
    }
}
