using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Interview;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Services.Services
{
    [ScopedRegistration]
    public class InterviewService
    {
        private InterviewRepository _interviewRepository;
        private UserRepository _userRepository;
        private CandidateRepository _candidateRepository;
        private ILogger<InterviewService> _logger;

        public InterviewService(ILogger<InterviewService> logger, InterviewRepository interviewRepository, UserRepository userRepository, CandidateRepository candidateRepository)
        {
            _interviewRepository = interviewRepository;
            _userRepository = userRepository;
            _candidateRepository = candidateRepository;
            _logger = logger;
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

        public int GetQuantity()
        {
            int result = _interviewRepository.GetAll().Count();

            return result;
        }

        public InterviewListing GetInterviews(Paging paging, SortOrder sortOrder, InterviewFiltringDTO interviewFiltringDTO)
        {
            IQueryable<Interview> interviews = _interviewRepository.GetAll();
            interviews = interviews.Where(i => !i.DeletedDate.HasValue);

            if (interviewFiltringDTO.FromDate.HasValue)
            {
                if (interviewFiltringDTO.ToDate.HasValue)
                {
                    interviews = interviews.Where(i => (i.Date >= interviewFiltringDTO.FromDate && i.Date <= interviewFiltringDTO.ToDate));
                }
                else
                {
                    interviews = interviews.Where(i => i.Date >= interviewFiltringDTO.FromDate);
                }
            }
            else if (interviewFiltringDTO.ToDate.HasValue)
            {
                interviews = interviews.Where(i => i.Date <= interviewFiltringDTO.ToDate);
            }

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

            interviews = Sorter<Interview>.Sort(interviews, sortOrder.Sort);

            InterviewListing interviewListing = new InterviewListing();
            interviewListing.TotalCount = interviews.Count();
            interviewListing.InterviewFiltringDTO = interviewFiltringDTO;
            interviewListing.Paging = paging;
            interviewListing.SortOrder = sortOrder;
            interviewListing.InterviewDTOs = interviews.Select(i => new InterviewDTO(
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

            return interviewListing;
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
            try
            {
                _interviewRepository.AddAndSaveChanges(interview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); ;
            }
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

            try
            {
                _interviewRepository.UpdateAndSaveChanges(interview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }

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
            try
            {
                _interviewRepository.UpdateAndSaveChanges(interview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }
            return interview.Id;
        }
    }
}