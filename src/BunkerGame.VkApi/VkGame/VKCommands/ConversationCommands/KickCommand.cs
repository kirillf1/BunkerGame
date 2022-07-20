using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands
{
    public class KickCommand : ConversationCommandBase
    {
        private record CharacterWithName(CharacterId CharacterId, string Name);
        private readonly IPlayerRepository playerRepository;
        private readonly IGameSessionRepository gameSessionRepository;
        public KickCommand(IVkApi vkApi, ConversationService conversationService,
            IPlayerRepository playerRepository, IGameSessionRepository gameSessionRepository) : base(vkApi, conversationService)
        {
            this.playerRepository = playerRepository;
            this.gameSessionRepository = gameSessionRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId!.Value;
            var conversation = await IsValidConversation(peerId);
            if (conversation == null)
                return false;
            var text = message.Text.ToLower();
            // todo избавиться от запроса gameSession
            var gameSession = await gameSessionRepository.GetGameSession(conversation.GameSessionId);
            var notKickedCharacters = await GetNoKickedCharacters(gameSession);
            if (text.Contains("исключить персонажей", StringComparison.OrdinalIgnoreCase))
            {
                var keyboard = VkKeyboardFactory.CreateOptionsButtoms(
                    notKickedCharacters.ConvertAll(c => c.Name), "!исключить: ");
                await SendVkMessage("Выберете игрока для исключения", peerId, keyboard);
                await conversationService.AddLastUsedKeyboard(conversation, keyboard);
                return true;

            }
            else if (text.Contains("исключить", StringComparison.OrdinalIgnoreCase))
            {
                text = text.Replace("!исключить: ", "");
                var character = notKickedCharacters.Find(c => c.Name.Contains(text, StringComparison.OrdinalIgnoreCase));
                if (character == null)
                {
                    await SendVkMessage($"Игрока с именем {text} не существует", peerId);
                    return false;
                }
                await conversationService.HandleCommand(new Commands.KickCharacter(gameSession.Id, character.CharacterId));
                return true;
            }
            return false;
        }
        private async Task<List<CharacterWithName>> GetNoKickedCharacters(GameSession gameSession)
        {
            var noKickedIds = gameSession.Characters.Where(c => !c.IsKicked).Select(c => new { c.PlayerId, CharacterId = c.Id });
            var playersIds = noKickedIds.Select(p => p.PlayerId);
            var players = await playerRepository.GetPlayers(0, playersIds.Count(), c => playersIds.Contains(c.Id));
            return players.Join(noKickedIds,
                c => c.Id,
                p => p.PlayerId,
               (p, c) => new CharacterWithName(c.CharacterId, p.FirstName + " " + p.LastName)).ToList();

        }
    }
}
