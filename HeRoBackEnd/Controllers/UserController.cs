using Common.Listing;
using HeRoBackEnd.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.User;
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
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status404NotFound)]
        public IActionResult Get(int userId)
        {
            UserDTO user = _userService.Get(userId);

            if (user == null)
            {
                return NotFound();
            }

            return new JsonResult(user);
        }

        /// <summary>
        /// Gets all users that abide by the filter from the database
        /// </summary>
        /// <param name="userListFilterViewModel">An object containing information about the filter</param>
        /// <returns>Object of the JsonResult class representing a list of Users in the JSON format</returns>
        /// <response code="200">List of Users</response>
        [HttpPost]
        [Route("User/GetList")]
        [ProducesResponseType(typeof(IEnumerable<UserFiltringDTO>), StatusCodes.Status200OK)]
        public IActionResult GetList(UserListFilterViewModel userListFilterViewModel)
        {
            Paging paging = userListFilterViewModel.Paging;
            SortOrder sortOrder = userListFilterViewModel.SortOrder;
            UserFiltringDTO userFiltringDTO = new UserFiltringDTO(userListFilterViewModel.Email, userListFilterViewModel.UserStatus, userListFilterViewModel.RoleName);

            IEnumerable<UserFiltringDTO>? result = _userService.GetUsers(paging, sortOrder, userFiltringDTO);

            return new JsonResult(result);
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
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
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
                return NotFound("No user with this UserId");
            }

            return Ok("Editing was successful");
        }

        /// <summary>
        /// Deletes a user represented by an id
        /// </summary>
        /// <param name="userId">Id of a user</param>
        /// <returns>User object deleted</returns>
        /// <response code="404">No user with this user id</response>
        /// <response code="200">User deleted</response>
        [HttpDelete]
        [Route("User/Delete/{userId}")]
        [Authorize(Policy = "AdminRequirment")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int userId)
        {
            int loginUserId = GetUserId();

            int result = _userService.Delete(userId, loginUserId);

            if (result == 0)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
