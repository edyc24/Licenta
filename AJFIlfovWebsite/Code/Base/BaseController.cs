using AJFIlfov.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using ControllerDependencies = AJFIlfovWebsite.Code.Base.ControllerDependencies;

namespace AJFIlfovWebsite.WebApp.Code.Base;

public class BaseController : Controller
{
    protected readonly CurrentUserDto CurrentUser;

    public BaseController(ControllerDependencies dependencies)
    {
        CurrentUser = dependencies.CurrentUser;
    }
}