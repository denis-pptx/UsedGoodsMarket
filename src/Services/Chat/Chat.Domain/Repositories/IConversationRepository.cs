﻿using Chat.Domain.Entities;

namespace Chat.Domain.Repositories;

public interface IConversationRepository
{
    Task<IEnumerable<Conversation>> GetByUserIdAsync(string userId, CancellationToken token = default);
}
