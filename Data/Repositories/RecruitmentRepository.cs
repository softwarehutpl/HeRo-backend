using Data.Entities;

namespace Data.Repositories
{


    public class RecruitmentRepository
    {
        private DataContext _dataContext;

        public RecruitmentRepository(DataContext context)
        {
            _dataContext = context;
        }

        public void GetRecruitmentById(int id) { }

        public IEnumerable<Recruitment> GetAllRecruitments()
        {
            var result = _dataContext.Recruitments;

            return result;
        }

        public void AddRecruitment(int id) { }

        public void RemoveRecruitment(int id) { }
    }
}
