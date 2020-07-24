namespace BlackjackSimulationFrame
{
    public interface IGameRules
    {
        int NumberOfHands { get; }
        int DeckCut { get; } // Remaining cards to re-shuffle deck at.
    }
}