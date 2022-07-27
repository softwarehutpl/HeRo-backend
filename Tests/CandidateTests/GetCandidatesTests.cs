using Common.Listing;
using Data.DTOs.Candidate;
using Data.Entities;
using Data.IRepositories;
using Moq;
using Services.Listing;
using Services.Services;

namespace Tests.CandidateTests
{
    public class GetCandidatesTests : BaseCandidateTests
    {
        [Fact]
        public void GetCandidates_ShouldWork()
        {
            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new CandidateService(
                mapper,
                candRepoMock.Object,
                loggerMock.Object,
                recruitmentRepositoryMock.Object
            );

            Paging paging = new Paging
            {
                PageNumber = 1,
                PageSize = 10,
            };

            SortOrder? sortOrder = new SortOrder
            {
                Sort = new List<KeyValuePair<string, string>>
                {
                    //new KeyValuePair<string, string>("Id","Desc")
                }
            };

            List<Candidate> candidates = new();

            Candidate candidate = new Candidate
            {
                Id = 1,
                Name = "Name1",
                LastName = "LastName1",
                Email = "email@gmail.com",
                PhoneNumber = "123123123",
                RecruitmentId = 1,
                RecruiterId = 1,
                Status = "NEW",
                Stage = "EVALUATION",
                CvPath = "CvPath",
                Recruiter = new User()
                {
                    Id = 1,
                    Email = "email@email.com",
                    Password = "xxxxxx",
                    UserStatus = "ACTIVE",
                    RoleName = "RECRUITER",
                },
                Tech = new User()
                {
                    Id = 2,
                    Email = "email3@email.com",
                    Password = "xxxxxx",
                    UserStatus = "ACTIVE",
                    RoleName = "TECHNICIAN",
                },
                Recruitment = new Recruitment()
                {
                    Id = 3,
                    Name = "RecName1",
                    RecruiterId = 1
                }
            };
            candidates.Add(candidate);

            Candidate candidate2 = new Candidate
            {
                Id = 2,
                Name = "Name2",
                LastName = "LastName2",
                Email = "email2@gmail.com",
                PhoneNumber = "123123125",
                RecruitmentId = 2,
                RecruiterId = 2,
                Status = "NEW",
                Stage = "EVALUATION",
                CvPath = "CvPath",
                Recruiter = new User()
                {
                    Id = 4,
                    Email = "email2@email.com",
                    Password = "xxxxxx",
                    UserStatus = "ACTIVE",
                    RoleName = "RECRUITER",
                },
                Tech = new User()
                {
                    Id = 5,
                    Email = "email3@email.com",
                    Password = "xxxxxx",
                    UserStatus = "ACTIVE",
                    RoleName = "TECHNICIAN",
                },
                Recruitment = new Recruitment()
                {
                    Id = 6,
                    Name = "RecName1",
                    RecruiterId = 2
                }
            };
            candidates.Add(candidate2);

            //Arrange
            List<string> statuslist = new();
            statuslist.Add("NEW");

            List<string> stageslist = new();
            stageslist.Add("EVALUATION");

            CandidateFilteringDTO candidateFiltringDTO = new CandidateFilteringDTO
            {
                RecruitmentId = null,
                Status = statuslist,
                Stages = stageslist
            };
            int expectedCount = 2;

            // Act
            IQueryable<Candidate> candidates_iq = candidates.AsQueryable();
            candRepoMock.Setup(i => i.GetAll()).Returns(candidates_iq);
            CandidateListing candidateListingActual = candidateService.GetCandidates(paging, sortOrder, candidateFiltringDTO);

            //Assert
            Assert.True(expectedCount == candidateListingActual.CandidateInfoForListDTOs.Count());
        }
    }
}