namespace Data.Repositories
{
    public interface ICandidateRepository
    {
        void AddCandidate(int id);
        void GetAllCandidate();
        void GetCandidatetById(int id);
        void RemoveCandidate(int id);
    }
}