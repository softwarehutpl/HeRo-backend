namespace Data.Repositories
{
    public interface IRecruitmentRepository
    {
        void AddRecruitment(int id);
        void GetAllRecruitments();
        void GetRecruitmentById(int id);
        void RemoveRecruitment(int id);
    }
}