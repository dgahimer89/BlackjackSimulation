using System.Collections.Generic;
using System.Linq;
using BlackjackSimulationFrame;

namespace DefaultBlackjackImplementation
{
    public class PlayerRules : IPlayerRules
    {
        public HandOptions ChooseAction(List<char> hand)
        {
            var values = Game.CalculateAllHandValues(hand);

            if (values.Any(x => x >= 17 && x <= 21) || values.All(x => x > 21))
                return HandOptions.Stand;

            return HandOptions.Hit;
        }
    }
}