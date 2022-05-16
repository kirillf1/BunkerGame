using BunkerGame.Domain.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard
{
    public abstract class UseCardCommandHandler
    {
        protected readonly ICharacterRepository characterRepository;

        public UseCardCommandHandler(ICharacterRepository characterRepository)
        {
            this.characterRepository = characterRepository;
        }
        /// <summary>
        /// Changes card state on used. If character taked from characterRepository save changes
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        protected virtual async Task UseCard(Character character, byte cardNumber)
        {
            var usedCard = character.UsedCards.FirstOrDefault(c => c.CardNumber == cardNumber);
            if (usedCard == null)
                throw new InvalidOperationException($"Card №{cardNumber} don't exist");
            usedCard.ChangeCardUsage(true);
            await characterRepository.CommitChanges();
        }
        /// <summary>
        /// Takes character from characterRepository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task<Character> GetCharacter(int id)
        {
            return await characterRepository.GetCharacterById(id) ?? throw new ArgumentNullException(nameof(Character));
        }
        
    }
}
