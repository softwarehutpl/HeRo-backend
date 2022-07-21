using Data.DTOs.Candidate;
using Data.Entities;
using Data.IRepositories;
using Moq;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CandidateTests
{
    public class AllocateRecruiterAndTechTests : BaseCandidateTests
    {

        [Fact]
        public void EntersGetByIdAtLeastOnce()
        {
            var candRepoMock = new Mock<ICandidateRepository>();

            CandidateService candidateService = new CandidateService(
               mapper,
               candRepoMock.Object,
               loggerMock.Object,
               recruitmentRepositoryMock.Object
            );

            int id = 9998;
            CandidateAssigneesDTO dto = new();
            dto.RecruiterId = 1;
            dto.TechId = 2;

            int result = candidateService.AllocateRecruiterAndTech(id, dto, out errorMessage);
            candRepoMock.Verify(c => c.GetById(id), Times.AtLeastOnce);
        }

        [Fact]
        public void SaveUserWhenGivenValidData()
        {
            var candRepoMock = new Mock<ICandidateRepository>();

            CandidateService candidateService = new CandidateService(
               mapper,
               candRepoMock.Object,
               loggerMock.Object,
               recruitmentRepositoryMock.Object
            );

            int id = 9998;

            CandidateAssigneesDTO dto = new();
            dto.RecruiterId = 1;
            dto.TechId = 2;

            Candidate candidate = new Candidate()
            {
                Id = id,
                Name = "name"
            };

            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            int result = candidateService.AllocateRecruiterAndTech(id, dto, out errorMessage);
            Assert.Equal(1, result);
        }

        [Fact]
        public void ErrorAssigningRecruiterWhenGivenInvalidData()
        {
            var candRepoMock = new Mock<ICandidateRepository>();

            CandidateService candidateService = new CandidateService(
               mapper,
               candRepoMock.Object,
               loggerMock.Object,
               recruitmentRepositoryMock.Object
            );

            int id = 9998;

            CandidateAssigneesDTO dto = new();
            dto.RecruiterId = 1;
            dto.TechId = 2;

            Candidate candidate = null;

            candRepoMock.Setup(x => x.GetById(id)).Equals(candidate);

            int result = candidateService.AllocateRecruiterAndTech(id, dto, out errorMessage);
            Assert.NotEqual(1, result);
        }
    }
}
