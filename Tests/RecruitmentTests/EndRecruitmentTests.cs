using Data.DTOs.Recruitment;
using Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.RecruitmentTests
{
    public class EndRecruitmentTests : BaseRecruitmentTests
    {
        [Fact]
        public void EndRecruitment_WhenRecruitmentDoesntExist_ShouldNotUpdate()
        {
            //arrange
            EndRecruimentDTO dto = new EndRecruimentDTO(0);
            Recruitment recruitment = null;

            _recruitmentRepo.Setup(e => e.GetById(dto.Id)).Returns(recruitment);

            //act
            int result = _recruitmentService.EndRecruitment(dto, out errorMessage);

            //assert
            Assert.Equal(result, -1);
            _recruitmentRepo.Verify(e => e.GetById(dto.Id), Times.Once);
            _recruitmentRepo.Verify(e => e.UpdateAndSaveChanges(It.IsAny<Recruitment>()), Times.Never());
        }
        [Fact]
        public void EndRecruitment_WhenEverythingIsOk_ShouldUpdate()
        {
            //arrange
            EndRecruimentDTO dto = new EndRecruimentDTO(1)
            {
                LastUpdatedById = 1,
                LastUpdatedDate = DateTime.Now,
                EndedById = 1,
                EndedDate = DateTime.Now
            };

            Recruitment recruitment = new Recruitment();

            _recruitmentRepo.Setup(e => e.GetById(dto.Id)).Returns(recruitment);

            //act
            int result = _recruitmentService.EndRecruitment(dto, out errorMessage);

            //assert
            Assert.Equal(result, 1);
            _recruitmentRepo.Verify(e => e.GetById(dto.Id), Times.Once());
            _recruitmentRepo.Verify(e => e.UpdateAndSaveChanges(recruitment), Times.Once);
        }
    }
}
