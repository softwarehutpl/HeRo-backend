using Data.Entities;
using Moq;

namespace Tests.UserTests
{
    public class GetUserGuidTests : BaseUserServiceTests
    {
        [Fact]
        public void SetUserRecoveryGuid_ShouldReturnUserRecoveryGuid_ShouldWork()
        {
            string email = "test@gmail.com";

            Guid expected = new();

            User user = new()
            {
                Email = email,
            };

            UserRepositoryMock.Setup(x => x.GetUserByEmail(email)).Returns(user);
            UserRepositoryMock.Setup(x => x.UpdateAndSaveChanges(user)).Verifiable();

            Guid result = sut.SetUserRecoveryGuid(email);

            UserRepositoryMock.Verify(x => x.GetUserByEmail(email), Times.Once());
            UserRepositoryMock.Verify(x => x.UpdateAndSaveChanges(It.IsAny<User>()), Times.Once);
            Assert.NotEqual(expected, result);
        }
    }
}