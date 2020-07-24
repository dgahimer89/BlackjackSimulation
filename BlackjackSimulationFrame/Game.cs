using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlackjackSimulationFrame
{
    public class Game
    {
        private static object _writeLock = new object();
        
        public static void Play(IGameRules gameRules, IDealerRules dealerRules, IPlayerRules playerRules)
        {
            var bankroll = 0;
            var wins = 0;
            var push = 0;
            var loss = 0;
            
            var deck = new BlackjackDeck(gameRules.NumberOfDecks, gameRules.DeckCut);
            
            for(var handNum = 0; handNum < 10_000; handNum++)
            {
                var playerHand = new List<char> { deck.DealCard(), deck.DealCard() };
                var dealerHand = new List<char> { deck.DealCard(), deck.DealCard() };

                while (CalculateMinimumHandValue(playerHand) < 22 && playerRules.ChooseAction(playerHand) != HandOptions.Stand)
                {
                    playerHand.Add(deck.DealCard());
                }
                
                while (CalculateMinimumHandValue(dealerHand) < 22 && dealerRules.ChooseAction(dealerHand) != HandOptions.Stand)
                {
                    dealerHand.Add(deck.DealCard());
                }

                bankroll += BankrollChange(playerHand, dealerHand);
                wins += DetermineResult(playerHand, dealerHand) == Result.Win ? 1 : 0;
                push += DetermineResult(playerHand, dealerHand) == Result.Push ? 1 : 0;
                loss += DetermineResult(playerHand, dealerHand) == Result.Loss ? 1 : 0;
                
                deck.ShuffleIfNecessary();
            }

            lock (_writeLock) File.AppendAllLines(@"C:\Temp\output.txt", new [] {$"{(decimal)wins/10000m * 100:F2}\t{(decimal)push/10000m * 100:F2}\t{(decimal)loss/10000m * 100:F2}"});
        }

        private static Result DetermineResult(List<char> playerHand, List<char> dealerHand)
        {
            var playerHandValues = CalculateAllHandValues(playerHand);
            var dealerHandValues = CalculateAllHandValues(dealerHand);

            if (playerHand.Count == 2 && playerHandValues.First() == 21 &&
                !(dealerHand.Count == 2 && dealerHandValues.First() == 21))
            {
                return Result.Win;
            }
            else if (playerHandValues.Min() > 21)
            {
                return Result.Loss;
            }
            else if (dealerHandValues.Min() > 21)
            {
                return Result.Win;
            }
            else if (playerHandValues.Where(x => x < 22).Max() > dealerHandValues.Where(x => x < 22).Max())
            {
                return Result.Win;
            }
            else if (playerHandValues.Where(x => x < 22).Max() < dealerHandValues.Where(x => x < 22).Max())
            {
                return Result.Loss;
            }

            return Result.Push;
        }
        private static int BankrollChange(List<char> playerHand, List<char> dealerHand)
        {
            var bankroll = 0;
            
            var playerHandValues = CalculateAllHandValues(playerHand);
            var dealerHandValues = CalculateAllHandValues(dealerHand);

            if (playerHand.Count == 2 && playerHandValues.First() == 21 &&
                !(dealerHand.Count == 2 && dealerHandValues.First() == 21))
            {
                bankroll += 150;
            }
            else if (playerHandValues.Min() > 21)
            {
                bankroll -= 100;
            }
            else if (dealerHandValues.Min() > 21)
            {
                bankroll += 100;
            }
            else if (playerHandValues.Where(x => x < 22).Max() > dealerHandValues.Where(x => x < 22).Max())
            {
                bankroll += 100;
            }
            else if (playerHandValues.Where(x => x < 22).Max() < dealerHandValues.Where(x => x < 22).Max())
            {
                bankroll -= 100;
            }

            return bankroll;
        }

        private static int CalculateMinimumHandValue(IEnumerable<char> hand)
        {
            var returnValue = 0;

            foreach (var card in hand)
            {
                switch (card)
                {
                    case 'K':
                    case 'Q':
                    case 'J':
                    case '0':
                        returnValue += 10;
                        break;
                    case 'A':
                        returnValue += 1;
                        break;
                    default:
                        returnValue += (byte)card & 15;
                        break;
                }
            }

            return returnValue;
        }

        public static IList<int> CalculateAllHandValues(IEnumerable<char> hand)
        {
            IList<int> returnValue = new List<int> { 0 };

            foreach (var card in hand)
            {
                switch (card)
                {
                    case 'K':
                    case 'Q':
                    case 'J':
                    case '0':
                        returnValue = returnValue.AddValueToAll(10);
                        break;
                    case 'A':
                        var lowAce = returnValue.Select(x => x += 1);
                        var highAce = returnValue.Select(x => x += 11);

                        returnValue = new List<int>();

                        ((List<int>) returnValue).AddRange(lowAce);
                        ((List<int>) returnValue).AddRange(highAce);
                        
                        break;
                    default:
                        returnValue = returnValue.AddValueToAll((byte)card & 15);
                        break;
                }
            }

            return returnValue;
        }
    }
}