using BunkerGame.Application.GameSessions.ChangeBunker;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
{
    public class BunkerChangeCommand : VkCommand
    {
      
        private readonly IMediator mediator;

        public BunkerChangeCommand(IVkApi vkApi, IMediator mediator) : base(vkApi)
        {
            this.mediator = mediator;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId.GetValueOrDefault();
            try
            {
                var bunker = await mediator.Send(new ChangeBunkerCommand(peerId));
                await SendVkMessage(GameComponentsConventer.ConvertBunker(bunker), peerId);
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
