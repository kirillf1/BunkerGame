using BunkerGame.Domain.GameResults;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
{
    public class StatisticsCommand : VkCommand
    {
        private readonly IGameResultRepository gameResultRepository;

        public StatisticsCommand(IVkApi vkApi, IGameResultRepository gameResultRepository) : base(vkApi)
        {
            this.gameResultRepository = gameResultRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId.GetValueOrDefault();
            var gameResult = await gameResultRepository.GetGameResult(peerId);
            if(gameResult == null)
            {
                await SendVkMessage("Вы не сыграли ни одной игры", peerId);
                return true;
            }
            await SendVkMessage($"Игр сыграно:{gameResult.GamesCount}. Выиграно игр:{gameResult.WinGames}, проиграно:{gameResult.WinGames}",peerId);
            return true;
        }
    }
}
