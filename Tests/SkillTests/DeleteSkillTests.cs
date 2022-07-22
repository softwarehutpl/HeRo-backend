using Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SkillTests
{
    public class DeleteSkillTests : BaseSkillTests
    {
        /*
         * Czy usuwa przy podaniu prawidłowych danych?
         */
        [Fact]
        public void DeleteSkill_WhenSkillDoesntExist_ShouldNotDelete()
        {
            //arrange
            int id = 0;
            Skill skill = null;

            _skillRepo.Setup(e => e.GetById(id)).Returns(skill);

            //act
            bool result =_skillService.DeleteSkill(id, out errorMessage);

            //assert
            Assert.False(result);
            _skillRepo.Verify(e => e.GetById(id), Times.Once);
            _recruitmentSkillRepo.Verify(e => e.IsSkillUsed(id), Times.Never());
            _skillRepo.Verify(e => e.RemoveByIdAndSaveChanges(id), Times.Never());
        }
        [Fact]
        public void DeleteSkill_WhenSkillUsedInRecruitment_ShouldNotDelete()
        {
            //arrange
            int id = 1;
            _skillRepo.Setup(e => e.GetById(id)).Returns(new Skill());
            _recruitmentSkillRepo.Setup(e => e.IsSkillUsed(id)).Returns(true);

            //act
            bool result =_skillService.DeleteSkill(id, out errorMessage);

            //assert
            Assert.False(result);
            _skillRepo.Verify(e => e.GetById(id), Times.Once);
            _recruitmentSkillRepo.Verify(e => e.IsSkillUsed(id), Times.Once);
            _skillRepo.Verify(e => e.RemoveByIdAndSaveChanges(id), Times.Never());
        }
        [Fact]
        public void DeleteSkill_WhenSkillExistsAndIsNotUsed_ShouldDelete()
        {
            //arrange
            int id = 1;
            _skillRepo.Setup(e => e.GetById(id)).Returns(new Skill());
            _recruitmentSkillRepo.Setup(e => e.IsSkillUsed(id)).Returns(false);

            //act
            bool result =_skillService.DeleteSkill(id, out errorMessage);

            //assert
            Assert.True(result);
            _skillRepo.Verify(e => e.GetById(id), Times.Once);
            _recruitmentSkillRepo.Verify(e => e.IsSkillUsed(id), Times.Once);
            _skillRepo.Verify(e => e.RemoveByIdAndSaveChanges(id), Times.Once);
        }
    }
}
