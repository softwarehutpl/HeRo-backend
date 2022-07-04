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
        public async Task<IActionResult> Create(User newUser)
        {
            userService.Add(newUser);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
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

        public async Task<IActionResult> Edit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User newUser)
        {
            userService.Update(id, newUser);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, User user)
        {
            userService.Delete(id, user);

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
