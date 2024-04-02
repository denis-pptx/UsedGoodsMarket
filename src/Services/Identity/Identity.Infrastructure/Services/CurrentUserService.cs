﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor _httpContextAccessor, UserManager<User> _userManager) 
    : ICurrentUserService
{
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
