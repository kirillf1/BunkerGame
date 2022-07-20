using BunkerGame.Domain.Characters;
using MediatR;

namespace BunkerGame.VkApi.VkGame.Characters.EventHandlers
{
    public class CharacterComponentUpdated : CharacterEventHandlerBase<Events.AdditionalInformationUpdated>,
        INotificationHandler<Events.AgeUpdated>, INotificationHandler<Events.CardsUpdated>,
        INotificationHandler<Events.CharacterItemsUpdated>, INotificationHandler<Events.ChildbearingUpdated>,
        INotificationHandler<Events.HealthUpdated>, INotificationHandler<Events.HobbyUpdated>,
        INotificationHandler<Events.PhobiaUpdated>, INotificationHandler<Events.ProfessionUpdated>,
        INotificationHandler<Events.SexUpdated>, INotificationHandler<Events.SizeUpdated>,
        INotificationHandler<Events.TraitUpdated>
    {
        public CharacterComponentUpdated(VkSenderByCharacter vkSenderByCharacter) : base(vkSenderByCharacter)
        {
        }

        public override async Task Handle(Events.AdditionalInformationUpdated notification, CancellationToken cancellationToken)
        {
            var text = "У Вас новая: " + CharacterComponentStringConventer.ConvertAddInf(notification.AdditionalInformation);
            await Notify(notification.CharacterId, text);
        }

        public async Task Handle(Events.AgeUpdated notification, CancellationToken cancellationToken)
        {
            var text = "У Вас новый: " + CharacterComponentStringConventer.ConvertAge(notification.Age);
            await Notify(notification.CharacterId, text);
        }

        public async Task Handle(Events.CardsUpdated notification, CancellationToken cancellationToken)
        {
            var text = "У Вас новые: " + CharacterComponentStringConventer.ConvertCharacterCards(notification.Cards);
            await Notify(notification.CharacterId, text);
        }

        public async Task Handle(Events.CharacterItemsUpdated notification, CancellationToken cancellationToken)
        {
            var text = "У Вас новые: " + CharacterComponentStringConventer.ConvertCharacterItem(notification.Items);
            await Notify(notification.CharacterId, text);
        }

        public async Task Handle(Events.ChildbearingUpdated notification, CancellationToken cancellationToken)
        {
            var text = "У Вас новое: " + CharacterComponentStringConventer.ConvertChildbearing(notification.Childbearing);
            await Notify(notification.CharacterId, text);
        }

        public async Task Handle(Events.HealthUpdated notification, CancellationToken cancellationToken)
        {
            var text = "У Вас новое: " + CharacterComponentStringConventer.ConvertHealth(notification.Health);
            await Notify(notification.CharacterId, text);
        }

        public async Task Handle(Events.HobbyUpdated notification, CancellationToken cancellationToken)
        {
            var text = "Ваше новое: " + CharacterComponentStringConventer.ConvertHobby(notification.Hobby);
            await Notify(notification.CharacterId, text);
        }

        public async Task Handle(Events.PhobiaUpdated notification, CancellationToken cancellationToken)
        {
            var text = "Фобия изменилась на: " + CharacterComponentStringConventer.ConvertPhobia(notification.Phobia);
            await Notify(notification.CharacterId, text);
        }

        public async Task Handle(Events.ProfessionUpdated notification, CancellationToken cancellationToken)
        {
            var text = "Профессия изменилась на: " + CharacterComponentStringConventer.ConvertProfession(notification.Profession);
            await Notify(notification.CharacterId, text);
        }

        public async Task Handle(Events.SexUpdated notification, CancellationToken cancellationToken)
        {
            var text = "Пол изменился на: " + CharacterComponentStringConventer.ConvertSex(notification.Sex);
            await Notify(notification.CharacterId, text);
        }

        public async Task Handle(Events.SizeUpdated notification, CancellationToken cancellationToken)
        {
            var text = "Телосложение изменилось на: " + CharacterComponentStringConventer.ConvertSize(notification.Size);
            await Notify(notification.CharacterId, text);
        }

        public async Task Handle(Events.TraitUpdated notification, CancellationToken cancellationToken)
        {
            var text = "Черта характера изменилась на: " + CharacterComponentStringConventer.ConvertTrait(notification.Trait);
            await Notify(notification.CharacterId, text);
        }
    }
}
