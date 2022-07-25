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
    public class UpdateCandidateTests : BaseCandidateTests
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

            var dto = new UpdateCandidateDTO()
            {
                Email = "email@gmail.com",
                PhoneNumber = "321321321"
            };

            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            candidateService.UpdateCandidate(id, dto, out errorMessage);

            candRepoMock.Verify(v=>v.GetById(id), Times.AtLeastOnce);
        }

        [Fact]
        public void Returns1IfDataIsValid()
        {
            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new CandidateService(
                mapper,
                candRepoMock.Object,
                loggerMock.Object,
                recruitmentRepositoryMock.Object
            );

            int id = 9997;
            Candidate candidate = new Candidate()
            {
                Id = id,
                Name = "name"
            };

            var dto = new UpdateCandidateDTO()
            {
                Email = "email@gmail.com",
                PhoneNumber = "321321321"
            };

            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            Assert.True(candidateService.UpdateCandidate(id, dto, out errorMessage));
        }

        [Fact]
        public void FailsSavingChangesWhenDataIsInvalid()
        {
            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new CandidateService(
                mapper,
                candRepoMock.Object,
                loggerMock.Object,
                recruitmentRepositoryMock.Object
            );

            int id = 9999;
            Candidate? candidate = null;

            var dto = new UpdateCandidateDTO()
            {
                Email = "email@gmail.com",
                PhoneNumber = "321321321"
            };

            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            Assert.False(candidateService.UpdateCandidate(id, dto, out errorMessage));
        }

    }
}
