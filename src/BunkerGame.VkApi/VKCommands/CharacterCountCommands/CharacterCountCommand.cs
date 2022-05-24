using VkNet.Abstractions;

namespace BunkerGame.VkApi.VKCommands.CharacterCountCommands
{
    public abstract class CharacterCountCommand : VkCommand
    {
        protected readonly IConversationRepository conversationRepository;

        protected CharacterCountCommand(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi)
        {
            this.conversationRepository = conversationRepository;
        }
        protected async Task<Conversation> GetOrCreateConversation(long peerId)
        {
            var conversation = await conversationRepository.GetConversation(peerId);
            if (conversation == null)
            {
                conversation = await Conversation.CreateConversation(vkApi, peerId);
                await conversationRepository.AddConversation(conversation);
            }
            return conversation;
        }  
    }
}
