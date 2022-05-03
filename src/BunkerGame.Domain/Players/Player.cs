using BunkerGame.Domain.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BunkerGame.Domain.Players
{
    public class Player
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private Player()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        {

        }
        public Player(string firstName)
        {
            FirstName = firstName;
            CreationTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Character> Characters { get; set; } = new List<Character>();
        public int WinGames { get; set; }
        public int LoseGames { get; set; }
    }
}
