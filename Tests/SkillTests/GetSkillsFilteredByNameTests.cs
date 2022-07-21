using Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SkillTests
{
    public class GetSkillsFilteredByNameTests : BaseSkillTests
    {
        [Fact]
        public void GetSkillsFilteredByName_WhenEverythingIsOk_ShouldReturnOnly5MatchingSkills()
        {
            //arrange
            string name = "C";
            IQueryable<Skill> skills = new List<Skill>()
            {
                new Skill(1,"C"),
                new Skill(2, "C++"),
                new Skill(3, "C#"),
                new Skill(4, "Basic knowledge of software engineering"),
                new Skill(5, "Basic understaning of version control systems"),
                new Skill(6, "Programming in Basic"),
                new Skill(7, "Python"),
                new Skill(8, "Java")
            }.AsQueryable<Skill>();

            _skillRepo.Setup(e => e.GetAll()).Returns(skills);

            //act
            var result = _skillService.GetSkillsFilteredByName(name);

            //assert
            Assert.NotNull(result);
            Assert.True(result.All(e => e.Name.ToLower().Contains(name.ToLower())));
            Assert.Equal(result.Count(), 5);
            _skillRepo.Verify(e => e.GetAll(), Times.Once);
        }
    }
}
