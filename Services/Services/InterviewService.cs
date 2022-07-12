using Data.Entities;
using Data.Repositories;
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
    }
}
