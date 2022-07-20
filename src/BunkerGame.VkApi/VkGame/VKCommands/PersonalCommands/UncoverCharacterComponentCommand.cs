using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame.VkGameServices;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.PersonalCommands
{
    public class UncoverCharacterComponentCommand : VkCommand
    {
        private static readonly Dictionary<string,Func<CharacterId,IRequest>> UncoverCommands;
        static UncoverCharacterComponentCommand()
        {
            UncoverCommands = new();
            UncoverCommands["фобия"] = (id) => new Commands.UncoverPhobia(id);
            UncoverCommands["здоровье"] = (id) => new Commands.UncoverHealth(id);
            UncoverCommands["багаж"] = (id) => new Commands.UncoverItems(id);
            UncoverCommands["деторождение"] = (id) => new Commands.UncoverChildbearing(id);
            UncoverCommands["возраст"] = (id) => new Commands.UncoverAge(id);
            UncoverCommands["доп.информация"] = (id) => new Commands.UncoverAdditionalInformation(id);
            UncoverCommands["черта характера"] = (id) => new Commands.UncoverTrait(id);
            UncoverCommands["пол"] = (id) => new Commands.UncoverSex(id);
            UncoverCommands["масса и рост"] = (id) => new Commands.UncoverSize(id);
            UncoverCommands["хобби"] = (id) => new Commands.UncoverHobby(id);
        }
        private readonly IUserService userService;
        public UncoverCharacterComponentCommand(IVkApi vkApi,IUserService userService) : base(vkApi)
        {
            this.userService = userService;
        }

        public async override Task<bool> SendAsync(Message message)
        {
            var userId = message.FromId.Value!;
            var text = message.Text;
            var conversation = await userService.GetUserGame(userId);
            if (conversation == null)
            {
                await SendVkMessage("Выберете беседу в которой Вам будет присвоен персонаж (кнопка: выбрать игру)", userId, VkKeyboardFactory.CreatePersonalButtons());
                return false;
            }
            if(text.Contains("раскрыть характеристики", StringComparison.OrdinalIgnoreCase))
            {
                await SendVkMessage("Выберете характеристику для раскрытия", userId, VkKeyboardFactory.CreateUncoverCharacteristicButtoms());
                return true;
            }
            await UncoverCharacteristic(conversation, userId, text);
            return true;
        }

        private async Task UncoverCharacteristic(Conversation conversation,long userId,string characteristicType)
        {
            var user = conversation.Users.First(c => c.UserId == userId);
            var command = FindCommand(user.CharacterId,characteristicType.Replace("!Хар: ",""));
            if(command == null)
            {
                await vkApi.SendVKMessage("Введите характеристику правильно!", userId);
                return;
            }
            await TryExecuteUncoverCommand(command,userId);
        }
          
        private async Task TryExecuteUncoverCommand(IRequest request,long userId)
        {
            try
            {
                await userService.HandleCharacterCommand(request);
            }
            catch
            {
                await SendVkMessage("Неизвестная ошибка попробуйте позже!", userId);
            }
        }
        private IRequest? FindCommand(CharacterId characterId,string charactersticType)
        {
            var command = UncoverCommands.FirstOrDefault(c => c.Key.Contains(charactersticType, StringComparison.OrdinalIgnoreCase)).Value;
            return command?.Invoke(characterId);
        }
        
    }
}
