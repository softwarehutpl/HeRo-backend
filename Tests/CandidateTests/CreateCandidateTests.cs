using AutoMapper;
using Data.DTOs.Candidate;
using Data.Entities;
using Data.IRepositories;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeRoBackEnd.Profiles;

namespace Tests.CandidateTests
{
    public class CreateCandidateTests : BaseCandidateTests
    {
        public static CandidateService candidateService = new CandidateService(
            mapper,
            candidateRepositoryMock.Object,
            loggerMock.Object,
            recruitmentRepositoryMock.Object
            );

        private string _errorMessage;

        [Fact]
       public void CreateCandidateWithCorrectValues()
        {
            CreateCandidateDTO dto = new()
            {
                Name = "Name",
                LastName = "LastName",
                Email = "test@email.com",
                Status = "NEW",
                PhoneNumber = "123123123",
                CvPath = "CvPath",
                OtherExpectations = "Yes",
                ApplicationDate = DateTime.Now,
                AvailableFrom = DateTime.Now.AddDays(1),
                ExpectedMonthlySalary = 5000,
                RecruitmentId = 5
            };

            recruitmentRepositoryMock.Setup(x => x.GetRecruiterId(dto.RecruitmentId)).Returns(1);
           
            Candidate candidate = mapper.Map<Candidate>(dto);

            Assert.Equal(1, candidateService.CreateCandidate(dto, out _errorMessage));
            //candidateRepositoryMock.Verify(g => g.AddAndSaveChanges(candidate), Times.Once);
        }

        [Fact]
        public void CreateCandidateWithIncorrectValues()
        {
            CreateCandidateDTO dto = new()
            {
                Name = "Name",
                LastName = "LastName",
                Email = "test@email.com",
                Status = "NEW",
                PhoneNumber = "123123123",
                CvPath = "CvPath",
                OtherExpectations = "Yes",
                ApplicationDate = DateTime.Now,
                AvailableFrom = DateTime.Now.AddDays(1),
                ExpectedMonthlySalary = 5000,
                RecruitmentId = 5
            };

            recruitmentRepositoryMock.Setup(x => x.GetRecruiterId(dto.RecruitmentId)).Returns(0);

            Candidate candidate = mapper.Map<Candidate>(dto);

            Assert.NotEqual(1, candidateService.CreateCandidate(dto, out _errorMessage));
        }
    }
}
