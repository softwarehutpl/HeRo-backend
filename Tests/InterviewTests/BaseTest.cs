using Data.RepositoriesMockup;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Services;

namespace Tests.InterviewTests
{
    public class BaseTest
    {
        public InterviewService InterviewService { get; set; }
        public Mock<InterviewRepositoryMockup> InterviewRepository { get; set; }
        public ILogger<InterviewService> Logger { get; set; }

        public BaseTest()
        {
            InterviewRepository = new Mock<InterviewRepositoryMockup>();
            InterviewService = new InterviewService(Logger, InterviewRepository.Object);
        }
    }
}