﻿using HeRoBackEnd.ViewModels;
using HeRoBackEnd.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.User;
using Services.Services;

namespace HeRoBackEnd.Controllers
{
    public class UserController : Controller
    {
        public IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        public IActionResult GetList(UserListFilterViewModel userListFilterViewModel)
        {
            UserPagingDTO userPagingDTO = new UserPagingDTO(userListFilterViewModel.Paging.PageSize, userListFilterViewModel.Paging.PageNumber);
            UserSortOrderDTO userSortOrderDTO = new UserSortOrderDTO(userListFilterViewModel.SortOrder.Sort);
            UserFiltringDTO userFiltringDTO = new UserFiltringDTO(userListFilterViewModel.Email, userListFilterViewModel.UserStatus, userListFilterViewModel.RoleName);

            return new JsonResult(userService.GetUsers(userPagingDTO, userSortOrderDTO, userFiltringDTO));
        }

        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            //User tempUser = userService.Get(id);

            //if (tempUser == null)
            //{
            //    return RedirectToAction("Index");
            //}

            //return new JsonResult(tempUser);
            return View();
        }

        public async Task<IActionResult> Create()
        {
            //return new JsonResult(new User());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewUserViewModel newUser)
        {
            //userService.Add(newUser);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewUserViewModel newUser)
        {
            //userService.Update(id, newUser);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            //userService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            //userService.SignIn(email, password);

            return RedirectToAction("Index");
        }
    }
}
