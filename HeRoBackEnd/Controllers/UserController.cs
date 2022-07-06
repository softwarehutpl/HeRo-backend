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

        [HttpGet]
        public IActionResult Index()
        {
            //List<User> users = usersService.GetAllActive();
            
            //return new JsonResult(users);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? userId)
        {
            if (userId == null)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewUserViewModel newUser)
        {
            //userService.Add(newUser);

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int userId, NewUserViewModel newUser)
        {
            //userService.Update(userId, newUser);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int userId)
        {
            //userService.Delete(userId);

            return RedirectToAction("Index");
        }

        //do zrobienia
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            //userService.SignIn(email, password);

            return RedirectToAction("Index");
        }
    }
}
