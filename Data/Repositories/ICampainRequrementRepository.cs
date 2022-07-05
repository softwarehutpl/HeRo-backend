namespace Data.Repositories
{
    public interface ICampainRequrementRepository
    {
        void AddCampainRequrement(int id);
        void GetAllCampainRequrements();
        void GetCampainRequrementId(int id);
        void RemoveCampainRequrement(int id);
    }
}