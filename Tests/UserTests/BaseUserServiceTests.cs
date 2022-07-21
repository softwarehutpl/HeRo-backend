using Data.IRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Services;

namespace Tests.UserTests
{
    public class BaseUserServiceTests
    {
        protected UserService sut;
        protected Mock<IUserRepository> UserRepositoryMock = new();
        protected Mock<ILogger<UserService>> LoggerMock = new();

        protected BaseUserServiceTests()
        {
            sut = new UserService(LoggerMock.Object, UserRepositoryMock.Object);
        }
    }
}