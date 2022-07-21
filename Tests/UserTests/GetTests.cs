using Data.DTOs.User;
using Data.Entities;
using Moq;

namespace Tests.UserTests
{
    public class GetTests : BaseUserServiceTests
    {
        [Fact]
        public void Get_ShouldReturnUserDTO_UserIdInDb()
        {
            int userId = 1;
            string userEmail = "test@gmail.com";
            string userStatus = "Active";
            string userRoleName = "ADMIN";

            User user = new User()
            {
                Id = userId,
                Email = userEmail,
                UserStatus = userStatus,
                RoleName = userRoleName
            };

            UserRepositoryMock.Setup(x => x.GetById(userId)).Returns(user);

            UserDTO? userDTO = sut.Get(userId);

            UserRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            Assert.NotNull(userDTO);
        }

        [Fact]
        public void Get_ShouldReturnNull_UserIdNotInDb()
        {
            int userId = 1;
            User? userIsNull = null;

            UserRepositoryMock.Setup(x => x.GetById(userId)).Returns(userIsNull);

            UserDTO? userDTO = sut.Get(userId);

            UserRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            Assert.Null(userDTO);
        }
    }
}