using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.VKCommands.PersonalCommands.CardCommands
{
    public abstract class CardCommand : VkCommand
    {
        protected readonly IUserService userService;
        protected readonly ICharacterRepository characterRepository;
        protected readonly IGameSessionRepository gameSessionRepository;

        protected CardCommand(IVkApi vkApi, IUserService userService, ICharacterRepository characterRepository,IGameSessionRepository gameSessionRepository) : base(vkApi)
        {
            this.userService = userService;
            this.characterRepository = characterRepository;
            this.gameSessionRepository = gameSessionRepository;
        }
        /// <summary>
        /// Get character from characterRepository. If user play in two or more games and not configured or not playing notify user about error. 
        /// </summary>
        /// <returns>If success return character else null</returns>
        protected async virtual Task<(Character, Conversation)?> ValidateCardRequest(long userId)
        {
            var conversation = await userService.GetUserGame(userId);
            if (conversation == null)
            {
                await SendVkMessage("Вы не в игре или состоите в нескольких играх, настройте конфигурацию! ", userId);
                return null;
            }
            var characterId = conversation.Users.First(c => c.UserId == userId).CharacterId;
            if(!await ValidateInGameSession(conversation.GameSessionId, characterId, userId))
            {
                return null;
            }
            var character = await characterRepository.GetCharacter(characterId);
            return (character, conversation);

        }
        private async Task<bool> ValidateInGameSession(GameSessionId gameSessionId, CharacterId characterId,long userId)
        {
            var gameSession = await gameSessionRepository.GetGameSession(gameSessionId);
            if (gameSession.GameState != GameState.Started)
            {
                await SendVkMessage("Игра еще не началась или уже закончилась чтобы использовать карты!", userId);
                return false;
            }
            var isKicked = gameSession.Characters.FirstOrDefault(c => c.Id == characterId)?.IsKicked;
            if(isKicked == true)
            {
                await SendVkMessage("Персонаж исключен!", userId);
                return false;
            }
            return true;
        }
    }
}
