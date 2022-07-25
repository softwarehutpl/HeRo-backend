using Data.IRepositories;
using Data.Repositories;
using HeRoBackEnd.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SkillTests
{
    public class BaseSkillTests
    {
        protected readonly Mock<ILogger<SkillService>> _logger;
        protected readonly Mock<ISkillRepository> _skillRepo;
        protected readonly Mock<IRecruitmentSkillRepository> _recruitmentSkillRepo;
        protected readonly SkillService _skillService;
        protected string errorMessage;
        public BaseSkillTests()
        {
            _logger= new Mock<ILogger<SkillService>>();
            _skillRepo = new Mock<ISkillRepository>();
            _recruitmentSkillRepo = new Mock<IRecruitmentSkillRepository>();
            _skillService = new SkillService(_skillRepo.Object, _logger.Object, _recruitmentSkillRepo.Object);
        }
    }
}
