using HeRoBackEnd.Controllers;
using Moq;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SkillTests
{
    public class BaseSkillTests
    {
        private readonly SkillController _skillController;
        public BaseSkillTests()
        {
            Mock<ISkillService> _skillService = new Mock<ISkillService>();

            _skillController = new SkillController(_skillService.Object);
        }
    }
}
