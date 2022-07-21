using Data.DTOs.Recruitment;
using Data.DTOs.RecruitmentSkill;
using Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.RecruitmentTests
{
    public class UpdateRecruitmentTests : BaseRecruitmentTests
    {
        [Fact]
        public void UpdateRecruitment_WhenRecruitmentDoesntExist_ShouldNotUpdate()
        {
            //arrange
            int id = 0;
            UpdateRecruitmentDTO dto = new UpdateRecruitmentDTO();
            Recruitment recruitment = null;

            _recruitmentRepo.Setup(e => e.GetRecruitmentDetails(id)).Returns(recruitment);

            //act
            int result=_recruitmentService.UpdateRecruitment(id, dto);

            //assert
            Assert.Equal(result, 0);
            _recruitmentRepo.Verify(e => e.GetRecruitmentDetails(id), Times.Once);
            _recruitmentRepo.Verify(e => e.UpdateAndSaveChanges(It.IsAny<Recruitment>()), Times.Never());
        }
        [Fact]
        public void UpdateRecruitment_WhenEverythingIsOk_ShouldUpdate()
        {
            //arrange
            int id = 1;
            UpdateRecruitmentDTO dto = new UpdateRecruitmentDTO()
            {
                BeginningDate = DateTime.Now,
                EndingDate = DateTime.Now.AddDays(10),
                Name = "Name",
                Description = "Description",
                RecruiterId = 1,
                LastUpdatedById = 1,
                LastUpdatedDate = DateTime.Now,
                RecruitmentPosition = "RecruitmentPosition",
                Localization = "Localization",
                Seniority = "Seniority",
                IsPublic = true,
                Skills = new List<RecruitmentSkillDTO>
                {
                    new RecruitmentSkillDTO(1, 1),
                    new RecruitmentSkillDTO(2, 2)
                }.AsEnumerable()
            };
            Recruitment recruitment = new Recruitment();
            recruitment.Skills = (ICollection<RecruitmentSkill>)new List<RecruitmentSkill>()
            {
                new RecruitmentSkill(id,1,5)
            };

            _recruitmentRepo.Setup(e => e.GetRecruitmentDetails(id)).Returns(recruitment);

            //act
            int result = _recruitmentService.UpdateRecruitment(id, dto);

            //assert
            Assert.Equal(result, 1);
            _recruitmentRepo.Verify(e => e.GetRecruitmentDetails(id), Times.Once());
            _recruitmentRepo.Verify(e => e.UpdateAndSaveChanges(recruitment), Times.Once());
        }
    }
}
