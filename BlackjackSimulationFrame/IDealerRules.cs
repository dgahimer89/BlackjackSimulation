using System.Collections.Generic;

namespace BlackjackSimulationFrame
{
    public interface IDealerRules
    {
        public HandOptions ChooseAction(List<char> hand);
    }
}