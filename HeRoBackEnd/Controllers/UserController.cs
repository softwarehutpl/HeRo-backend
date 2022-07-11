using Common.Listing;
using HeRoBackEnd.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.User;
using Services.Services;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class UserController : Controller
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
        [HttpGet]
        [Route("User/Get/{userId}")]
        public IActionResult Get(int? userId)
        {
            if (userId == null)
            {
                return NotFound("UserId must be a value");
            }

            UserDTO user = _userService.Get(userId);

            if (user == null)
            {
                return NotFound("No user with this UserId");
            }

            return new JsonResult(user);
        }

        /// <summary>
        /// Gets all users that abide by the filter from the database
        /// </summary>
        /// <param name="userListFilterViewModel">An object containing information about the filter</param>
        /// <returns>Object of the JsonResult class representing a list of Users in the JSON format</returns>
        [HttpPost]
        [Route("User/GetList")]
        public IActionResult GetList(UserListFilterViewModel userListFilterViewModel)
        {
            Paging paging = userListFilterViewModel.Paging;
            SortOrder sortOrder = userListFilterViewModel.SortOrder;
            UserFiltringDTO userFiltringDTO = new UserFiltringDTO(userListFilterViewModel.Email, userListFilterViewModel.UserStatus, userListFilterViewModel.RoleName);

            var resutl = _userService.GetUsers(paging, sortOrder, userFiltringDTO);

            return new JsonResult(resutl);
        }

        /// <summary>
        /// Updates information about a user represented by an id
        /// </summary>
        /// <param name="userId">Id of a user</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("User/Edit/{userId}")]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int? userId, EditUserViewModel editUser)
        {
            if (userId == null)
            {
                return NotFound("UserId must be a value");
            }

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
        /// <returns>IActionResult</returns>
        [HttpDelete]
        [Route("User/Delete/{userId}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int userId)
        {
            //userService.Delete(userId);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Logs user in
        /// </summary>
        /// <param name="email">Users email</param>
        /// <param name="password">Users password</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("User/SignIn")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            //userService.SignIn(email, password);

            return RedirectToAction("Index");
        }
    }
}
