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

            int actualResult = InterviewService.Delete(1, 1, out errorMessage);

            //Assert
            Assert.True(expectedResult == actualResult);
        }

        [Fact]
        public void Delete_ShouldFail()
        {
            Interview interview = null;

            //Arrange
            int expectedResult = -1;

            // Act
            IQueryable<Interview> interviews = Interviews.AsQueryable();
            InterviewRepository.Setup(i => i.GetById(1)).Returns(interview);

            int actualResult = InterviewService.Delete(1, 1, out errorMessage);

            //Assert
            Assert.True(expectedResult == actualResult);
        }

        [Fact]
        public void Delete_ShoudException()
        {
            Interview interview = Interviews[0];

            Exception ex = new Exception();

            //Arrange
            int expectedResult = -1;

            // Act
            IQueryable<Interview> interviews = Interviews.AsQueryable();
            InterviewRepository.Setup(i => i.GetById(1)).Returns(interview);
            InterviewRepository.Setup(i => i.UpdateAndSaveChanges(interview)).Throws(ex);

            int actualResult = InterviewService.Delete(1, 1, out errorMessage);

            //Assert
            Assert.True(expectedResult == actualResult);
        }
    }
}