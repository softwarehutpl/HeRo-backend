using Data.Entities;
using Moq;

namespace Tests.UserTests
{
    public class CreateUserTests : BaseUserServiceTests
    {
        [Fact]
        public async Task CreateUser_ShouldReturnGuid_ShouldWork()
        {
            string name = "John";
            string surname = "Teslaw";
            string password = "password";
            string email = "test@gmail.com";
            Guid expected = Guid.Empty;
            UserRepositoryMock.Setup(x => x.AddAndSaveChanges(It.IsAny<User>())).Verifiable();

            Guid actual = await sut.CreateUser(name, surname, password, email);

            UserRepositoryMock.Verify(x => x.AddAndSaveChanges(It.IsAny<User>()), Times.Once);
            Assert.False(actual == expected);
        }
    }
}