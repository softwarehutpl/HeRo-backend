using Data.IRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Services;

namespace Tests.InterviewTests
{
    public class BaseTest
    {
        public InterviewService InterviewService { get; set; }
        public Mock<IInterviewRepository> InterviewRepository { get; set; }
        public ILogger<InterviewService> Logger { get; set; }

        public BaseTest()
        {
            InterviewRepository = new Mock<IInterviewRepository>();
            InterviewService = new InterviewService(Logger, InterviewRepository.Object);
        }
    }
}