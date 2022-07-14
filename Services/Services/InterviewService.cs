using Common.Listing;
using Data.DTOs.Interview;
using Data.Entities;
using Data.Repositories;
using PagedList;
using Data.DTOs.Interview;
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

            var candidateTemp = _candidateRepository.GetAll().Where(x => x.Id == interview.CandidateId).Select(x => new { x.Name, x.LastName, x.Email }).First();
            string workerEmail = _userRepository.GetUserEmail(interview.WorkerId);

            InterviewDTO interviewDTO =
                new InterviewDTO(
                    interview.Id,
                    interview.Date,
                    interview.CandidateId,
                    candidateTemp.Name,
                    candidateTemp.LastName,
                    candidateTemp.Email,
                    interview.WorkerId,
                    workerEmail,
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
                if (sort.Key.ToLower() == "workerId")
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

            var resutl = interviews.Select(i => new InterviewDTO(
                                                    i.Id,
                                                    i.Date,
                                                    i.CandidateId,
                                                    i.Candidate.Name,
                                                    i.Candidate.LastName,
                                                    i.Candidate.Email,
                                                    i.WorkerId,
                                                    i.User.Email,
                                                    i.Type
                                                )).ToPagedList(paging.PageNumber, paging.PageSize);

            return resutl;
        }

        public void Create(InterviewCreateDTO interviewCreate, int userCreatedId)
        {
            Interview interview = new Interview();

            interview.Date = interviewCreate.Date;
            interview.CandidateId = interviewCreate.CandidateId;
            interview.WorkerId = interviewCreate.WorkerId;
            interview.Type = interviewCreate.Type;
            interview.CreatedById = userCreatedId;
            interview.CreatedDate = DateTime.UtcNow;

            _interviewRepository.AddAndSaveChanges(interview);
        }

        public int Update(InterviewEditDTO interviewEdit, int userEditId)
        {
            Interview interview = _interviewRepository.GetById(interviewEdit.InterviewId);
            if (interview == null)
            {
                return 0;
            }

            interview.Date = interviewEdit.Date;
            interview.WorkerId = interviewEdit.WorkerId;
            interview.Type = interviewEdit.Type;
            interview.LastUpdatedById = userEditId;
            interview.LastUpdatedDate = DateTime.UtcNow;

            _interviewRepository.UpdateAndSaveChanges(interview);

            return interview.Id;
        }

        public int Delete(int interviewId, int loginUserId)
        {
            Interview interview = _interviewRepository.GetById(interviewId);
            if (interview == null)
            {
                return 0;
            }

            interview.DeletedById = loginUserId;
            interview.DeletedDate = DateTime.UtcNow;

            _interviewRepository.UpdateAndSaveChanges(interview);

            return interview.Id;
        }
    }
}