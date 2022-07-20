using BunkerGame.Domain.Characters;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.Characters.CommandHandlers
{
    public class UpdateCharacterFromGeneratorHandler : IRequestHandler<Commands.UpdateSex>,
        IRequestHandler<Commands.UpdateSize>, IRequestHandler<Commands.UpdateChildbearing>,
        IRequestHandler<Commands.UpdateAge>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly IEventStore eventStore;
        private readonly CharacterComponentGenerator componentGenerator;

        public UpdateCharacterFromGeneratorHandler(ICharacterRepository characterRepository, IEventStore eventStore, CharacterComponentGenerator componentGenerator)
        {
            this.characterRepository = characterRepository;
            this.eventStore = eventStore;
            this.componentGenerator = componentGenerator;
        }

        public async Task<Unit> Handle(Commands.UpdateAge request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var age = request.Age;
            var professionExp = character.Profession.Experience;
            if (age == null)
                age = componentGenerator.GenerateAge(professionExp);
            character.UpdateAge(age);
            return await SaveEvents(character);
        }

        public async Task<Unit> Handle(Commands.UpdateSize request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var size = request.Size;
            if (size == null)
                size = componentGenerator.GenerateSize();
            character.UpdateSize(size);
            return await SaveEvents(character);
        }

        public async Task<Unit> Handle(Commands.UpdateSex request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var sex = request.Sex;
            if (sex == null)
                sex = componentGenerator.GenerateSex();
            character.UpdateSex(sex);
            return await SaveEvents(character);
        }

        public async Task<Unit> Handle(Commands.UpdateChildbearing request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var childBearing = request.Childbearing;
            if (childBearing == null)
                childBearing = componentGenerator.GenerateChildbearing();
            character.UpdateChildbearing(childBearing);
            return await SaveEvents(character);
        }
        private async Task<Unit> SaveEvents(Character character)
        {
            await eventStore.Save(character);
            return Unit.Value;
        }
    }
}
