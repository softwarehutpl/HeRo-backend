using AutoMapper;
using Data.IRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.RecruitmentTests
{
    public class BaseRecruitmentTests
    {
        protected readonly Mock<IMapper> _mapper;
        protected readonly Mock<IRecruitmentRepository> _recruitmentRepo;
        protected readonly Mock<ILogger<RecruitmentService>> _logger;
        protected readonly RecruitmentService _recruitmentService;

        public BaseRecruitmentTests()
        {
            _mapper = new Mock<IMapper>();
            _recruitmentRepo = new Mock<IRecruitmentRepository>();
            _logger = new Mock<ILogger<RecruitmentService>>();

            _recruitmentService = new RecruitmentService(_mapper.Object, _recruitmentRepo.Object, _logger.Object);
        }
    }
}
