using AutoMapper;
using Data.IRepositories;
using HeRoBackEnd.Profiles;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CandidateTests
{
    public class BaseCandidateTests
    {
        protected static Mock<ICandidateRepository> candidateRepositoryMock = new Mock<ICandidateRepository>();
        protected static IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new CandidateProfile())));
        protected static Mock<ILogger<RecruitmentService>> loggerMock = new Mock<ILogger<RecruitmentService>>();
        protected static Mock<IRecruitmentRepository> recruitmentRepositoryMock = new Mock<IRecruitmentRepository>();  
    }
}
