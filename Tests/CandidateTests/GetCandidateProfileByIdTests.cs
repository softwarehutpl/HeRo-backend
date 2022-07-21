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
    public class GetCandidateProfileByIdTests : BaseCandidateTests
    {
        

        [Fact]
        public void GetNullWhenGettingNonexistingCandidateTest()
        {
            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new CandidateService(
           mapper,
           candRepoMock.Object,
           loggerMock.Object,
            new Mock<IRecruitmentRepository>().Object
           );

            int id = 9998;
            Candidate candidate = null;
            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            CandidateProfileDTO? result = candidateService.GetCandidateProfileById(id, out errorMessage);
            CandidateProfileDTO? expected = null;

            Assert.Equal(expected, result);
        }


        [Fact]
        public void GetCandidateByIdWhenIdIsOfValidCandidate()
        {
            var candRepoMock = new Mock<ICandidateRepository>();
            CandidateService candidateService = new CandidateService(
            mapper,
            candRepoMock.Object,
            loggerMock.Object,
             new Mock<IRecruitmentRepository>().Object
            );

            int id = 9997;
            Candidate candidate = new()
            {
                Id =id,
            };
            candRepoMock.Setup(x => x.GetById(id)).Returns(candidate);

            CandidateProfileDTO? result = candidateService.GetCandidateProfileById(id, out errorMessage);
            CandidateProfileDTO? expected = null;

            Assert.NotEqual(expected, result);
        }
    }
}
