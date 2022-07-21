using Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SkillTests
{
    public class GetSkillsTests : BaseSkillTests
    {
        [Fact]
        public void GetSkills_WhenEverythingIsOk_ShouldReturnAllSkills()
        {
            //arrange
            IQueryable<Skill> skills = new List<Skill>()
            {
                new Skill(1,"C#"),
                new Skill(2,"Java"),
                new Skill(3, "Python")
            }.AsQueryable<Skill>();

            _skillRepo.Setup(e => e.GetAll()).Returns(skills);

            //act
            var result = _skillService.GetSkills();

            //assert
            Assert.Equal(result.Count(), skills.Count());
            _skillRepo.Verify(e => e.GetAll(), Times.Once);
        }
    }
}
