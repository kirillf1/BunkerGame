using BunkerGame.Domain.GameSessions.Bunkers;

namespace BunkerGame.Domain.GameSessions
{
    public class FreeSeatsSize : Value<FreeSeatsSize>
    {
        private FreeSeatsSize()
        {

        }
        public FreeSeatsSize(int freeSeats, int changedSeats)
        {
            FreeSeats = freeSeats;
            ChangedFreeSeats = changedSeats;
        }
        public FreeSeatsSize(Size bunkerSize)
        {
            FreeSeats = CalculateFreeSeats(bunkerSize);
            ChangedFreeSeats = 0;
        }
        public FreeSeatsSize(Size bunkerSize, int changedFreeSize)
        {
            FreeSeats = CalculateFreeSeats(bunkerSize);
            ChangedFreeSeats = changedFreeSize;
        }
        public int FreeSeats { get; }
        public int ChangedFreeSeats { get; }
        public bool FreeSeatsFilled(int characterCount)
        {
            return FreeSeats + ChangedFreeSeats >= characterCount;
        }
        public FreeSeatsSize Subtract(int count)
        {
            return new FreeSeatsSize(FreeSeats, ChangedFreeSeats - count);
        }
        public FreeSeatsSize Subtract()
        {
            return new FreeSeatsSize(FreeSeats, ChangedFreeSeats - 1);
        }
        public FreeSeatsSize Add()
        {
            return new FreeSeatsSize(FreeSeats, ChangedFreeSeats + 1);
        }
        public FreeSeatsSize Add(int count)
        {
            return new FreeSeatsSize(FreeSeats, ChangedFreeSeats + count);
        }
        public int GetAvailableSeats()
        {
            return FreeSeats + ChangedFreeSeats;
        }
        public static int CalculateFreeSeats(Size bunkerSize)
        {
            return bunkerSize.Value switch
            {
                double size when size > 400 => 4,
                _ => 3
            };
        }
    }
}
