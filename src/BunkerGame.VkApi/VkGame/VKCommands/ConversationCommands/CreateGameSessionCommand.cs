using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands
{
    public class CreateGameSessionCommand : VkCommand
    {
        private readonly ConversationService conversationService;
        private readonly IPlayerRepository playerRepository;

        public CreateGameSessionCommand(IVkApi vkApi, ConversationService conversationService, IPlayerRepository playerRepository) : base(vkApi)
        {
            this.conversationService = conversationService;
            this.playerRepository = playerRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId.GetValueOrDefault();
            var userId = message.FromId!.Value;
            if (peerId == 0)
            {
                await SendVkMessage("Данная команда выполняется только в беседе!", message.FromId.GetValueOrDefault(), VkKeyboardFactory.CreatePersonalButtons());
                return true;
            }
            Conversation conversation;
            bool conversationExisted = await conversationService.ConversationExists(peerId);
            if (conversationExisted)
            {
                conversation = await conversationService.GetConversation(peerId);
                await conversationService.RestartGame(peerId);
            }
            else
            {
                conversation = await CreateConversation(peerId, userId);
                await AddNewPlayers(conversation);
            }

            var keyboard = VkKeyboardFactory.CreateConversationButtons(false);
            await SendVkMessage("Теперь можете установить параметры игры и получать персонажей(написать в ЛС боту)", peerId,
                   keyboard);
            await conversationService.AddLastUsedKeyboard(conversation, keyboard);
            return true;
        }
        private async Task<Conversation> CreateConversation(long peerId, long userId)
        {
            var name = await GetConversationName(peerId);
            var creator = await GetCreator(peerId, userId);
            return await conversationService.CreateConversation(peerId, creator, name);
        }
        private async Task<string> GetConversationName(long peerId)
        {
            var conversationResult = await vkApi.Messages.GetConversationsByIdAsync(new List<long> { peerId });
            if (conversationResult == null)
                throw new ArgumentException($"Can't find conversation with id {peerId}");
            return conversationResult.Items.First().ChatSettings.Title;
        }
        private async Task<User> GetCreator(long peerId, long userId)
        {
            var usersResult = await vkApi.Messages.GetConversationMembersAsync(peerId);
            var vkUser = usersResult.Profiles.FirstOrDefault(c => c.Id == userId);
            if (vkUser == null)
                throw new ArgumentNullException(nameof(vkUser));
            var playerId = new PlayerId(Guid.NewGuid());
            var user = new User(userId, playerId);
            await playerRepository.AddPlayer(new Player(playerId, vkUser.FirstName) { LastName = vkUser.LastName });
            return user;
        }
        private async Task AddNewPlayers(Conversation conversation)
        {
            var peerId = conversation.ConversationId;
            var usersResult = await vkApi.Messages.GetConversationMembersAsync(peerId);
            var vkUsers = usersResult.Profiles;
            foreach (var user in vkUsers)
            {
                if (!conversation.Users.Any(c => c.UserId == user.Id))
                {
                    var playerId = new PlayerId(Guid.NewGuid());
                    await playerRepository.AddPlayer(new Player(playerId, user.FirstName) { LastName = user.LastName });
                    var newUser = new User(user.Id, playerId);
                    conversation.AddUser(newUser);
                }
            }
        }
    }
}
