﻿using Common.Listing;
using HeRoBackEnd.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.User;
using Services.Services;
using System.Security.Claims;

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
        /// <remarks>
        /// Sample Responses:
        ///
        ///     {
        ///        "email": "test@da.com",
        ///        "userStatus": "Active",
        ///        "roleName": "Admin"
        ///     }
        ///
        /// </remarks>
        [HttpGet]
        [Route("User/Get/{userId}")]
        public IActionResult Get(int userId)
        {
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
        /// <remarks>
        /// Sample Responses:
        ///
        ///     [
        ///          {
        ///              "email": "test@da.com",
        ///              "userStatus": "Active",
        ///              "roleName": "Admin"
        ///          },
        ///          {
        ///              "email": "test2@da.com",
        ///              "userStatus": "Active",
        ///              "roleName": "Recruiter"
        ///         },
        ///     ]
        /// </remarks>
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
        /// <returns>IActionResult</returns>
        [HttpDelete]
        [Route("User/Delete/{userId}")]
        [Authorize(Policy = "AdminRequirment")]
        public IActionResult Delete(int userId)
        {
            int loginUserId = GetUserId();

            int result = _userService.Delete(userId, loginUserId);

            if (result == 0)
            {
                return NotFound("No user with this UserId");
            }

            return Ok("Deleting was successful");
        }
    }
}
