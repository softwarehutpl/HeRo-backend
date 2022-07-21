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
    public class AddCandidateNoteTests : BaseCandidateTests
    {
        

        [Fact]
        public void GetNullWhenAddingHRNoteToNonexistingUserTest()
        {
            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new CandidateService(
               mapper,
               candRepoMock.Object,
               loggerMock.Object,
               recruitmentRepositoryMock.Object
           );

            CandidateAddHRNoteDTO dto = new();
            dto.RecruiterId = 1;
            dto.Score = 5;

            int id = 9998;
            Candidate candidate = null;

            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            int result = candidateService.AddHRNote(id, dto);
            Assert.NotEqual(1, result);
        }

        [Fact]
        public void SuccessWhenAddingHRNoteToExistingUserTest()
        {
            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new CandidateService(
            mapper,
            candRepoMock.Object,
            loggerMock.Object,
            recruitmentRepositoryMock.Object
            );

            CandidateAddHRNoteDTO dto = new();
            dto.RecruiterId = 1;
            dto.Score = 5;

            int id = 9999;
            Candidate candidate = new Candidate();
            candidate.Id = id;

            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            int result = candidateService.AddHRNote(id, dto);
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetNullWhenAddingTechNoteToNonexistingUserTest()
        {

            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new(
                mapper,
                candRepoMock.Object,
                loggerMock.Object,
                recruitmentRepositoryMock.Object
            );

            CandidateAddTechNoteDTO dto = new()
            {
                TechId = 1,
                Score = 5
            };

            int id = 9998;
            Candidate? candidate = null;

            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            int result = candidateService.AddTechNote(id, dto);
            Assert.NotEqual(1, result);
        }

        [Fact]
        public void SuccessWhenAddingTechNoteToExistingUserTest()
        {
            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new(
                mapper,
                candRepoMock.Object,
                loggerMock.Object,
                recruitmentRepositoryMock.Object
            );

            CandidateAddTechNoteDTO dto = new();
            dto.TechId = 1;
            dto.Score = 5;

            int id = 9999;
            Candidate candidate = new()
            {
                Id = id
            };

            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            int result = candidateService.AddTechNote(id, dto);
            Assert.Equal(1, result);
        }
    }
}
