using Data.Entities;
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
        public Mock<ILogger<InterviewService>> Logger { get; set; }
        public List<Interview> Interviews { get; set; }

        public BaseTest()
        {
            InterviewRepository = new Mock<IInterviewRepository>();
            Logger = new Mock<ILogger<InterviewService>>();

            InterviewService = new InterviewService(Logger.Object, InterviewRepository.Object);

            DateTime interviewDate = new DateTime(2022, 07, 20);

            Interviews = new List<Interview>()
            {
                new Interview
                {
                    Id = 1,
                    Date = interviewDate,
                    CandidateId = 1,
                    WorkerId = 1,
                    Type = "HR",
                    Candidate = new Candidate
                    {
                        Id = 1,
                        Name = "John",
                        LastName = "Teslaw",
                        Email = "JohnT@mail.com"
                    },
                    User = new User
                    {
                        Id = 1,
                        Email = "Worker@mail.com"
                    }
                },
                new Interview
                {
                    Id = 2,
                    Date = interviewDate.AddDays(5),
                    CandidateId = 2,
                    WorkerId = 2,
                    Type = "HR",
                    Candidate = new Candidate
                    {
                        Id = 2,
                        Name = "Mary",
                        LastName = "Mua",
                        Email = "MaryM@mail.com"
                    },
                    User = new User
                    {
                        Id = 2,
                        Email = "Worker2@mail.com"
                    }
                },
                new Interview
                {
                    Id = 3,
                    Date = interviewDate.AddDays(6),
                    CandidateId = 3,
                    WorkerId = 2,
                    Type = "HR",
                    Candidate = new Candidate
                    {
                        Id = 3,
                        Name = "Ed",
                        LastName = "Edd",
                        Email = "EddyM@mail.com"
                    },
                    User = new User
                    {
                        Id = 2,
                        Email = "Worker2@mail.com"
                    }
                },
                new Interview
                {
                    Id = 4,
                    Date = interviewDate.AddDays(-6),
                    CandidateId = 4,
                    WorkerId = 1,
                    Type = "HR",
                    Candidate = new Candidate
                    {
                        Id = 4,
                        Name = "Johnny",
                        LastName = "Bravo",
                        Email = "JohnnyB@mail.com"
                    },
                    User = new User
                    {
                        Id = 1,
                        Email = "Worker@mail.com"
                    }
                },
                new Interview
                {
                    Id = 5,
                    Date = interviewDate.AddDays(2),
                    CandidateId = 4,
                    WorkerId = 3,
                    Type = "Tech",
                    Candidate = new Candidate
                    {
                        Id = 4,
                        Name = "Johnny",
                        LastName = "Bravo",
                        Email = "JohnnyB@mail.com"
                    },
                    User = new User
                    {
                        Id = 3,
                        Email = "Tech@mail.com"
                    }
                },
            };
        }
    }
}