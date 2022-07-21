using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Moq;

namespace Tests.AuthTests
{
    public class ConfirmUserTests : BaseAuthServiceTest
    {
        [Fact]
        public void ConfirmUser_ShouldReturnTrue_DbGuidAndInputGuidAreTheSame()
        {
            string inputEmail = "test@gmail.com";
            Guid inputGuid = Guid.NewGuid();

            User user = new()
            {
                ConfirmationGuid = inputGuid
            };

            UserRepositoryMock.Setup(x => x.GetUserByEmail(inputEmail)).Returns(user);
            UserRepositoryMock.Setup(x => x.UpdateAndSaveChanges(user)).Verifiable();

            bool check = sut.ConfirmUser(inputGuid, inputEmail);

            Assert.True(check);
        }

        [Fact]
        public void ConfirmUser_ShouldReturnFalse_DbGuidAndInputGuidAreNotTheSame()
        {
            string inputEmail = "test@gmail.com";
            Guid inputGuid = Guid.NewGuid();

            User user = new()
            {
                ConfirmationGuid = Guid.NewGuid(),
            };

            UserRepositoryMock.Setup(x => x.GetUserByEmail(inputEmail)).Returns(user);

            bool check = sut.ConfirmUser(inputGuid, inputEmail);

            UserRepositoryMock.Verify(x => x.GetUserByEmail(inputEmail), Times.Once);
            Assert.False(check);
        }

        [Fact]
        public void ConfirmUser_ShouldReturnFalse_UserNotInDb()
        {
            string inputEmail = "test@gmail.com";
            Guid inputGuid = Guid.NewGuid();

            User? user = null;

            UserRepositoryMock.Setup(x => x.GetUserByEmail(inputEmail)).Returns(user);

            bool check = sut.ConfirmUser(inputGuid, inputEmail);

            UserRepositoryMock.Verify(x => x.GetUserByEmail(inputEmail), Times.Once);
            Assert.False(check);
        }
    }
}