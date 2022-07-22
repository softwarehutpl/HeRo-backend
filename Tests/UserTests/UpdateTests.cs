using Data.DTOs.User;
using Data.Entities;
using Moq;

namespace Tests.UserTests
{
    public class UpdateTests : BaseUserServiceTests
    {
        [Fact]
        public void Update_ShouldReturnUserId_UserExists()
        {
            int userId = 10;
            string newStatus = "NOTACTIVE";
            string newRoleName = "NEWROLE";

            UserEditDTO dto = new(userId, newStatus, newRoleName);

            User userToUpdate = new()
            {
                Id = userId,
                UserStatus = "ACTIVE",
                RoleName = "TECH",
            };

            UserRepositoryMock.Setup(x => x.GetById(userId)).Returns(userToUpdate);
            UserRepositoryMock.Setup(x => x.UpdateAndSaveChanges(userToUpdate)).Verifiable();

            bool expected = sut.Update(dto, out errorMessage);

            UserRepositoryMock.Verify(x => x.UpdateAndSaveChanges(userToUpdate), Times.Once);
            UserRepositoryMock.Verify(x => x.GetById(userId), Times.Once);

            Assert.True(expected);
        }

        [Fact]
        public void Update_ShouldReturn0_UserNotInDb()
        {
            int userId = 10;
            string newStatus = "NOTACTIVE";
            string newRoleName = "NEWROLE";

            UserEditDTO dto = new(userId, newStatus, newRoleName);

            User userToUpdate = null;

            UserRepositoryMock.Setup(x => x.GetById(userId)).Returns(userToUpdate);

            bool result = sut.Update(dto, out errorMessage);

            UserRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            Assert.False(result);
        }
    }
}