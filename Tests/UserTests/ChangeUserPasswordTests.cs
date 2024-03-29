﻿using Moq;
using Data.Entities;

namespace Tests.UserTests
{
    public class ChangeUserPasswordTests : BaseUserServiceTests
    {
        [Fact]
        public async Task ChangeUserPassword_ShouldReturnTrue_PasswordChanged()
        {
            string inputEmail = "testc@gmail.com";
            string password = "password";
            string passwordInDB = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8";

            User user = new()
            {
                Password = passwordInDB
            };

            UserRepositoryMock.Setup(x => x.GetUserByEmail(inputEmail)).Returns(user);
            UserRepositoryMock.Setup(x => x.ChangeUserPasswordByEmail(inputEmail, It.IsAny<string>())).Verifiable();

            bool actual = await sut.ChangeUserPassword(inputEmail, password);

            UserRepositoryMock.Verify(x => x.GetUserByEmail(inputEmail), Times.Once);
            UserRepositoryMock.Verify(x => x.ChangeUserPasswordByEmail(inputEmail, It.IsAny<string>()), Times.Never);

            Assert.False(actual);
        }

        [Fact]
        public async Task ChangeUserPassword_ShouldReturnFalse_PasswordNotChanged()
        {
            string inputEmail = "testc@gmail.com";
            string password = "password";
            string passwordInDB = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d812341";

            User user = new()
            {
                Password = passwordInDB
            };

            UserRepositoryMock.Setup(x => x.GetUserByEmail(inputEmail)).Returns(user);
            UserRepositoryMock.Setup(x => x.ChangeUserPasswordByEmail(inputEmail, It.IsAny<string>())).Verifiable();

            bool actual = await sut.ChangeUserPassword(inputEmail, password);

            UserRepositoryMock.Verify(x => x.GetUserByEmail(inputEmail), Times.Once);
            UserRepositoryMock.Verify(x => x.ChangeUserPasswordByEmail(inputEmail, It.IsAny<string>()), Times.Once);

            Assert.True(actual);
        }
    }
}