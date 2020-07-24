using System.Threading;
using BlackjackSimulationFrame;
using DefaultBlackjackImplementation;

namespace BlackjackSimulation
{
    static class Program
    {
        static void Main(string[] args)
        {
            var gameRules = new GameRules();
            var playerRules = new PlayerRules();
            var dealerRules = new DealerRules();

            int runs = 100_000;

            using (var countdown = new CountdownEvent(runs))
            {
                for (var i = 0; i < runs; i++)
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        RunGame();
                        countdown.Signal();
                    });

                countdown.Wait();
            }

            //Game.Play(gameRules, dealerRules, playerRules);
        }

        static void RunGame()
        {
            var gameRules = new GameRules();
            var dealerRules = new DealerRules();
            var playerRules = new PlayerRules();
            
            Game.Play(gameRules, dealerRules, playerRules);
        }
    }
}