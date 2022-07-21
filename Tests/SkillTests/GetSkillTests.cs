using Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SkillTests
{
    public class GetSkillTests : BaseSkillTests 
    {
        [Fact]
        public void GetSkill_WhenSkillDoesntExist_ShouldReturnNull()
        {
            //arrange
            int id = 0;
            Skill skill = null;

            _skillRepo.Setup(e => e.GetById(id)).Returns(skill);

            //act
            var result = _skillService.GetSkill(id);

            //assert
            Assert.Null(result);
            _skillRepo.Verify(e => e.GetById(id), Times.Once);
        }
        [Fact]
        public void GetSkill_WhenSkillExists_ShouldReturnSkill()
        {
            //arrange
            int id = 1;

            _skillRepo.Setup(e => e.GetById(id)).Returns(new Skill());

            //act
            var result = _skillService.GetSkill(id);

            //assert
            Assert.NotNull(result);
            _skillRepo.Verify(e => e.GetById(id), Times.Once);
        }
    }
}
