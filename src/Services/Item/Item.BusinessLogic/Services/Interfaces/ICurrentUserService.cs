﻿namespace Item.BusinessLogic.Services.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Role { get; }
}