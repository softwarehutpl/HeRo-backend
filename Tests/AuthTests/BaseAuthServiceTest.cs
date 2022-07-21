using Data.Entities;
using Data.IRepositories;
using Moq;
using Services.Services;

namespace Tests.AuthTests
{
    public class BaseAuthServiceTest
    {
        protected readonly Mock<IUserRepository> UserRepositoryMock = new();
        protected readonly AuthService sut;

        protected BaseAuthServiceTest()
        {
            sut = new AuthService(UserRepositoryMock.Object);
        }
    }
}