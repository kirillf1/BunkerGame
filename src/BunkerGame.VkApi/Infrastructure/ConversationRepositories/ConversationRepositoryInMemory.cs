﻿using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame;
using System.Collections.Concurrent;

namespace BunkerGame.VkApi.Infrastructure.ConversationRepositories
{
    public class ConversationRepositoryInMemory : IConversationRepository
    {
        private ConcurrentDictionary<long, Conversation> _conversations;
        public ConversationRepositoryInMemory()
        {
            _conversations = new ConcurrentDictionary<long, Conversation>();

        }
        public Task AddConversation(Conversation conversation)
        {
            _conversations.TryAdd(conversation.ConversationId, conversation);
            return Task.CompletedTask;
        }

        public Task DeleteConversation(long id)
        {
            _conversations.TryRemove(id, out _);
            return Task.CompletedTask;
        }

        public Task<Conversation?> GetConversation(string name)
        {
            return Task.FromResult(_conversations.Select(c => c.Value).FirstOrDefault(c => c.ConversationName.Contains(name, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<Conversation?> GetConversation(long id)
        {
            _conversations.TryGetValue(id, out var conversation);
            return Task.FromResult(conversation);
        }

        public Task<Conversation?> GetConversation(GameSessionId gameSessionId)
        {
            return Task.FromResult(_conversations.Select(c => c.Value).FirstOrDefault(c => c.GameSessionId == gameSessionId));
        }

        public Task<Conversation?> GetConversationByCharacterId(CharacterId characterId)
        {
            return Task.FromResult(_conversations.Select(c => c.Value).FirstOrDefault(c => c.Users.Any(c => c.CharacterId == characterId)));
        }

        public Task<IEnumerable<Conversation>> GetConversationsByUserId(long userId)
        {
            return Task.FromResult(_conversations.Select(c => c.Value).Where(c => c.Users.Any(c => c.UserId == userId)));
        }

        public Task UpdateConversation(Conversation conversation)
        {
            return Task.CompletedTask;
        }
    }
}
