using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions.ExternalSurroundings;

namespace BunkerGame.Domain.GameSessions
{
    public class GameSession
    {
        public const int MinCharactersInGame = 6;
        public const int MaxCharactersInGame = 12; 
#pragma warning disable CS8618
        private GameSession()
#pragma warning restore CS8618 
        {

        }
        public GameSession(string gameName, Bunker bunker, Catastrophe catastrophe, List<Character> characters)
        {
            if (characters.Count != 0 && (characters.Count > MaxCharactersInGame || characters.Count < MinCharactersInGame))
            {
                throw new ArgumentOutOfRangeException("Characters must be less than 13. Maybe need clear characters from " + nameof(GameSession));
            }
            Bunker = bunker;
            Catastrophe = catastrophe;
            GameState = GameState.Preparation;
            Characters = characters;
            GameName = gameName;
            externalSurroundings = new List<ExternalSurrounding>();
        }
        public GameSession(long id, string gameName, Bunker bunker, Catastrophe catastrophe, List<Character> characters)
        {
            if(characters.Count != 0 && ( characters.Count > MaxCharactersInGame || characters.Count< MinCharactersInGame))
            {
                throw new ArgumentOutOfRangeException("Characters must be less than 13. Maybe need clear characters from " + nameof(GameSession));
            }
            Bunker = bunker;
            RefreshFreePlaceSize(bunker.BunkerSize.Value);
            Catastrophe = catastrophe;
            GameState = GameState.Preparation;
            Characters = characters;
            Id = id;
            GameName = gameName;
            externalSurroundings = new List<ExternalSurrounding>();
        }
        public byte FreePlaceSize { get; private set; }
        public byte ChangedPlaceSize { get; private set; }
        public string GameName { get; private set; }
        public long Id { get; private set; }
        public Catastrophe Catastrophe { get; private set; }
        public List<Character> Characters { get; set; }
        //public int CatastropheId { get; set; }
        public GameState GameState { get; private set; }
        public Bunker Bunker { get; private set; }
        public Difficulty Difficulty { get; private set; } = Difficulty.Easy;
        public IReadOnlyCollection<ExternalSurrounding> ExternalSurroundings { get => externalSurroundings; }
        private List<ExternalSurrounding> externalSurroundings;

        public void UpdateDifficulty(Difficulty difficulty)
        {
            Difficulty = difficulty;
        }
        public async Task<ResultGameReport> EndGame(IGameResultCounter resultCounter)
        {
            GameState = GameState.Ended;
            return await resultCounter.CalculateResult();
        }
        public void ClearCharacters()
        {
            Characters.Clear();
        }
        
        public void RemoveCharacterFromGame(Character character)
        {
            var characterForDelelete = Characters.FirstOrDefault(c => c.Id == character.Id);
            if (characterForDelelete != null)
                Characters.Remove(characterForDelelete);
        }
        public void AddFreePlace()
        {
            FreePlaceSize++;
            ChangedPlaceSize++;
        }
        public void RemoveFreePlace()
        {
            if (FreePlaceSize > 1)
                FreePlaceSize--;
            ChangedPlaceSize--;
        }
        public void RefreshFreePlaceSize(double bunkerSize)
        {
            FreePlaceSize = bunkerSize switch
            {
                double size when size > 400 => 4,
                _ => 3
            };
            FreePlaceSize += ChangedPlaceSize;
        }
        public void AddCharactersInGame(IEnumerable<Character> characters)
        {
            var totalCharactersCount = characters.Count() + Characters.Count;
            if (totalCharactersCount > MaxCharactersInGame || totalCharactersCount < MinCharactersInGame)
                throw new ArgumentOutOfRangeException("Characters must be less than 13 or more then 5. Maybe need clear characters from " + nameof(GameSession));
            foreach (var character in characters)
            {
                Characters.Add(character);
                character.RegisterCharacterInGame((byte)Characters.Count, Id);
            }
        }

        public void ChangeGameName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть неопределенным или пустым.", nameof(name));
            }

            GameName = name;
        }
        public void UpdateBunker(Bunker bunker)
        {
            Bunker = bunker ?? throw new ArgumentNullException();
            Bunker.RegisterBunkerInGame(Id);
            RefreshFreePlaceSize(bunker.BunkerSize.Value);

        }
        public void UpdateСatastrophe(Catastrophe catastrophe)
        {
            Catastrophe = catastrophe ?? throw new ArgumentNullException();
        }
        public void AddSurrounding(ExternalSurrounding externalSurrounding)
        {
            if (externalSurrounding is null)
            {
                throw new ArgumentNullException(nameof(externalSurrounding));
            }

            externalSurroundings.Add(externalSurrounding);
        }
    }
    public enum GameState
    {
        Preparation,
        Started,
        Ended

    }
    public enum Difficulty
    {
        Easy = 0,
        Medium,
        Hard
    }
}

