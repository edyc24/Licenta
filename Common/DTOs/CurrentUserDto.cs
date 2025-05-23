﻿namespace AJFIlfov.Common.DTOs;

public class CurrentUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsAuthenticated { get; set; }
    public string Role { get; set; }
}