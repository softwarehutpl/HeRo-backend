using Data.Entities;

namespace Data.Repositories
{
    public class InterviewRepository : BaseRepository<Interview>
    {
        private DataContext _dataContext;

        public InterviewRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public Interview GetInterviewById(int id)
        {
            var result = _dataContext.Interviews.Find(id);
            return result;
        }

        public IQueryable<Interview> GetAllInterviews()
        {
            var result = _dataContext.Interviews;
            return result;
        }
    }
}
