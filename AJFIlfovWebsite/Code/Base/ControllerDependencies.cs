using AJFIlfov.Common.DTOs;

namespace AJFIlfovWebsite.Code.Base;

public class ControllerDependencies
{
    public ControllerDependencies(CurrentUserDto currentUser)
    {
        CurrentUser = currentUser;
    }

    public CurrentUserDto CurrentUser { get; set; }
}