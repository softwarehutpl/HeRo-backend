using Microsoft.AspNetCore.Mvc;

namespace HeRoBackEnd.Controllers
{
    public class UserController : Controller
    {
        private userService; 

        public UserController()
        {
            UserServices userService = new UserServices();
        }

        public IActionResult Index()
        {
            List<User> users = usersService.GetAllActive();
            return View(users);
        }

        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            User tempUser = userService.Get(id);

            if (tempUser == null)
            {
                return RedirectToAction("Index");
            }

            return View(tempUser);
        }

        public async Task<IActionResult> Create()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewUserView newUser)
        {
            userService.Add(newUser);

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewUserView newUser)
        {
            userService.Update(id, newUser);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            userService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            userService.SignIn(email, password);

            return RedirectToAction("Index");
        }
    }
}
