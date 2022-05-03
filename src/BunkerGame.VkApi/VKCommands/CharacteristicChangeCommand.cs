using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Domain.Players;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
{
    public class CharacteristicChangeCommand : VkCommand
    {
        private readonly IUserOptionsService userOptionsService;
        private readonly IMediator mediator;
        private readonly IPlayerRepository playerRepository;

        public CharacteristicChangeCommand(IVkApi vkApi,
            IUserOptionsService userOptionsService,
            IMediator mediator,
            IPlayerRepository playerRepository) : base(vkApi)
        {
            this.userOptionsService = userOptionsService;
            this.mediator = mediator;
            this.playerRepository = playerRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var text = message.Text.ToLower();
            var userId = message.FromId.GetValueOrDefault();
            if (userId == 0)
                return false;
            if (!await userOptionsService.CheckSinglenessGame(userId))
            {
                await SendVkMessage("Вы играете в нескольких беседах, настройте беседу в которой будет изменена характеристика", userId);
            }
            if (text.Contains("характеристики"))
            {
                await SendVkMessage("Выберете характеристику", userId, VkKeyboardFactory.CreateCharacteristicButtoms());
                return true;
            }
            else if (text.Contains("!хар:"))
            {
                text = text.Replace("!хар:", "").TrimStart();
                var characteristicType = GameComponentTypeTextConventer.ConvertTextToCharacteristicTypeRussian(text);
                if (characteristicType == null)
                {
                    await SendVkMessage("Введите название характеристики корректно", userId, VkKeyboardFactory.BuildPersonalButtons());
                    return false;
                }
                var conversation = await userOptionsService.GetUserGame(userId);
                var characterId = await playerRepository.GetCharactersIds(userId, c => c.GameSessionId == conversation!.ConversationId);
                try
                {
                    var character = await mediator.Send(new ChangeCharacteristicCommand(characterId.First(), characteristicType));
                    await SendVkMessage("Ваш персонаж\n" + GameComponentsConventer.ConvertCharacter(character), userId, VkKeyboardFactory.BuildPersonalButtons());
                    return true;
                }
                catch (ArgumentNullException)
                {
                    await SendVkMessage("Не могу найти персонажа", userId, VkKeyboardFactory.BuildPersonalButtons());
                    return false;
                }
            }
            return false;
        }
    }
}
