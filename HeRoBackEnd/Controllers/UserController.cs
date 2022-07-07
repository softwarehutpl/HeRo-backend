using HeRoBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Data.Entities;
using HeRoBackEnd.ViewModels.User;

namespace HeRoBackEnd.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        public IUserService userService;

        public UserController(IUserService _userService)
        {
            //UserServices userService = new UserServices();
        }

        /// <summary>
        /// Gets all users from the database
        /// </summary>
        /// <returns>Json string representing a list of Users</returns>
        [HttpGet]
        [Route("User/Index")]
        public IActionResult Index()
        {
            //List<User> users = usersService.GetAllActive();
            
            //return new JsonResult(users);
            return View();
        }

        /// <summary>
        /// Gets a user specified by an id
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Json string representing an object of the User class</returns>
        [HttpGet]
        [Route("User/Get/{userId}")]
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

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="newUser">Object containing information about a new user</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("User/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewUserViewModel newUser)
        {
            //userService.Add(newUser);

            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Updates information about a user represented by an id
        /// </summary>
        /// <param name="userId">Id of a user</param>
        /// <param name="newUser">Object containing new information about a user</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("User/Edit/{userId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int userId, NewUserViewModel newUser)
        {
            //userService.Update(userId, newUser);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes a user represented by an id
        /// </summary>
        /// <param name="userId">Id of a user</param>
        /// <returns>IActionResult</returns>
        [HttpDelete]
        [Route("User/Delete/{userId}")]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            //userService.SignIn(email, password);

            return RedirectToAction("Index");
        }
    }
}
