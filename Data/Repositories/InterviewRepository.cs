using Common.ServiceRegistrationAttributes;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    [ScopedRegistration]
    public class InterviewRepository : BaseRepository<Interview>
    {
        private DataContext _dataContext;

        public InterviewRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public Interview GetInterview(int id)
        {
            Interview interview = _dataContext.Interviews.Where(i => i.Id == id)
                .Include(i => i.Candidate)
                .Include(i => i.User)
                .FirstOrDefault();

            return interview;
        }
    }
}