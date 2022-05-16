using BunkerGame.Application.GameSessions.EndGame;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
{
    public class EndGameSessionCommand : VkCommand
    {
        private readonly IMediator mediator;
        private readonly IConversationRepository conversationRepository;

        public EndGameSessionCommand(IVkApi vkApi, IMediator mediator, 
            IConversationRepository conversationRepository) : base(vkApi)
        {
            this.mediator = mediator;
            this.conversationRepository = conversationRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId.GetValueOrDefault();
            var result = await mediator.Send(new EndGameCommand(peerId));
            await SendVkMessage(result.GameReport, peerId, VkKeyboardFactory.BuildConversationButtons(false));
            return true;
        }
    }
}
