using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Abstractions;
using VkNet.Abstractions.Authorization;
using VkNet.Abstractions.Category;
using VkNet.Abstractions.Core;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Model.RequestParams.Messages;
using VkNet.Model.Results.Messages;
using VkNet.Utils;
using VkNet.Utils.AntiCaptcha;

namespace BunkerGame.VkApi.IntegrationTests.Infrastructure
{
    public class VkApiMessageContainer : IVkApi
    {
        private readonly MessageBag messageBag;

        public VkApiMessageContainer(MessageBag messageBag)
        {
            this.messageBag = messageBag;
        }
        public IMessagesCategory Messages => messageBag;
        #region notImplemented
        public int RequestsPerSecond { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IBrowser Browser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IAuthorizationFlow AuthorizationFlow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public INeedValidationHandler NeedValidationHandler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IVkApiVersionManager VkApiVersion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Token => throw new NotImplementedException();

        public long? UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsAuthorized => throw new NotImplementedException();

        public IUsersCategory Users => throw new NotImplementedException();

        public IFriendsCategory Friends => throw new NotImplementedException();

        public IStatusCategory Status => throw new NotImplementedException();


        public IGroupsCategory Groups => throw new NotImplementedException();

        public IAudioCategory Audio => throw new NotImplementedException();

        public IDatabaseCategory Database => throw new NotImplementedException();

        public IUtilsCategory Utils => throw new NotImplementedException();

        public IWallCategory Wall => throw new NotImplementedException();

        public IBoardCategory Board => throw new NotImplementedException();

        public IFaveCategory Fave => throw new NotImplementedException();

        public IVideoCategory Video => throw new NotImplementedException();

        public IAccountCategory Account => throw new NotImplementedException();

        public IPhotoCategory Photo => throw new NotImplementedException();

        public IDocsCategory Docs => throw new NotImplementedException();

        public ILikesCategory Likes => throw new NotImplementedException();

        public IPagesCategory Pages => throw new NotImplementedException();

        public IAppsCategory Apps => throw new NotImplementedException();

        public INewsFeedCategory NewsFeed => throw new NotImplementedException();

        public IStatsCategory Stats => throw new NotImplementedException();

        public IGiftsCategory Gifts => throw new NotImplementedException();

        public IMarketsCategory Markets => throw new NotImplementedException();

        public IAuthCategory Auth => throw new NotImplementedException();

        public IExecuteCategory Execute => throw new NotImplementedException();

        public IPollsCategory PollsCategory => throw new NotImplementedException();

        public ISearchCategory Search => throw new NotImplementedException();

        public IStorageCategory Storage => throw new NotImplementedException();

        public IAdsCategory Ads => throw new NotImplementedException();

        public INotificationsCategory Notifications => throw new NotImplementedException();

        public IWidgetsCategory Widgets => throw new NotImplementedException();

        public ILeadsCategory Leads => throw new NotImplementedException();

        public IStreamingCategory Streaming => throw new NotImplementedException();

        public IPlacesCategory Places => throw new NotImplementedException();

        public INotesCategory Notes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IAppWidgetsCategory AppWidgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IOrdersCategory Orders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ISecureCategory Secure { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IStoriesCategory Stories { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ILeadFormsCategory LeadForms { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPrettyCardsCategory PrettyCards { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPodcastsCategory Podcasts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IDonutCategory Donut => throw new NotImplementedException();

        public IDownloadedGamesCategory DownloadedGames => throw new NotImplementedException();

        public ICaptchaSolver CaptchaSolver => throw new NotImplementedException();

        public int MaxCaptchaRecognitionCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTimeOffset? LastInvokeTime => throw new NotImplementedException();

        public TimeSpan? LastInvokeTimeSpan => throw new NotImplementedException();

        public event VkApiDelegate OnTokenExpires;
        public event VkApiDelegate OnTokenUpdatedAutomatically;

        public void Authorize(IApiAuthParams @params)
        {
            throw new NotImplementedException();
        }

        public void Authorize(ApiAuthParams @params)
        {
            throw new NotImplementedException();
        }

        public Task AuthorizeAsync(IApiAuthParams @params)
        {
            throw new NotImplementedException();
        }

        public VkResponse Call(string methodName, VkParameters parameters, bool skipAuthorization = false)
        {
            throw new NotImplementedException();
        }

        public T Call<T>(string methodName, VkParameters parameters, bool skipAuthorization = false, params JsonConverter[] jsonConverters)
        {
            throw new NotImplementedException();
        }

        public Task<VkResponse> CallAsync(string methodName, VkParameters parameters, bool skipAuthorization = false)
        {
            throw new NotImplementedException();
        }

        public Task<T> CallAsync<T>(string methodName, VkParameters parameters, bool skipAuthorization = false)
        {
            throw new NotImplementedException();
        }

        public VkResponse CallLongPoll(string server, VkParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<VkResponse> CallLongPollAsync(string server, VkParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public Language? GetLanguage()
        {
            throw new NotImplementedException();
        }

        public string Invoke(string methodName, IDictionary<string, string> parameters, bool skipAuthorization = false)
        {
            throw new NotImplementedException();
        }

        public Task<string> InvokeAsync(string methodName, IDictionary<string, string> parameters, bool skipAuthorization = false)
        {
            throw new NotImplementedException();
        }

        public string InvokeLongPoll(string server, Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public Task<string> InvokeLongPollAsync(string server, Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public JObject InvokeLongPollExtended(string server, Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public Task<JObject> InvokeLongPollExtendedAsync(string server, Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }

        public Task LogOutAsync()
        {
            throw new NotImplementedException();
        }

        public void RefreshToken(Func<string> code = null)
        {
            throw new NotImplementedException();
        }

        public Task RefreshTokenAsync(Func<string> code = null)
        {
            throw new NotImplementedException();
        }

        public void SetLanguage(Language language)
        {
            throw new NotImplementedException();
        }

        public void Validate(string validateUrl)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    public record VkConversationData(long PeerId, IEnumerable<User> Users, string Name);
    public interface IMessageSendParamsGetter { public IEnumerable<MessagesSendParams> GetParams(); }
    public class MessageBag : IMessagesCategory, IMessageSendParamsGetter
    {
        public Dictionary<long, VkConversationData> Conversations;
        List<MessagesSendParams> _messages;
        public MessageBag(IEnumerable<VkConversationData> vkConversationData)
        {
            _messages = new();
            Conversations = new();
            foreach (var data in vkConversationData)
            {
                Conversations[data.PeerId] = data;
            }
        }
        public IEnumerable<MessagesSendParams> GetParams()
        {
            return _messages;
        }
        public long Send(MessagesSendParams @params)
        {
            _messages.Add(@params);
            return 0;
        }

        public async Task<long> SendAsync(MessagesSendParams @params)
        {
            return await Task.Run(() =>
            {
                _messages.Add(@params);
                return 0;
            });
        }
        public Task<GetConversationMembersResult> GetConversationMembersAsync(long peerId, IEnumerable<string> fields = null, ulong? groupId = null)
        {
            return Task.Run(() => new GetConversationMembersResult()
            {
                Profiles = new ReadOnlyCollection<User>(Conversations[peerId].Users.ToList())
            });

        }
        public Task<ConversationResult> GetConversationsByIdAsync(IEnumerable<long> peerIds, IEnumerable<string> fields = null, bool? extended = null, ulong? groupId = null)
        {
            return Task.Run(() =>
            {
                var conversationResult = new ConversationResult();
                var items = new List<Conversation>();
                foreach (var peerId in peerIds)
                {
                    if (Conversations.TryGetValue(peerId, out var data))
                    {
                        items.Add(new Conversation() { ChatSettings = new ConversationChatSettings() { Title = data.Name } });
                    }
                }
                conversationResult.Items = items;
                return conversationResult;
            });
        }
        #region notImplemented
        public bool AddChatUser(long chatId, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddChatUserAsync(long chatId, long userId)
        {
            throw new NotImplementedException();
        }

        public bool AllowMessagesFromGroup(long groupId, string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AllowMessagesFromGroupAsync(long groupId, string key)
        {
            throw new NotImplementedException();
        }

        public long CreateChat(IEnumerable<ulong> userIds, string title)
        {
            throw new NotImplementedException();
        }

        public Task<long> CreateChatAsync(IEnumerable<ulong> userIds, string title)
        {
            throw new NotImplementedException();
        }

        public IDictionary<ulong, bool> Delete(IEnumerable<ulong> messageIds, bool? spam = null, ulong? groupId = null, bool? deleteForAll = null)
        {
            throw new NotImplementedException();
        }

        public IDictionary<ulong, bool> Delete(IEnumerable<ulong> conversationMessageIds, ulong peerId, bool? spam = null, ulong? groupId = null, bool? deleteForAll = null)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<ulong, bool>> DeleteAsync(IEnumerable<ulong> messageIds, bool? spam = null, ulong? groupId = null, bool? deleteForAll = null)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<ulong, bool>> DeleteAsync(IEnumerable<ulong> conversationMessageIds, ulong peerId, bool? spam = null, ulong? groupId = null, bool? deleteForAll = null)
        {
            throw new NotImplementedException();
        }

        public Chat DeleteChatPhoto(out ulong messageId, ulong chatId, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Chat> DeleteChatPhotoAsync(ulong chatId, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public ulong DeleteConversation(long? userId, long? peerId = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task<ulong> DeleteConversationAsync(long? userId, long? peerId = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public ulong DeleteDialog(long? userId, long? peerId = null, uint? offset = null, uint? count = null)
        {
            throw new NotImplementedException();
        }

        public Task<ulong> DeleteDialogAsync(long? userId, long? peerId = null, uint? offset = null, uint? count = null)
        {
            throw new NotImplementedException();
        }

        public bool DenyMessagesFromGroup(long groupId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DenyMessagesFromGroupAsync(long groupId)
        {
            throw new NotImplementedException();
        }

        public bool Edit(MessageEditParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditAsync(MessageEditParams @params)
        {
            throw new NotImplementedException();
        }

        public bool EditChat(long chatId, string title)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditChatAsync(long chatId, string title)
        {
            throw new NotImplementedException();
        }

        public MessagesGetObject Get(MessagesGetParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<MessagesGetObject> GetAsync(MessagesGetParams @params)
        {
            throw new NotImplementedException();
        }

        public GetByConversationMessageIdResult GetByConversationMessageId(long peerId, IEnumerable<ulong> conversationMessageIds, IEnumerable<string> fields, bool? extended = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task<GetByConversationMessageIdResult> GetByConversationMessageIdAsync(long peerId, IEnumerable<ulong> conversationMessageIds, IEnumerable<string> fields, bool? extended = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public VkCollection<Message> GetById(IEnumerable<ulong> messageIds, IEnumerable<string> fields, ulong? previewLength = null, bool? extended = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task<VkCollection<Message>> GetByIdAsync(IEnumerable<ulong> messageIds, IEnumerable<string> fields, ulong? previewLength = null, bool? extended = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Chat GetChat(long chatId, ProfileFields fields = null, NameCase nameCase = null)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<Chat> GetChat(IEnumerable<long> chatIds, ProfileFields fields = null, NameCase nameCase = null)
        {
            throw new NotImplementedException();
        }

        public Task<Chat> GetChatAsync(long chatId, ProfileFields fields = null, NameCase nameCase = null)
        {
            throw new NotImplementedException();
        }

        public Task<ReadOnlyCollection<Chat>> GetChatAsync(IEnumerable<long> chatIds, ProfileFields fields = null, NameCase nameCase = null)
        {
            throw new NotImplementedException();
        }

        public ChatPreview GetChatPreview(string link, ProfileFields fields)
        {
            throw new NotImplementedException();
        }

        public Task<ChatPreview> GetChatPreviewAsync(string link, ProfileFields fields)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<User> GetChatUsers(IEnumerable<long> chatIds, UsersFields fields, NameCase nameCase)
        {
            throw new NotImplementedException();
        }

        public Task<ReadOnlyCollection<User>> GetChatUsersAsync(IEnumerable<long> chatIds, UsersFields fields, NameCase nameCase)
        {
            throw new NotImplementedException();
        }

        public GetConversationMembersResult GetConversationMembers(long peerId, IEnumerable<string> fields = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public GetConversationsResult GetConversations(GetConversationsParams getConversationsParams)
        {
            throw new NotImplementedException();
        }

        public Task<GetConversationsResult> GetConversationsAsync(GetConversationsParams getConversationsParams)
        {
            throw new NotImplementedException();
        }

        public ConversationResult GetConversationsById(IEnumerable<long> peerIds, IEnumerable<string> fields = null, bool? extended = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }



        public MessagesGetObject GetDialogs(MessagesDialogsGetParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<MessagesGetObject> GetDialogsAsync(MessagesDialogsGetParams @params)
        {
            throw new NotImplementedException();
        }

        public MessageGetHistoryObject GetHistory(MessagesGetHistoryParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<MessageGetHistoryObject> GetHistoryAsync(MessagesGetHistoryParams @params)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<HistoryAttachment> GetHistoryAttachments(MessagesGetHistoryAttachmentsParams @params, out string nextFrom)
        {
            throw new NotImplementedException();
        }

        public Task<ReadOnlyCollection<HistoryAttachment>> GetHistoryAttachmentsAsync(MessagesGetHistoryAttachmentsParams @params)
        {
            throw new NotImplementedException();
        }

        public GetImportantMessagesResult GetImportantMessages(GetImportantMessagesParams getImportantMessagesParams)
        {
            throw new NotImplementedException();
        }

        public Task<GetImportantMessagesResult> GetImportantMessagesAsync(GetImportantMessagesParams getImportantMessagesParams)
        {
            throw new NotImplementedException();
        }

        public GetIntentUsersResult GetIntentUsers(MessagesGetIntentUsersParams getIntentUsersParams)
        {
            throw new NotImplementedException();
        }

        public Task<GetIntentUsersResult> GetIntentUsersAsync(MessagesGetIntentUsersParams getIntentUsersParams, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public string GetInviteLink(ulong peerId, bool reset)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetInviteLinkAsync(ulong peerId, bool reset)
        {
            throw new NotImplementedException();
        }

        public LastActivity GetLastActivity(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<LastActivity> GetLastActivityAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public LongPollHistoryResponse GetLongPollHistory(MessagesGetLongPollHistoryParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<LongPollHistoryResponse> GetLongPollHistoryAsync(MessagesGetLongPollHistoryParams @params)
        {
            throw new NotImplementedException();
        }

        public LongPollServerResponse GetLongPollServer(bool needPts = false, uint lpVersion = 2, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task<LongPollServerResponse> GetLongPollServerAsync(bool needPts = false, uint lpVersion = 2, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public GetRecentCallsResult GetRecentCalls(IEnumerable<string> fields, ulong? count = null, ulong? startMessageId = null, bool? extended = null)
        {
            throw new NotImplementedException();
        }

        public Task<GetRecentCallsResult> GetRecentCallsAsync(IEnumerable<string> fields, ulong? count = null, ulong? startMessageId = null, bool? extended = null)
        {
            throw new NotImplementedException();
        }

        public bool IsMessagesFromGroupAllowed(ulong groupId, ulong userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMessagesFromGroupAllowedAsync(ulong groupId, ulong userId)
        {
            throw new NotImplementedException();
        }

        public long JoinChatByInviteLink(string link)
        {
            throw new NotImplementedException();
        }

        public Task<long> JoinChatByInviteLinkAsync(string link)
        {
            throw new NotImplementedException();
        }

        public bool MarkAsAnsweredConversation(long peerId, bool? answered = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkAsAnsweredConversationAsync(long peerId, bool? answered = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public bool MarkAsAnsweredDialog(long peerId, bool answered = true)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkAsAnsweredDialogAsync(long peerId, bool answered = true)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<long> MarkAsImportant(IEnumerable<long> messageIds, bool important = true)
        {
            throw new NotImplementedException();
        }

        public Task<ReadOnlyCollection<long>> MarkAsImportantAsync(IEnumerable<long> messageIds, bool important = true)
        {
            throw new NotImplementedException();
        }

        public bool MarkAsImportantConversation(long peerId, bool? important = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkAsImportantConversationAsync(long peerId, bool? important = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public bool MarkAsImportantDialog(long peerId, bool important = true)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkAsImportantDialogAsync(long peerId, bool important = true)
        {
            throw new NotImplementedException();
        }

        public bool MarkAsRead(string peerId, long? startMessageId = null, long? groupId = null, bool? markConversationAsRead = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkAsReadAsync(string peerId, long? startMessageId = null, long? groupId = null, bool? markConversationAsRead = null)
        {
            throw new NotImplementedException();
        }

        public bool MarkAsUnreadConversation(long peerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkAsUnreadConversationAsync(long peerId)
        {
            throw new NotImplementedException();
        }

        public PinnedMessage Pin(long peerId, ulong? messageId = null, ulong? conversationMessageId = null)
        {
            throw new NotImplementedException();
        }

        public Task<PinnedMessage> PinAsync(long peerId, ulong? messageId = null, ulong? conversationMessageId = null)
        {
            throw new NotImplementedException();
        }

        public bool RemoveChatUser(ulong chatId, long? userId = null, long? memberId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveChatUserAsync(ulong chatId, long? userId = null, long? memberId = null)
        {
            throw new NotImplementedException();
        }

        public bool Restore(ulong messageId, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RestoreAsync(ulong messageId, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public MessageSearchResult Search(MessagesSearchParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<MessageSearchResult> SearchAsync(MessagesSearchParams @params)
        {
            throw new NotImplementedException();
        }

        public SearchConversationsResult SearchConversations(string q, IEnumerable<string> fields, ulong? count = null, bool? extended = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task<SearchConversationsResult> SearchConversationsAsync(string q, IEnumerable<string> fields, ulong? count = null, bool? extended = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public SearchDialogsResponse SearchDialogs(string query, ProfileFields fields = null, uint? limit = null)
        {
            throw new NotImplementedException();
        }

        public Task<SearchDialogsResponse> SearchDialogsAsync(string query, ProfileFields fields = null, uint? limit = null)
        {
            throw new NotImplementedException();
        }


        public bool SendMessageEventAnswer(string eventId, long userId, long peerId, EventData eventData = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendMessageEventAnswerAsync(string eventId, long userId, long peerId, EventData eventData = null)
        {
            throw new NotImplementedException();
        }

        public long SendSticker(MessagesSendStickerParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<long> SendStickerAsync(MessagesSendStickerParams parameters)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<MessagesSendResult> SendToUserIds(MessagesSendParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<ReadOnlyCollection<MessagesSendResult>> SendToUserIdsAsync(MessagesSendParams @params)
        {
            throw new NotImplementedException();
        }

        public bool SetActivity(string userId, MessageActivityType type, long? peerId = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetActivityAsync(string userId, MessageActivityType type, long? peerId = null, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public long SetChatPhoto(out long messageId, string file)
        {
            throw new NotImplementedException();
        }

        public Task<long> SetChatPhotoAsync(string file)
        {
            throw new NotImplementedException();
        }

        public bool Unpin(long peerId, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnpinAsync(long peerId, ulong? groupId = null)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
