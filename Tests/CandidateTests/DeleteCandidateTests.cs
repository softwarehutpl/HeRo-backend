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
    public class DeleteCandidateTests : BaseCandidateTests
    {
        
        [Fact]
        public void EntersGetByIdOnce()
        {
            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new CandidateService(
                mapper,
                candRepoMock.Object,
                loggerMock.Object,
                recruitmentRepositoryMock.Object
            );

            int id = 1;
            Candidate candidate = new()
            {
                Id = id
            };

            var dto = new DeleteCandidateDTO(1);

            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            candidateService.DeleteCandidate(dto, out errorMessage);

            candRepoMock.Verify(v=>v.GetById(id), Times.AtLeastOnce);
        }

        [Fact]
        public void DeletesCandidateWhenDataIsValid()
        {
            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new CandidateService(
                mapper,
                candRepoMock.Object,
                loggerMock.Object,
                recruitmentRepositoryMock.Object
            );

            int id = 9999;
            Candidate candidate = new Candidate()
            {
                Id = id,
                Name = "name"
            };

            var dto = new DeleteCandidateDTO(id);

            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            Assert.Equal(1, candidateService.DeleteCandidate(dto, out errorMessage));
        }

        [Fact]
        public void FailsDeletingCandidateWhenDataIsInvalid()
        {
            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new CandidateService(
                mapper,
                candRepoMock.Object,
                loggerMock.Object,
                recruitmentRepositoryMock.Object
            );

            int id = 9998;
            Candidate? candidate = null;

            var dto = new DeleteCandidateDTO(id);

            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            Assert.NotEqual(1, candidateService.DeleteCandidate(dto, out errorMessage));
        }

    }
}
