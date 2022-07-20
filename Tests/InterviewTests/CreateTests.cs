using Data.DTOs.Interview;
using Data.Entities;
using Moq;

namespace Tests.InterviewTests
{
    public class CreateTests : BaseTest
    {
        [Fact]
        public void Create_ShoudWork()
        {
            Interview interview = Interviews[0];

            InterviewCreateDTO interviewDTOExpected = new InterviewCreateDTO
            (
                interview.Date,
                interview.CandidateId,
                interview.WorkerId,
                interview.Type
            );

            //Arrange
            int expectedResult = 1;

            // Act
            InterviewRepository.Setup(i => i.AddAndSaveChanges(It.IsAny<Interview>())).Returns(interview);
            int actualResult = InterviewService.Create(interviewDTOExpected, 1);

            //Assert
            Assert.True(expectedResult == actualResult);
        }

        [Fact]
        public void Create_ShoudException()
        {
            Interview interview = Interviews[0];

            InterviewCreateDTO interviewDTOExpected = new InterviewCreateDTO
            (
                interview.Date,
                interview.CandidateId,
                interview.WorkerId,
                interview.Type
            );

            Exception ex = new Exception();

            //Arrange
            int expectedResult = -1;

            // Act
            InterviewRepository.Setup(i => i.AddAndSaveChanges(It.IsAny<Interview>())).Throws(ex);

            int actualResult = InterviewService.Create(interviewDTOExpected, 1);

            //Assert

            Assert.True(expectedResult == actualResult);
        }
    }
}