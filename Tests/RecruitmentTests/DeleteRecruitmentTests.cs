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
    public class DeleteRecruitmentTests : BaseRecruitmentTests
    {
        [Fact]
        public void DeleteRecruitment_WhenRecruitmentDoesntExist_ShouldNotUpdate()
        {
            //arrange
            DeleteRecruitmentDTO dto = new DeleteRecruitmentDTO(0);
            Recruitment recruitment = null;

            _recruitmentRepo.Setup(e => e.GetById(dto.Id)).Returns(recruitment);

            //act
            int result = _recruitmentService.DeleteRecruitment(dto, out errorMessage);

            //assert
            Assert.Equal(result, -1);
            _recruitmentRepo.Verify(e => e.GetById(dto.Id), Times.Once);
            _recruitmentRepo.Verify(e => e.UpdateAndSaveChanges(It.IsAny<Recruitment>()), Times.Never());
        }
        [Fact]
        public void DeleteRecruitment_WhenEverythingIsOk_ShouldUpdate()
        {
            //arrange
            DeleteRecruitmentDTO dto = new DeleteRecruitmentDTO(1)
            {
                LastUpdatedById = 1,
                LastUpdatedDate = DateTime.Now,
                DeletedById = 1,
                DeletedDate = DateTime.Now
            };

            Recruitment recruitment = new Recruitment();

            _recruitmentRepo.Setup(e => e.GetById(dto.Id)).Returns(recruitment);

            //act
            int result = _recruitmentService.DeleteRecruitment(dto, out errorMessage);

            //assert
            Assert.Equal(result, 1);
            _recruitmentRepo.Verify(e => e.GetById(dto.Id), Times.Once());
            _recruitmentRepo.Verify(e => e.UpdateAndSaveChanges(recruitment), Times.Once);
        }
    }
}
