using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.InterviewTests
{
    public class DeleteTests : BaseTest
    {
        [Fact]
        public void Delete_ShoudWork()
        {
            Interview interview = Interviews[0];

            //Arrange
            int expectedResult = 1;

            // Act
            IQueryable<Interview> interviews = Interviews.AsQueryable();
            InterviewRepository.Setup(i => i.GetById(1)).Returns(interview);

            int actualResult = InterviewService.Delete(1, 1);

            //Assert
            Assert.True(expectedResult == actualResult);
        }

        [Fact]
        public void Delete_ShoudFail()
        {
            Interview interview = null;

            //Arrange
            int expectedResult = 0;

            // Act
            IQueryable<Interview> interviews = Interviews.AsQueryable();
            InterviewRepository.Setup(i => i.GetById(1)).Returns(interview);

            int actualResult = InterviewService.Delete(1, 1);

            //Assert
            Assert.True(expectedResult == actualResult);
        }
    }
}