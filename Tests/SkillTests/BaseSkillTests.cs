using Data.Repositories;
using HeRoBackEnd.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Services.IServices;
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
        
        private readonly SkillService _skillService;
        public BaseSkillTests()
        {
            Mock<ILogger<SkillService>> logger;
        Mock<ISkillRepository> skillRepo;
        Mock<IRecruitmentSkillRepository> recruitmentSkillRepo;

        Mock<ISkillService> _skillService = new Mock<ISkillService>();

            _skillController = new SkillController(_skillService.Object);
        }
    }
}
