using Common.Listing;
using Data.Entities;
using Data.Repositories;
using PagedList;
using Services.DTOs.Interview;

namespace Services.Services
{
    public class InterviewService
    {
        private InterviewRepository _interviewRepository;

        public InterviewService(InterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        public InterviewDTO Get(int interviewId)
        {
            Interview interview = _interviewRepository.GetInterviewById(interviewId);
            if (interview == null)
            {
                return null;
            }

            InterviewDTO interviewDTO = new InterviewDTO(interview.Date, interview.CandidateId, interview.UserId);

            return interviewDTO;
        }

        public IEnumerable<InterviewListingDTO> GetInterviews(Paging paging, SortOrder sortOrder, InterviewFiltringDTO interviewFiltringDTO)
        {
            IQueryable<Interview> interviews = _interviewRepository.GetAllInterviews();

            interviews = interviews.Where(i => (i.Date >= interviewFiltringDTO.FromDate && i.Date <= interviewFiltringDTO.ToDate));

            if (interviewFiltringDTO.CandidateId.HasValue)
            {
                interviews = interviews.Where(s => s.CandidateId == interviewFiltringDTO.CandidateId);
            }
            if (interviewFiltringDTO.UserId.HasValue)
            {
                interviews = interviews.Where(s => s.UserId == interviewFiltringDTO.UserId);
            }
            if (!String.IsNullOrEmpty(interviewFiltringDTO.Type))
            {
                interviews = interviews.Where(s => s.Type.Equals(interviewFiltringDTO.Type));
            }

            foreach (KeyValuePair<string, string> sort in sortOrder.Sort)
            {
                if (sort.Key.ToLower() == "date")
                {
                    if (sort.Value.ToUpper() == "DESC")
                    {
                        interviews = interviews.OrderByDescending(i => i.Date);
                    }
                    else
                    {
                        interviews = interviews.OrderBy(i => i.Date);
                    }
                }
                if (sort.Key.ToLower() == "candidateid")
                {
                    if (sort.Value.ToUpper() == "DESC")
                    {
                        interviews = interviews.OrderByDescending(i => i.CandidateId);
                    }
                    else
                    {
                        interviews = interviews.OrderBy(i => i.CandidateId);
                    }
                }
                if (sort.Key.ToLower() == "userid")
                {
                    if (sort.Value.ToUpper() == "DESC")
                    {
                        interviews = interviews.OrderByDescending(i => i.UserId);
                    }
                    else
                    {
                        interviews = interviews.OrderBy(i => i.UserId);
                    }
                }
                if (sort.Key.ToLower() == "type")
                {
                    if (sort.Value.ToUpper() == "DESC")
                    {
                        interviews = interviews.OrderByDescending(i => i.Type);
                    }
                    else
                    {
                        interviews = interviews.OrderBy(i => i.Type);
                    }
                }
            }

            var result = interviews
                .Select(x => new InterviewListingDTO(x.Date, x.CandidateId, x.UserId, x.Type))
                .ToPagedList(paging.PageNumber, paging.PageSize);

            return result;
        }

        public void Create(InterviewCreateDTO interviewCreate, int userCreatedId)
        {
            Interview interview = new Interview();

            interview.Date = interviewCreate.Date;
            interview.CandidateId = interviewCreate.CandidateId;
            interview.UserId = interviewCreate.UserId;
            interview.CreatedById = userCreatedId;
            interview.CreatedDate = DateTime.UtcNow;

            _interviewRepository.AddAndSaveChanges(interview);
        }
    }
}