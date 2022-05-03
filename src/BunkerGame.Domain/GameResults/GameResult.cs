using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameResults
{
    public class GameResult
    {
        /// <summary>
        /// Id must be like gameSessionId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="conversationName"></param>
        public GameResult(long id, string conversationName)
        {
            Id = id;
            ConversationName = conversationName;
        }
        public GameResult()
        {
            ConversationName = "Unknown";
        }
        public long Id { get; set; }
        public long GamesCount { get { return WinGames + LostGames; } }
        [MaxLength(256)]
        public string ConversationName { get; set; }
        public long WinGames { get; set; }
        public long LostGames { get; set; }
    }
}
