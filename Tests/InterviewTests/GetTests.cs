using Data.DTOs.Interview;

namespace Tests.InterviewTests
{
    public class GetTests : BaseTest
    {
        [Fact]
        public void GetInterview_ShoudWork()
        {
            InterviewDTO interviewDTOActual = InterviewService.Get(3);

            //Assert
            Assert.True(interviewDTOActual != null);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void GetInterview_ShoudFail(int interviewId)
        {
            InterviewDTO interviewDTOActual = InterviewService.Get(interviewId);

            //Assert
            Assert.True(interviewDTOActual == null);
        }
    }
}