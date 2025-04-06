//using AJFIlfov.BusinessLogic.Implementation.Account;
//using AJFIlfov.BusinessLogic.Implementation.User;
//using AJFIlfov.BusinessLogic.Implementation.User.Models;
//using AJFIlfov.Code.Base;
//using AJFIlfov.WebApp.Code.Base;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace AJFIlfov.Controllers
//{
//   // [Authorize(Roles = "Administrator")]
//    public class UsersController : BaseController
//    {
//        private readonly UsersService Service;

//        public UsersController(ControllerDependencies dependencies, UsersService service)
//           : base(dependencies)
//        {
//            this.Service = service;
//        }

//        public IActionResult Index()
//        {
//            var users = Service.GetAllUsers();
//            return View(users);
//        }

//        public IActionResult Edit(Guid id)
//        {
//            var user = Service.GetUserById(id);
//            return View(user);
//        }

//        [HttpPost]
//        public IActionResult Edit(UserManagmentModel userModel)
//        {
//            Service.UpdateUser(userModel);
//            return RedirectToAction(nameof(Index));
//        }

//        public IActionResult UserMap()
//        {
//            var usersWithAddresses = Service.GetAllUserAddresses();
//            return View(usersWithAddresses);
//        }

//        public IActionResult Details(Guid id)
//        {
//            var currentUserDetails = Service.DisplayProfile(id);
//            return View(currentUserDetails);
//        }
//    }
//}
