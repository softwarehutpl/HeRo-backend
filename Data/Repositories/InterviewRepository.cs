using Common.ServiceRegistrationAttributes;
using Data.Entities;

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
                .Select(x => new Interview
                {
                    Id = x.Id,
                    Date = x.Date,
                    CandidateId = x.CandidateId,
                    WorkerId = x.WorkerId,
                    Type = x.Type,
                    Candidate = x.Candidate,
                    User = x.User
                })
                .FirstOrDefault();

            return interview;
        }
    }
}