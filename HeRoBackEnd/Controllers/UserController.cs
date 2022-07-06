using Data.Entities;
using HeRoBackEnd.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using PagedList;
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

        public IActionResult GetList(PagingViewModel pagingViewModel)
        {
            IEnumerable<User> users = userService.GetUsers();

            foreach (KeyValuePair<string, string> sort in pagingViewModel.SortOrder.Sort)
            {
                if (sort.Key == "Email")
                {
                    if (sort.Value == "DESC")
                    {
                        users = users.OrderByDescending(u => u.Email);
                    }
                    else
                    {
                        users = users.OrderBy(s => s.Email);
                    }
                }
                else if (sort.Key == "UserStatus")
                {
                    if (sort.Value == "DESC")
                    {
                        users = users.OrderByDescending(u => u.UserStatus);
                    }
                    else
                    {
                        users = users.OrderBy(s => s.UserStatus);
                    }
                }
                else if (sort.Key == "Role")
                {
                    if (sort.Value == "DESC")
                    {
                        users = users.OrderByDescending(u => u.Role.RoleName);
                    }
                    else
                    {
                        users = users.OrderBy(s => s.Role.RoleName);
                    }
                }
            }

            return new JsonResult(users.ToPagedList(pagingViewModel.Paging.PageNumber, pagingViewModel.Paging.PageSize));
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
