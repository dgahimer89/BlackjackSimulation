using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulationFrame
{
    public class BlackjackDeck
    {
        private List<char> _dealDeck;
        private List<char> _discardDeck;

        private static readonly char[] CardPerSuit = new []{'A', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'J', 'Q', 'K'};
        
        public int NumberOfDecks { get; }
        public int DeckCut { get; } // Remaining cards to re-shuffle deck at.
        public IReadOnlyList<char> DealDeck => _dealDeck;
        public IReadOnlyList<char> DiscardDeck => _discardDeck;

        public BlackjackDeck(int numberOfDecks, int deckCut)
        {
            NumberOfDecks = numberOfDecks;
            DeckCut = deckCut;

            _dealDeck = new List<char>();
            _discardDeck = new List<char>();

            for (var i = 0; i < numberOfDecks; i++)
            {
                for (var k = 0; k < 4; k++)
                {
                    _dealDeck.AddRange(CardPerSuit);
                }
            }

            Shuffle();
        }

        protected void Shuffle()
        {
            for (var i = _dealDeck.Count - 1; i >= 0; i--)
            {
                var swapIndex = RandomGen.Next((i));

                var temp = _dealDeck[swapIndex];
                _dealDeck[swapIndex] = _dealDeck[i];
                _dealDeck[i] = temp;
            }
        }

        protected char DealCard()
        {
            var returnValue = _dealDeck.First();

            _dealDeck.RemoveAt(0);
            _discardDeck.Add(returnValue);

            return returnValue;
        }
    }
}