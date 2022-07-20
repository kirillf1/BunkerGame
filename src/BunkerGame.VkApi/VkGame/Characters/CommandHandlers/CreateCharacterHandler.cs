using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Shared;
using BunkerGame.Framework;
using MediatR;
using Commands = BunkerGame.Domain.Characters.Commands;

namespace BunkerGame.VkApi.VkGame.Characters.CommandHandlers
{
    public class CreateCharacterHandler : CharacterCommandHandlerBase<Commands.CreateCharacter>
    {
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly ICharacterFactory characterFactory;
        private readonly VkSenderByCharacter characterSender;

        public CreateCharacterHandler(ICharacterRepository characterRepository, IEventStore eventStore,
            IGameSessionRepository gameSessionRepository, ICharacterFactory characterFactory, VkSenderByCharacter characterSender) : base(characterRepository, eventStore)
        {
            this.gameSessionRepository = gameSessionRepository;
            this.characterFactory = characterFactory;
            this.characterSender = characterSender;
        }

        public override async Task<Unit> Handle(Commands.CreateCharacter request, CancellationToken cancellationToken)
        {
            var characterId = request.CharacterId;
            var gameSessionId = request.GameSessionId;
            var playerId = request.PlayerId;
            var gameSession = await gameSessionRepository.GetGameSession(gameSessionId);
            if (IsCharacterExistsInGame(gameSession, characterId))
            {
                await SendCharacter(characterId);
                return Unit.Value;
            }
            if (gameSession.Characters.Count >= gameSession.CurrentMaxCharactersInGame)
                throw new InvalidOperationException("Too much character in game");
            var character = await characterFactory.CreateCharacter(characterId, playerId, gameSessionId);
            await characterRepository.AddCharacter(character);
            gameSession.AddCharacter(new CharacterGame(characterId, playerId));
            await eventStore.Save(character);
            await eventStore.Save(gameSession);
            return Unit.Value;
        }

        private async Task SendCharacter(CharacterId characterId)
        {
            var character = await characterRepository.GetCharacter(characterId);
            await characterSender.SendCharacter(character);
        }
        private static bool IsCharacterExistsInGame(GameSession gameSession, CharacterId characterId)
        {
            return gameSession.Characters.Any(c => c.Id == characterId);
        }
    }
}
