using HeRoBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Data.Entities;

namespace HeRoBackEnd.Controllers
{
    public class UserController : Controller
    {
        public IUserService userService;

        public UserController(IUserService _userService)
        {
            //UserServices userService = new UserServices();
        }

        public IActionResult Index()
        {
            //List<User> users = usersService.GetAllActive();
            
            //return new JsonResult(users);
            return View();
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
