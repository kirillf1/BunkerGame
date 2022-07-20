﻿using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame;

namespace BunkerGame.VkApi.Infrastructure.ConversationRepositories
{
    public class ConversationRepositoryInMemory : IConversationRepository
    {
        private List<Conversation> _conversations;
        public ConversationRepositoryInMemory()
        {
            _conversations = new List<Conversation>();
        }
        public Task AddConversation(Conversation conversation)
        {
            if (_conversations.Any(c => c.ConversationId == conversation.ConversationId))
            {
                UpdateConversation(conversation);
                return Task.CompletedTask;
            }
            _conversations.Add(conversation);
            return Task.CompletedTask;
        }

        public Task DeleteConversation(long id)
        {
            var conversation = _conversations.Find(c => c.ConversationId == id);
            if (conversation != null)
                _conversations.Remove(conversation);
            return Task.CompletedTask;
        }

        public Task<Conversation?> GetConversation(string name)
        {
            return Task.Run(() => _conversations.Find(c => c.ConversationName.Contains(name, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<Conversation?> GetConversation(long id)
        {
            return Task.Run(() => _conversations.Find(c => c.ConversationId == id));
        }

        public Task<Conversation?> GetConversation(GameSessionId gameSessionId)
        {
            return Task.Run(() => _conversations.Find(c => c.GameSessionId == gameSessionId));
        }

        public Task<Conversation?> GetConversationByCharacterId(CharacterId characterId)
        {
            return Task.Run(() => _conversations.Find(c => c.Users.Any(c => c.CharacterId == characterId)));
        }

        public Task<IEnumerable<Conversation>> GetConversationsByUserId(long userId)
        {
            return Task.Run(() => _conversations.Where(c => c.Users.Any(c => c.UserId == userId)));
        }

        public Task UpdateConversation(Conversation conversation)
        {
            var conversationUpdate = _conversations.Find(c => c.ConversationId == conversation.ConversationId);
            if (conversationUpdate != null)
            {
                conversationUpdate = conversation;
            }
            return Task.CompletedTask;

        }
    }
}