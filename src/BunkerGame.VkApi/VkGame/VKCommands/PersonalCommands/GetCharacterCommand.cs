using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.PersonalCommands
{
    public class GetCharacterCommand : VkCommand
    {

        private readonly IUserService userService;

        public GetCharacterCommand(IVkApi vkApi, IUserService userService) : base(vkApi)
        {
            this.userService = userService;
        }
        public override async Task<bool> SendAsync(Message message)
        {
            var userId = message.FromId.GetValueOrDefault();
            if (userId == 0)
                return false;
            await GetCharacter(userId);
            return true;
        }
        private async Task GetCharacter(long userId)
        {
            var conversation = await userService.GetUserGame(userId);
            if (conversation != null)
            {
                var user = conversation.Users.First(c => c.UserId == userId);
                await TryExecuteGetCharacterCommand(user, conversation.GameSessionId);
                return;
            }
            await SendVkMessage("Выберете беседу в которой Вам будет присвоен персонаж (кнопка: выбрать игру)", userId, VkKeyboardFactory.CreatePersonalButtons());
            return;

        }
        private async Task TryExecuteGetCharacterCommand(User user, GameSessionId gameSessionId)
        {
            try
            {
                await userService.HandleCharacterCommand(new Commands.CreateCharacter(user.CharacterId, gameSessionId, user.PlayerId));
            }
            catch
            {
                await SendVkMessage("Все места уже заняты!", user.UserId);
            }
        }
    }
}
