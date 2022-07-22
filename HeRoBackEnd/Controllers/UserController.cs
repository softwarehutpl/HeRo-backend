using Common.AttributeRoleVerification;
using Data.DTOs.User;
using HeRoBackEnd.ViewModels;
using HeRoBackEnd.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Listing;
using Services.Services;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets a user specified by an id
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Json string representing an object of the User class</returns>
        /// <response code="200">User object</response>
        /// <response code="404">No User with this UserId</response>
        [HttpGet]
        [Route("User/Get/{userId}")]
        [RequireUserRole("ADMIN")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Get(int userId)
        {
            UserDTO user = _userService.Get(userId);

            if (user == null)
            {
                return BadRequest(new ResponseViewModel("Not found"));
            }

            return Ok(user);
        }

        /// <summary>
        /// Gets all users that abide by the filter from the database
        /// </summary>
        /// <param name="userListFilterViewModel">An object containing information about the filter</param>
        /// <returns>Object of the JsonResult class representing a list of Users in the JSON format</returns>
        /// <remarks>
        /// <h2>Nullable:</h2>
        ///    "email", "userStatus", "roleName", "sortOrder" <br /><br />
        /// <h2>Filtring:</h2>
        ///    <h3>Contains:</h3> "email" <br />
        ///    <h3>Equals:</h3> "userStatus" or "roleName" <br /><br />
        /// <h2>Sorting:</h2>
        ///     <h3>Possible keys:</h3> "Email", "UserStatus", "RoleName" <br />
        ///     <h3>Value:</h3> "DESC" - sort the result in descending order <br />
        ///                      Another value - sort the result in ascending order <br />
        ///
        /// </remarks>
        /// <response code="200">List of Users</response>
        [HttpPost]
        [Route("User/GetList")]
        [RequireUserRole("ADMIN")]
        [ProducesResponseType(typeof(UserListing), StatusCodes.Status200OK)]
        public IActionResult GetList(UserListFilterViewModel userListFilterViewModel)
        {
            UserFiltringDTO userFiltringDTO = new UserFiltringDTO(userListFilterViewModel.Email, userListFilterViewModel.UserStatus, userListFilterViewModel.RoleName);

            var result = _userService.GetUsers(userListFilterViewModel.Paging, userListFilterViewModel.SortOrder, userFiltringDTO);

            return Ok(result);
        }

        /// <summary>
        /// Returns 5 recruiters which email contain a string passed as an argument
        /// </summary>
        [HttpPost]
        [Route("User/GetRecruiters")]
        [Authorize(Policy = "RecruiterRequirment")]
        [ProducesResponseType(typeof(UserListing), StatusCodes.Status200OK)]
        public IActionResult GetRecruiters(string? email)
        {
            var result = _userService.GetRecruiters(email);

            return Ok(result);
        }

        /// <summary>
        /// Updates information about a user represented by an id
        /// </summary>
        /// <param name="userId" example="1">Id of a user</param>
        /// <param name="editUser">Id of a user</param>
        /// <returns>User object eddited</returns>
        /// <response code="200">User edited</response>
        /// <response code="404">No user with this UserId</response>
        [HttpPost]
        [Route("User/Edit/{userId}")]
        [RequireUserRole("ADMIN")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Edit(int userId, EditUserViewModel editUser)
        {
            UserEditDTO editUserDTO =
                new UserEditDTO(
                    userId,
                    editUser.UserStatus,
                    editUser.RoleName);

            int result = _userService.Update(editUserDTO);

            if (result == 0)
            {
                return NotFound(new ResponseViewModel("No user with this UserId"));
            }

            return Ok(new ResponseViewModel("Editing was successful"));
        }

        /// <summary>
        /// Deletes a user represented by an id
        /// </summary>
        /// <param name="userId">Id of a user</param>
        /// <returns>User object deleted</returns>
        /// <response code="200">User deleted successfully</response>
        /// <response code="400">Error while deleting the user</response>
        /// <response code="404">No user with this user id</response>

        [HttpDelete]
        [Route("User/Delete/{userId}")]
        [RequireUserRole("ADMIN")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int userId)
        {
            int loginUserId = GetUserId();

            int result = _userService.Delete(userId, loginUserId);

            if (result == 0)
            {
                return NotFound(new ResponseViewModel("No user with this user id"));
            }
            if (result == -1)
            {
                return BadRequest(new ResponseViewModel("Error while deleting the user"));
            }

            return Ok(new ResponseViewModel("User deleted successfully"));
        }
    }
}