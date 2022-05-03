using BunkerGame.Application.GameSessions.ChangeCatastophe;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
{
    public class CatastropheChangeCommand : VkCommand
    {
        private readonly IMediator mediator;

        public CatastropheChangeCommand(IVkApi vkApi, IMediator mediator) : base(vkApi)
        {
            
            this.mediator = mediator;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId.GetValueOrDefault();
            try
            {
                var catastrophe = await mediator.Send(new ChangeCatastropheCommand(peerId));
                await SendVkMessage(GameComponentsConventer.ConvertCatastrophe(catastrophe), peerId);
                return true;
            }
            catch (ArgumentNullException)
            {
                await SendVkMessage("Ошибка: игра не создана", peerId, VkKeyboardFactory.BuildConversationButtons(false));
                return false;
            }
        }
    }
}
