using BunkerGame.Domain.GameSessions;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
{
    public class CharacterSizeCommand : VkCommand
    {
        private readonly IGameSessionRepository gameSessionRepository;

        public CharacterSizeCommand(IVkApi vkApi, IGameSessionRepository gameSessionRepository) : base(vkApi)
        {
            this.gameSessionRepository = gameSessionRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId!.Value;
            var gameSession = await gameSessionRepository.GetGameSessionWithBunker(peerId);
            if (gameSession == null)
            {
                await SendVkMessage("Создайте игру!", peerId, VkKeyboardFactory.BuildConversationButtons(false));
                return true;
            }
            await SendVkMessage($"Вместимость бункера: {gameSession.FreePlaceSize}",peerId);
            return true;
        }
    }
}
