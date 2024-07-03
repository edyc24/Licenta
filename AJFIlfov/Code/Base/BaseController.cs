using AJFIlfov.Code.Base;
using AJFIlfov.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfov.WebApp.Code.Base
{
    public class BaseController : Controller
    {
        protected readonly CurrentUserDto CurrentUser;

        public BaseController(ControllerDependencies dependencies)
            : base()
        {
            CurrentUser = dependencies.CurrentUser;
        }
    }
}
