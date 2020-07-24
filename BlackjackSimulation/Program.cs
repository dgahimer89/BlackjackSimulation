using System;
using BlackjackSimulationFrame;

namespace BlackjackSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new BlackjackDeck(1, 1);

            foreach (var item in deck.DealDeck)
            {
                Console.WriteLine(item);
            }
        }
    }
}