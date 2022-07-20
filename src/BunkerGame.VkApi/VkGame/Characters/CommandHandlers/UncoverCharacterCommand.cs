using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.Characters.CommandHandlers
{
    public class UncoverCharacterCommand : IRequestHandler<Commands.UncoverAdditionalInformation>,
        IRequestHandler<Commands.UncoverAge>, IRequestHandler<Commands.UncoverCards>,
        IRequestHandler<Commands.UncoverCharacter>, IRequestHandler<Commands.UncoverChildbearing>,
        IRequestHandler<Commands.UncoverHealth>, IRequestHandler<Commands.UncoverHobby>,
        IRequestHandler<Commands.UncoverItems>, IRequestHandler<Commands.UncoverPhobia>,
        IRequestHandler<Commands.UncoverSex>, IRequestHandler<Commands.UncoverSize>,
        IRequestHandler<Commands.UncoverTrait>

    {
        private readonly IVkApi vkApi;
        private readonly IConversationRepository conversationRepository;
        private readonly ICharacterRepository characterRepository;
        private readonly IPlayerRepository playerRepository;

        public UncoverCharacterCommand(IVkApi vkApi, IConversationRepository conversationRepository,
            ICharacterRepository characterRepository, IPlayerRepository playerRepository)
        {
            this.vkApi = vkApi;
            this.conversationRepository = conversationRepository;
            this.characterRepository = characterRepository;
            this.playerRepository = playerRepository;
        }
        public async Task<Unit> Handle(Commands.UncoverAdditionalInformation request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                c => CharacterComponentStringConventer.ConvertAddInf(c.AdditionalInformation));
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UncoverAge request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                c => CharacterComponentStringConventer.ConvertAge(c.Age));
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UncoverCards request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                c => CharacterComponentStringConventer.ConvertCharacterCards(c.Cards));
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UncoverCharacter request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                 c => GameComponentsConventer.ConvertCharacter(c));
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UncoverChildbearing request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                c => CharacterComponentStringConventer.ConvertChildbearing(c.Childbearing));
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UncoverHealth request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                c => CharacterComponentStringConventer.ConvertHealth(c.Health));
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UncoverHobby request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                c => CharacterComponentStringConventer.ConvertHobby(c.Hobby));
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UncoverItems request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                c => CharacterComponentStringConventer.ConvertCharacterItem(c.Items));
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UncoverPhobia request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                c => CharacterComponentStringConventer.ConvertPhobia(c.Phobia));
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UncoverSex request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                c => CharacterComponentStringConventer.ConvertSex(c.Sex));
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UncoverSize request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                c => CharacterComponentStringConventer.ConvertSize(c.Size));
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UncoverTrait request, CancellationToken cancellationToken)
        {
            await SendUncoveredCharacterComponent(request.CharacterId,
                c => CharacterComponentStringConventer.ConvertTrait(c.Trait));
            return Unit.Value;
        }
        private async Task SendUncoveredCharacterComponent(CharacterId characterId, Func<Character, string> uncover)
        {
            var character = await characterRepository.GetCharacter(characterId);
            var conversation = await conversationRepository.GetConversation(character.GameSessionId);
            if (conversation == null)
                return;
            var playerName = await GetPlayerName(character.PlayerId);
            var text = $"Игрок {playerName} раскрыл свою характеристику:{Environment.NewLine}{uncover(character)}";
            await vkApi.SendVKMessage(text, conversation.ConversationId);
        }

        private async Task<string> GetPlayerName(PlayerId playerId)
        {
            var player = await playerRepository.GetPlayer(playerId);
            return player.FirstName + " " + player.LastName;
        }
    }
}
