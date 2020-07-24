using System.Collections.Generic;

namespace BlackjackSimulationFrame
{
    public interface IPlayerRules
    {
        public HandOptions ChooseAction(List<char> hand);
    }
}