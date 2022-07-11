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
        public UserService userService;

        public UserController(UserService _userService)
        {
            userService = _userService;
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
                return NotFound();
            }

            UserDTO user = userService.Get(userId);

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
        [HttpPost]
        [Route("User/GetList")]
        public IActionResult GetList(UserListFilterViewModel userListFilterViewModel)
        {
            Paging paging = userListFilterViewModel.Paging;
            SortOrder sortOrder = userListFilterViewModel.SortOrder;
            UserFiltringDTO userFiltringDTO = new UserFiltringDTO(null, userListFilterViewModel.Email, userListFilterViewModel.UserStatus, userListFilterViewModel.RoleName);

            var resutl = userService.GetUsers(paging, sortOrder, userFiltringDTO);

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
                return NotFound();
            }

            UserEditDTO editUserDTO =
                new UserEditDTO(
                    userId,
                    editUser.Email,
                    editUser.UserStatus,
                    editUser.RoleName);

            int result = userService.Update(editUserDTO);

            if (result != 0)
            {
                return NotFound();
            }

            return Ok();
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
