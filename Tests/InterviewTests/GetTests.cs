using Data.DTOs.Interview;
using Data.Entities;
using Moq;

namespace Tests.InterviewTests
{
    public class GetTests : BaseTest
    {
        [Fact]
        public void Get_ShoudWork()
        {
            DateTime interviewDate = new DateTime(2022, 07, 20);
            Interview interview = Interviews[0];

            //Arrange
            InterviewDTO interviewDTOExpected = new InterviewDTO
            {
                InterviewId = interview.Id,
                Date = interview.Date,
                CandidateId = interview.CandidateId,
                CandidateName = interview.Candidate.Name,
                CandidateLastName = interview.Candidate.LastName,
                CandidateEmail = interview.Candidate.Email,
                WorkerId = interview.WorkerId,
                WorkerEmail = interview.User.Email,
                Type = interview.Type
            };

            // Act
            InterviewRepository.Setup(i => i.GetInterview(1)).Returns(interview);
            InterviewDTO interviewDTOActual = InterviewService.Get(1);

            //Assert
            Assert.True(interviewDTOExpected.Equals(interviewDTOActual));
        }

        [Fact]
        public void Get_ShoudFail()
        {
            Interview interview = null;
            InterviewRepository.Setup(i => i.GetInterview(2)).Returns(interview);
            InterviewDTO interviewDTOActual = InterviewService.Get(2);

            //Assert
            Assert.True(interviewDTOActual == null);
        }
    }
}