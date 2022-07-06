using HeRoBackEnd.ViewModels;
using HeRoBackEnd.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.User;
using Services.Services;
using Common.Listing;

namespace HeRoBackEnd.Controllers
{
    public class UserController : Controller
    {
        public UserService userService;

        public UserController(UserService _userService)
        {
            userService = _userService;
        }

        public IActionResult GetList(UserListFilterViewModel userListFilterViewModel)
        {
            Paging paging = userListFilterViewModel.Paging;
            SortOrder sortOrder = userListFilterViewModel.SortOrder;
            UserFiltringDTO userFiltringDTO = new UserFiltringDTO(userListFilterViewModel.Email, userListFilterViewModel.UserStatus, userListFilterViewModel.RoleName);

            var resutl = userService.GetUsers(paging, sortOrder, userFiltringDTO);

            return new JsonResult(resutl);
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
