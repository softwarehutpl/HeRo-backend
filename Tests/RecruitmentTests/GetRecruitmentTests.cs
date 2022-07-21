using Data.DTOs.Recruitment;
using Data.DTOs.RecruitmentSkill;
using Data.Entities;

namespace Tests.RecruitmentTests
{
    public class GetRecruitmentTests : BaseRecruitmentTests
    {
        [Fact]
        public void GetRecruitment_ShoudWork()
        {
            //Arrange
            RecruitmentDetailsDTO recruitmentDetailsDTOExpected = new RecruitmentDetailsDTO
            {
                Id = 1,
                BeginningDate = new DateTime(2022, 7, 11),
                EndingDate = new DateTime(2022, 7, 22),
                Name = "Staz",
                Description = "Frontend",
                RecruiterId = 6,
                CandidateCount = 2,
                RecruitmentPosition = "Trainee",
                Localization = "Białystok",
                Seniority = "Trainee",
                HiredCount = 2,
                Skills = new List<RecruitmentSkillDetailsDTO>
                {
                    new RecruitmentSkillDetailsDTO
                    {
                        SkillId = 1,
                        Name = "C#",
                        SkillLevel = 3,
                    },
                    new RecruitmentSkillDetailsDTO
                    {
                        SkillId = 2,
                        Name = "Git",
                        SkillLevel = 2,
                    }
                }
            };

            // Act
            _recruitmentRepo.Setup(i => i.GetRecruitmentDTOById(1)).Returns(recruitmentDetailsDTOExpected);
            RecruitmentDetailsDTO recruitmentDetailsDTOActual = _recruitmentService.GetRecruitment(1);

            //Assert
            Assert.True(recruitmentDetailsDTOActual != null);
        }

        [Fact]
        public void GetRecruitment_ShoudFail()
        {
            RecruitmentDetailsDTO recruitmentDetailsDTOExpected = null;

            _recruitmentRepo.Setup(i => i.GetRecruitmentDTOById(2)).Returns(recruitmentDetailsDTOExpected);
            RecruitmentDetailsDTO recruitmentDetailsDTOActual = _recruitmentService.GetRecruitment(2);

            //Assert
            Assert.True(recruitmentDetailsDTOActual == null);
        }
    }
}