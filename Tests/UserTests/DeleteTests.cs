using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Moq;

namespace Tests.UserTests
{
    public class DeleteTests : BaseUserServiceTests
    {
        [Fact]
        public void Delete_ShouldReturnUserId_UserExists()
        {
            int userId = 5;
            int loginUserId = 1;
            string expectedStatus = "DELETED";
            int expectedId = 5;

            User userToDelete = new()
            {
                Id = userId,
                UserStatus = "ACTIVE",
                RoleName = "TECH",
            };

            UserRepositoryMock.Setup(x => x.GetById(userId)).Returns(userToDelete);
            UserRepositoryMock.Setup(x => x.UpdateAndSaveChanges(It.IsAny<User>())).Verifiable();

            sut.Delete(userId, loginUserId, out errorMessage);

            UserRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            UserRepositoryMock.Verify(x => x.UpdateAndSaveChanges(It.IsAny<User>()), Times.Once);

            Assert.Equal(userToDelete.UserStatus, expectedStatus);
            Assert.True(userId == expectedId);
        }

        [Fact]
        public void Delete_ShouldReturn0_UserNotInDb()
        {
            int userId = 5;
            int loginUserId = 1;
            int expectedId = -1;
            User user = null;

            UserRepositoryMock.Setup(x => x.GetById(userId)).Returns(user);

            int actualId = sut.Delete(userId, loginUserId, out errorMessage);

            UserRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            UserRepositoryMock.Verify(x => x.UpdateAndSaveChanges(It.IsAny<User>()), Times.Never);

            Assert.True(actualId == expectedId);
        }
    }
}