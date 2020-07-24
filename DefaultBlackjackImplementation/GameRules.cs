using BlackjackSimulationFrame;

namespace DefaultBlackjackImplementation
{
    public class GameRules : IGameRules
    {
        public int NumberOfDecks { get; }
        public int DeckCut { get; }

        public GameRules()
        {
            NumberOfDecks = 6;
            DeckCut = 52;
        }
    }
}