namespace BlackjackSimulationFrame
{
    public interface IGameRules
    {
        int NumberOfDecks { get; }
        int DeckCut { get; } // Remaining cards to re-shuffle deck at.
    }
}