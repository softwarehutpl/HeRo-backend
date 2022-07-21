using Data.DTOs.Skill;
using Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SkillTests
{
    public class UpdateSkillTests : BaseSkillTests
    {
        [Fact]
        public void UpdateSkill_WhenSkillDoesntExist_ShouldNotUpdate()
        {
            //arrange
            UpdateSkillDTO dto = new UpdateSkillDTO(0, "C#");
            Skill skill = null;

            _skillRepo.Setup(e => e.GetById(dto.Id)).Returns(skill);

            //act
            int result = _skillService.UpdateSkill(dto, out errorMessage);

            //assert
            Assert.Equal(result, -1);
            _skillRepo.Verify(e => e.GetById(dto.Id), Times.Once);
            _skillRepo.Verify(e => e.Exists(dto.Id, dto.Name), Times.Never());
            _skillRepo.Verify(e => e.UpdateAndSaveChanges(It.IsAny<Skill>()), Times.Never());
        }
        [Fact]
        public void UpdateSkill_WhenSkillNameAlreadyExistsAndIsNotTheUpdatedSkillName_ShouldNotUpdate()
        {
            //arrange
            UpdateSkillDTO dto = new UpdateSkillDTO(1, "C#");

            _skillRepo.Setup(e => e.GetById(dto.Id)).Returns(new Skill());
            _skillRepo.Setup(e => e.Exists(dto.Id, dto.Name)).Returns(true);

            //act
            int result = _skillService.UpdateSkill(dto, out errorMessage);

            //assert
            Assert.Equal(result, -1);
            _skillRepo.Verify(e => e.GetById(dto.Id), Times.Once);
            _skillRepo.Verify(e => e.Exists(dto.Id, dto.Name), Times.Once);
            _skillRepo.Verify(e => e.UpdateAndSaveChanges(It.IsAny<Skill>()), Times.Never());
        }
        [Fact]
        public void UpdateSkill_WhenSkillExistsAndSkillNameDoesntExistOrIsTheUpdatedSkillName_ShouldUpdate()
        {
            //arrange
            UpdateSkillDTO dto = new UpdateSkillDTO(1, "C#");

            _skillRepo.Setup(e => e.GetById(dto.Id)).Returns(new Skill());
            _skillRepo.Setup(e => e.Exists(dto.Id, dto.Name)).Returns(false);

            //act
            int result = _skillService.UpdateSkill(dto, out errorMessage);

            //assert
            Assert.Equal(result, 1);
            _skillRepo.Verify(e => e.GetById(dto.Id), Times.Once);
            _skillRepo.Verify(e => e.Exists(dto.Id, dto.Name), Times.Once);
            _skillRepo.Verify(e => e.UpdateAndSaveChanges(It.IsAny<Skill>()), Times.Once);
        }
    }
}
