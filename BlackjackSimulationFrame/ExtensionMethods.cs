using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulationFrame
{
    public static class ExtensionMethods
    {
        public static IList<int> AddValueToAll(this IList<int> list, int value)
        {
            return list.Select(x => x + value).ToList();
        }
    }
}