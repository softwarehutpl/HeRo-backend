using Common.Listing;
using Data.DTOs.User;
using Data.Entities;
using Moq;

namespace Tests.UserTests
{
    public class GetUsersTests : BaseUserServiceTests
    {
        [Fact]
        public void GetUsers_ShouldReturnUsersList_ShouldWork_NoFiltring_NoSort()
        {
            string email = "";
            string userStatus = "";
            string roleName = "";

            ICollection<User> userList = new List<User>();
            {
                new User { Id = 1, Email = "test1@gmail.com" };
                new User { Id = 2, Email = "test2@gmail.com" };
                new User { Id = 3, Email = "test3@gmail.com" };
            };

            int expectedCount = userList.Count();

            Paging paging = new()
            {
                PageNumber = 1,
                PageSize = 10
            };

            SortOrder? sortOrder = new()
            {
                Sort = new List<KeyValuePair<string, string>>()
            };

            UserFiltringDTO userFiltringDTO = new(email, userStatus, roleName);

            UserRepositoryMock.Setup(x => x.GetAllUsers()).Returns(userList.AsQueryable);

            var actualList = sut.GetUsers(paging, sortOrder, userFiltringDTO);

            UserRepositoryMock.Verify(x => x.GetAllUsers(), Times.Once);
            Assert.Equal(expectedCount, actualList.TotalCount);
        }

        [Theory]
        [InlineData("@gmail.com", null, null, 1)]
        [InlineData("@softwarehut.com", "", "", 3)]
        [InlineData("", "ACTIVE", "", 5)]
        [InlineData("", "NOTACTIVE", "", 2)]
        [InlineData("", "", "ADMIN", 2)]
        [InlineData("", "", "TECH", 5)]
        [InlineData("", "ACTIVE", "TECH", 3)]
        public void GetUsers_ShouldReturnUsersFiltredList_ShouldWork_Filtred_NoSort(string email, string userStatus, string roleName, int expectedCount)
        {
            Paging paging = new()
            {
                PageNumber = 1,
                PageSize = 10
            };

            SortOrder? sortOrder = new()
            {
                Sort = new List<KeyValuePair<string, string>>()
            };

            List<User> userList = new List<User>()
            {
                new User { Id = 1, Email = "test1@gmail.com", UserStatus = "ACTIVE", RoleName = "ADMIN" },
                new User { Id = 2, Email = "test1@onet.com", UserStatus = "ACTIVE", RoleName = "TECH" },
                new User { Id = 3, Email = "test2@onet.com", UserStatus = "ACTIVE", RoleName = "ADMIN" },
                new User { Id = 4, Email = "test3@onet.com", UserStatus = "NOTACTIVE", RoleName = "TECH" },
                new User { Id = 5, Email = "test1@softwarehut.com", UserStatus = "NOTACTIVE", RoleName = "TECH" },
                new User { Id = 6, Email = "test2@softwarehut.com", UserStatus = "ACTIVE", RoleName = "TECH" },
                new User { Id = 7, Email = "test3@softwarehut.com", UserStatus = "ACTIVE", RoleName = "TECH" }
            };

            UserFiltringDTO userFiltringDTO = new(email, userStatus, roleName);

            UserRepositoryMock.Setup(x => x.GetAllUsers()).Returns(userList.AsQueryable());

            var actualList = sut.GetUsers(paging, sortOrder, userFiltringDTO);

            UserRepositoryMock.Verify(x => x.GetAllUsers(), Times.Once);
            Assert.Equal(expectedCount, actualList.TotalCount);
        }
    }
}