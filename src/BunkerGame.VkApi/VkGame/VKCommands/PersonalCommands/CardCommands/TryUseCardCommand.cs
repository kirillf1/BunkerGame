using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame.Characters;
using BunkerGame.VkApi.VkGame.Characters.CommandHandlers;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;
using Commands = BunkerGame.Domain.Characters.Commands;

namespace BunkerGame.VkApi.VkGame.VKCommands.PersonalCommands.CardCommands
{
    public sealed class TryUseCardCommand : CardCommand
    {
        private readonly CharacterService characterService;
        private readonly IPlayerRepository playerRepository;

        public TryUseCardCommand(IVkApi vkApi, IUserService userOptionsService,
            ICharacterRepository characterRepository, CharacterService characterService,
            IPlayerRepository playerRepository, IGameSessionRepository gameSessionRepository) : base(vkApi, userOptionsService, characterRepository, gameSessionRepository)
        {
            this.characterService = characterService;
            this.playerRepository = playerRepository;
        }

        public async override Task<bool> SendAsync(Message message)
        {
            if (!message.FromId.HasValue)
                return false;
            var userId = message.FromId.Value;
            var characterConv = await ValidateCardRequest(userId);
            if (characterConv == null)
                return false;
            var character = characterConv.Value.Item1;
            var messageText = message.Text;
            if (!byte.TryParse(messageText.Replace("использовать карту №", ""), out byte cardNumber) || cardNumber > 3)
            {
                await SendVkMessage("Введите номер карты корректно", userId, VkKeyboardFactory.CreatePersonalButtons());
                return false;
            }
            if (character.Cards.First(c => c.Id.Value == cardNumber).IsUsed)
            {
                await SendVkMessage("Карта уже использована!", userId);
                return true;
            }
            await TryUsecard(character, cardNumber, userId);
            return true;
        }
        private async Task TryUsecard(Character character, byte cardNumber, long userId)
        {
            try
            {
                await characterService.Handle(new Commands.UseCard(character.Id, cardNumber, null));
            }
            catch (NoTargetCharacterExpection)
            {
                var targetNames = await GetUserNames(character.Id, character.GameSessionId);
                if (targetNames.Count == 0)
                {
                    await SendVkMessage("Нет доступных игроков", userId, VkKeyboardFactory.CreatePersonalButtons());
                    return;
                }
                await SendVkMessage("Выберете игрока на которого будет применена карта",
                userId, VkKeyboardFactory.CreateOptionsButtoms(targetNames, "карта на: "));
                await userService.SetOperation(userId, UserOperationType.CardNumber, cardNumber.ToString());
            }
        }
        private async Task<List<string>> GetUserNames(CharacterId characterId, GameSessionId gameSessionId)
        {
            var gameSession = await gameSessionRepository.GetGameSession(gameSessionId);
            var notKickedPlayersId = gameSession.Characters.Where(c => c.Id != characterId && !c.IsKicked).Select(c => c.PlayerId);
            var players = await playerRepository.GetPlayers(0, notKickedPlayersId.Count(), p => notKickedPlayersId.Contains(p.Id));
            return players.Select(p => p.FirstName + " " + p.LastName).ToList();
        }
    }
}
