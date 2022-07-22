using Data.DTOs.Interview;
using Data.Entities;

namespace Tests.InterviewTests
{
    public class UpdateTests : BaseTest
    {
        [Fact]
        public void Update_ShoudWork()
        {
            Interview interview = Interviews[0];

            InterviewEditDTO interviewDTOExpected = new InterviewEditDTO
            (
                interview.Id,
                interview.Date,
                interview.WorkerId,
                interview.Type
            );

            //Arrange
            bool expectedResult = true;

            // Act
            InterviewRepository.Setup(i => i.GetById(1)).Returns(interview);

            bool actualResult = InterviewService.Update(interviewDTOExpected, 1, out errorMessage);

            //Assert
            Assert.True(expectedResult == actualResult);
        }

        [Fact]
        public void Update_ShoudFail()
        {
            Interview interview = Interviews[0];
            Interview interview2 = null;

            InterviewEditDTO interviewDTOExpected = new InterviewEditDTO
            (
                interview.Id,
                interview.Date,
                interview.WorkerId,
                interview.Type
            );

            //Arrange
            bool expectedResult = false;

            // Act
            InterviewRepository.Setup(i => i.GetById(1)).Returns(interview2);

            bool actualResult = InterviewService.Update(interviewDTOExpected, 1, out errorMessage);

            //Assert
            Assert.True(expectedResult == actualResult);
        }

        [Fact]
        public void Update_ShoudException()
        {
            Interview interview = Interviews[0];

            InterviewEditDTO interviewDTOExpected = new InterviewEditDTO
            (
                interview.Id,
                interview.Date,
                interview.WorkerId,
                interview.Type
            );

            Exception ex = new Exception();

            //Arrange
            bool expectedResult = false;

            // Act
            InterviewRepository.Setup(i => i.GetById(1)).Returns(interview);
            InterviewRepository.Setup(i => i.UpdateAndSaveChanges(interview)).Throws(ex);

            bool actualResult = InterviewService.Update(interviewDTOExpected, 1, out errorMessage);

            //Assert
            Assert.True(expectedResult == actualResult);
        }
    }
}