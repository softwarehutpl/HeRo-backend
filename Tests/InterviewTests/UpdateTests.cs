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
            int expectedResult = 1;

            // Act
            IQueryable<Interview> interviews = Interviews.AsQueryable();
            InterviewRepository.Setup(i => i.GetById(1)).Returns(interview);

            int actualResult = InterviewService.Update(interviewDTOExpected, 1);

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
            int expectedResult = 0;

            // Act
            IQueryable<Interview> interviews = Interviews.AsQueryable();
            InterviewRepository.Setup(i => i.GetById(1)).Returns(interview2);

            int actualResult = InterviewService.Update(interviewDTOExpected, 1);

            //Assert
            Assert.True(expectedResult == actualResult);
        }
    }
}