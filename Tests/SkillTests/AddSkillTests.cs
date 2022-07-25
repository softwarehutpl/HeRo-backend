using Data.Entities;
using HeRoBackEnd.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SkillTests
{
    public class AddSkillTests : BaseSkillTests
    {
        [Fact]
        public void AddSkill_WhenSkillNameAlreadyExists_ShouldNotCreate()
        {
            //arrange
            string skillName = "C#";
            Skill skill = new Skill(skillName);

            _skillRepo.Setup(e => e.Exists(skillName)).Returns(true);

            //act
            bool result =_skillService.AddSkill(skillName, out errorMessage);

            //assert
            Assert.False(result);
            _skillRepo.Verify(e => e.Exists(skillName), Times.Once);
            _skillRepo.Verify(e=>e.AddAndSaveChanges(skill), Times.Never());
        }
        [Fact]
        public void AddSkill_WhenSkillNameDoesntExist_ShouldCreate()
        {
            //arrange
            string skillName = "C#";
            Skill skill = new Skill(skillName);

            _skillRepo.Setup(e => e.Exists(skillName)).Returns(false);
            _skillRepo.Setup(e => e.AddAndSaveChanges(skill)).Returns(skill);

            //act
            bool result =_skillService.AddSkill(skillName, out errorMessage);

            //assert
            Assert.True(result);
            _skillRepo.Verify(e => e.Exists(skillName), Times.Once);
            _skillRepo.Verify(e => e.AddAndSaveChanges(It.IsAny<Skill>()), Times.Once);
        }
    }
}
