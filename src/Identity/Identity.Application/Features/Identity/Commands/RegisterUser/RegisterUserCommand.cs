﻿namespace Identity.Application.Features.Identity.Commands.RegisterUser;

public record RegisterUserCommand(string UserName, string Email, string Password)
    : ICommand<Unit>;