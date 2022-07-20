using BunkerGame.VkApi.IntegrationTests.Infrastructure;
using BunkerGame.VkApi.VkGame.VkGameServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.VkApi.IntegrationTests.Helpers
{
    public static class VkCommandHelpers
    {
        public static async Task SendCreateGameSessionCommand(this IMessageService messageService,long userId, long peerId)
        {
            const string commandText = "Бот,создать новую игру";
            await messageService.SendMessage(new VkNet.Model.Message() { PeerId = peerId, FromId = userId, Text = commandText });
        }
        public static async Task SendCreateCharactersCommand(this IMessageService messageService,IEnumerable<long> userIds)
        {
            const string commandText = "Получить персонажа";
            foreach (var id in userIds)
            {
               await messageService.SendMessage(new VkNet.Model.Message() { FromId = id, Text = commandText });
            }
        }
        public static async Task SendStartGameCommand(this IMessageService messageService,long userId,long peerId)
        {
            const string commandText = "Бот, стартовать игру!";
            await messageService.SendMessage(new VkNet.Model.Message { FromId = userId, PeerId = peerId, Text = commandText });
        }
        public static async Task SendKickCharacterCommand(this IMessageService messageService, long peerId, string userName)
        {
            const string commandText = "!исключить: ";
            await messageService.SendMessage(new VkNet.Model.Message() { PeerId = peerId, Text = commandText + userName });
        }
        public static async Task CreateStartedGame(this IMessageService messageService, MessageBag messageBag,long peerId)
        {
            var conversation = messageBag.Conversations[peerId];
            var userCreatorId = conversation.Users.First().Id;
            await messageService.SendCreateGameSessionCommand(userCreatorId, peerId);
            await messageService.SendCreateCharactersCommand(conversation.Users.Select(c => c.Id));
            await messageService.SendStartGameCommand(userCreatorId, peerId);
        }
    }
}
