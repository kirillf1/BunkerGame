using BunkerGame.Application.Characters.SpyCharacterComponent;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Players;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public abstract class SpiedCharacterComponentNotificationHandlerBase<T> : INotificationHandler<SpiedCharacterComponentNotification<T>>
        where T : CharacterComponent
    {
        protected readonly IVkApi vkApi;
        protected readonly IPlayerRepository playerRepository;

        protected SpiedCharacterComponentNotificationHandlerBase(IVkApi vkApi, IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
            this.vkApi = vkApi;
        }
        public virtual async Task Handle(SpiedCharacterComponentNotification<T> notification, CancellationToken cancellationToken)
        {
            var player = await playerRepository.GetPlayerByCharacterId(notification.CharacterId);
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams($"Характеристика игрока: " +
                $"{Environment.NewLine}" + GetComponentDescription(notification.CharacterComponent),
                notification.GameSessionId, VkKeyboardFactory.BuildConversationButtons(false)));
        }
        public abstract string GetComponentDescription(T component);
    }
    public class SpiedCharacterComponentNotificationHandler<T> : SpiedCharacterComponentNotificationHandlerBase<T>
        where T : CharacterComponent
    {
        public SpiedCharacterComponentNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository) : base(vkApi, playerRepository)
        {
        }

        public override string GetComponentDescription(T component) => component.ToString() ?? "unknown component";
    }
    public class SpiedSexNotificationHandler : SpiedCharacterComponentNotificationHandlerBase<Sex>
    {
        public SpiedSexNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository) : base(vkApi, playerRepository)
        {
        }

        public override string GetComponentDescription(Sex component) => CharacterComponentStringConventer.ConvertSex(component);
    }
    public class SpiedProfessionNotificationHandler : SpiedCharacterComponentNotificationHandlerBase<Profession>
    {
        public SpiedProfessionNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository) : base(vkApi, playerRepository)
        {
        }
        public override string GetComponentDescription(Profession component) =>
            CharacterComponentStringConventer.ConvertProfession(component,0);   
    }
    public class SpiedTraitNotificationHandler : SpiedCharacterComponentNotificationHandlerBase<Trait>
    {
        public SpiedTraitNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository) : base(vkApi, playerRepository)
        {
        }
        public override string GetComponentDescription(Trait component) => CharacterComponentStringConventer.ConvertTrait(component);
    }
    public class SpiedSizeNotificationHandler : SpiedCharacterComponentNotificationHandlerBase<Size>
    {
        public SpiedSizeNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository) : base(vkApi, playerRepository)
        {
        }

        public override string GetComponentDescription(Size component) => CharacterComponentStringConventer.ConvertSize(component);
    }
    public class SpiedHealthNotificationHandler : SpiedCharacterComponentNotificationHandlerBase<Health>
    {
        public SpiedHealthNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository) : base(vkApi, playerRepository)
        {
        }

        public override string GetComponentDescription(Health component) => CharacterComponentStringConventer.ConvertHealth(component);
    }
    public class SpiedHobbyNotificationHandler : SpiedCharacterComponentNotificationHandlerBase<Hobby>
    {
        public SpiedHobbyNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository) : base(vkApi, playerRepository)
        {
        }

        public override string GetComponentDescription(Hobby component) => CharacterComponentStringConventer.ConvertHobby(component,0);
    }
    public class SpiedAgeNotificationHandler : SpiedCharacterComponentNotificationHandlerBase<Age>
    {
        public SpiedAgeNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository) : base(vkApi, playerRepository)
        {
        }

        public override string GetComponentDescription(Age component) => CharacterComponentStringConventer.ConvertAge(component);
    }
    public class SpiedChildbearingNotificationHandler : SpiedCharacterComponentNotificationHandlerBase<Childbearing>
    {
        public SpiedChildbearingNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository) : base(vkApi, playerRepository)
        {
        }

        public override string GetComponentDescription(Childbearing component) => CharacterComponentStringConventer.ConvertChildbearing(component);      
    }
    public class SpiedAdditionalInfNotificationHandler : SpiedCharacterComponentNotificationHandlerBase<AdditionalInformation>
    {
        public SpiedAdditionalInfNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository) : base(vkApi, playerRepository)
        {
        }

        public override string GetComponentDescription(AdditionalInformation component)
            => CharacterComponentStringConventer.ConvertAddInf(component);
    }
}
