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
        private UserRepository _userRepository;
        private CandidateRepository _candidateRepository;

        public InterviewService(InterviewRepository interviewRepository, UserRepository userRepository, CandidateRepository candidateRepository)
        {
            _interviewRepository = interviewRepository;
            _userRepository = userRepository;
            _candidateRepository = candidateRepository;
        }

        public InterviewDTO Get(int interviewId)
        {
            Interview interview = _interviewRepository.GetById(interviewId);
            if (interview == null)
            {
                return null;
            }

            InterviewDTO interviewDTO =
                new InterviewDTO(
                    interview.Id,
                    interview.Date,
                    interview.CandidateId,
                    _candidateRepository.GetById(interview.CandidateId).Email,
                    interview.WorkerId,
                    _userRepository.GetById(interview.WorkerId).Email,
                    interview.Type);

            return interviewDTO;
        }

        public IEnumerable<InterviewDTO> GetInterviews(Paging paging, SortOrder sortOrder, InterviewFiltringDTO interviewFiltringDTO)
        {
            IQueryable<Interview> interviews = _interviewRepository.GetAll();

            interviews = interviews.Where(i => (i.Date >= interviewFiltringDTO.FromDate && i.Date <= interviewFiltringDTO.ToDate));

            if (interviewFiltringDTO.CandidateId.HasValue)
            {
                interviews = interviews.Where(s => s.CandidateId == interviewFiltringDTO.CandidateId);
            }
            if (interviewFiltringDTO.WorkerId.HasValue)
            {
                interviews = interviews.Where(s => s.WorkerId == interviewFiltringDTO.WorkerId);
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
                        interviews = interviews.OrderByDescending(i => i.WorkerId);
                    }
                    else
                    {
                        interviews = interviews.OrderBy(i => i.WorkerId);
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
                .Select(x => new InterviewDTO(
                    x.Id,
                    x.Date,
                    x.CandidateId,
                    _candidateRepository.GetById(x.CandidateId).Email,
                    x.WorkerId,
                    _userRepository.GetById(x.WorkerId).Email,
                    x.Type))
                .ToPagedList(paging.PageNumber, paging.PageSize);

            return result;
        }
    }
}