﻿using Chat.Domain.Entities;

namespace Chat.Infrastructure.Hubs;

public interface IChatHub
{
    Task ReceiveMessage(Message message);
}