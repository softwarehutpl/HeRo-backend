using Data.Entities;

namespace Data.Repositories
{
<<<<<<< HEAD
    public class RecruitmentRepository 
=======
    public class RecruitmentRepository
>>>>>>> 68a34c719ccdfc66b5fddd9cd28b6a7058bd9de6
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
