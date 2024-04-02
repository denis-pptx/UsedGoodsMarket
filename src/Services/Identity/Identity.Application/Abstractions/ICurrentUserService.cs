﻿namespace Identity.Application.Abstractions;

public interface ICurrentUserService
{
    string? UserId { get; }
    Task<User?> GetCurrentUserAsync();
}