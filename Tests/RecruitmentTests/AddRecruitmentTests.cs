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
    public class AddRecruitmentTests : BaseRecruitmentTests
    {
        [Fact]
        public void AddRecruitment_WhenEverythingIsOk_ShouldCreate()
        {
            //arrange
            CreateRecruitmentDTO dto = new CreateRecruitmentDTO()
            {
                BeginningDate = DateTime.Now,
                EndingDate = DateTime.Now,
                Name = "Name",
                Description = "Description",
                RecruiterId = 1,
                CreatedById = 1,
                CreatedDate = DateTime.Now,
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
            Recruitment recruitment = new Recruitment()
            {
                BeginningDate = dto.BeginningDate,
                EndingDate = dto.EndingDate,
                Name = dto.Name,
                Description = dto.Description,
                RecruiterId = dto.RecruiterId,
                CreatedById = dto.CreatedById,
                CreatedDate = dto.CreatedDate,
                LastUpdatedDate = dto.LastUpdatedDate,
                RecruitmentPosition = dto.RecruitmentPosition,
                Localization = dto.Localization,
                Seniority = dto.Seniority,
                IsPublic = dto.IsPublic
            };
            IEnumerable<RecruitmentSkill> skills = new List<RecruitmentSkill>()
            {
                new RecruitmentSkill(1,1),
                new RecruitmentSkill(2,2)
            }.AsEnumerable();

            _mapper.Setup(e => e.Map<Recruitment>(dto)).Returns(recruitment);
            _mapper.Setup(e => e.Map<IEnumerable<RecruitmentSkill>>(dto.Skills)).Returns(skills);

            //act
            int result = _recruitmentService.AddRecruitment(dto);

            //assert
            Assert.Equal(result, 1);
            _recruitmentRepo.Verify(e => e.AddAndSaveChanges(It.IsAny<Recruitment>()), Times.Once);
        }
    }
}
