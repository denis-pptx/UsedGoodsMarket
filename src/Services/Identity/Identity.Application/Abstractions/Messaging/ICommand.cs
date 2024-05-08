﻿using MediatR;

namespace Identity.Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>;