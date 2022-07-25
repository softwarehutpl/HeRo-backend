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
            bool expectedResult = true;

            // Act
            InterviewRepository.Setup(i => i.AddAndSaveChanges(It.IsAny<Interview>())).Returns(interview);
            bool actualResult = InterviewService.Create(interviewDTOExpected, 1);

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
            bool expectedResult = false;

            // Act
            InterviewRepository.Setup(i => i.AddAndSaveChanges(It.IsAny<Interview>())).Throws(ex);

            bool actualResult = InterviewService.Create(interviewDTOExpected, 1);

            //Assert

            Assert.True(expectedResult == actualResult);
        }
    }
}