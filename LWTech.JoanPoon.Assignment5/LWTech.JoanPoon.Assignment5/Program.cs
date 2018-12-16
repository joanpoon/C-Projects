using System;
using System.Collections.Generic;

namespace LWTech.JoanPoon.Assignment5
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Joan Poon \t\t\t\t Assignment 5");
            Console.WriteLine("Go Fish game simulation with 4 computer players");
            Console.WriteLine("----------------------------------------------------------\n\n");



            Console.WriteLine("Players Introduction: (without any cheaters)");
            Console.WriteLine("Player 1: Donald chooses a random player to ask for the rank of his first card.");
            Console.WriteLine("Player 2: Pooh chooses a random player to ask for a random rank.");
            Console.WriteLine("Player 3: chooses a random player to ask for the rank of his first card.");
            Console.WriteLine("Player 4: Mickey chooses a random player to ask for a random rank.");
            Console.WriteLine("\n----------------------------------------------------------\n\n");


            List<Player> players = new List<Player>
            {
                new PlayerRandomFirst("Donald"),
                new PlayerRandomRandom("Pooh"),
                new PlayerRandomFirst("Joan"),
                new PlayerRandomRandom("Mickey")
            };


            //To test normal game play with cheater that remember player's last requested rank
            /*
            List<Player> players = new List<Player>
            {
                new PlayerRandomFirst("Donald"),
                new PlayerRandomRandom("Pooh"),
                new PlayerRemember("Joan"),
                new PlayerRandomRandom("Mickey")
            };
            */

            //----------------------------------------------------------------------------------------

            //To test normal game play with cheater that ask for a card he/she doesnt have
            /*
            List<Player> players = new List<Player>
            {
                new PlayerRandomFirst("Donald"),
                new PlayerRandomRandom("Pooh"),
                new PlayerCheat("Joan"),
                new PlayerRandomRandom("Mickey")
            };
            */

            Deck deck = new Deck();
            deck.ShuffleCard();
            deck.CutCard();
            int i, j;
            Card cardFromBook;

            for (i = 0; i < players.Count; i++)
                for (j = 0; j < 5; j++)
                    players[i].AddCard(deck.DealCard());


            //display player hand and check if any player already has a book at the same time
            for (i = 0; i < players.Count; i++)
            {
                Console.WriteLine(players[i]);
                cardFromBook = players[i].Hand.HasBook();
                if (cardFromBook != null)
                {
                    Console.WriteLine(players[i].Name + " has a book of " + cardFromBook.Rank);
                    players[i].Hand.RemoveBook(cardFromBook);
                }
            }


            Console.WriteLine();
            Console.WriteLine(players[0].GetScore() + " | " + players[1].GetScore() + " | " + players[2].GetScore() + " | " + players[3].GetScore());
            Console.WriteLine("Remaining cards in deck: " + deck.Size() + "\n\n");

            bool isFinished = false;
            int round = 1;
            int whoseTurn = 0;
            bool samePlayersTurn;

            while (!isFinished)
            {
                Player currentPlayer = players[whoseTurn];
                Player opponent = currentPlayer.ChoosePlayerToAsk(players);
                Rank requestRank = currentPlayer.ChooseRankToAskFor();
                int rankCount = opponent.Hand.GetRankCount(requestRank);
                int drewCard;
                samePlayersTurn = false;
                Card c; 

                Console.WriteLine("_____________________________________________________________________________________");
                Console.WriteLine($"\nRound {round} : {currentPlayer.Name}'s turn.");
                Console.WriteLine("{0} says: {1} ! Give me all your {2} !", currentPlayer.Name, opponent.Name, requestRank);

                if (rankCount > 0)
                {
                    List<Card> takenCards = opponent.Hand.GiveCardsToOpponent(requestRank, currentPlayer);
                    Console.Write(currentPlayer.Name + " took ");
                    for (int i1 = 0; i1 < takenCards.Count; i1++)
                    {
                        Card d = takenCards[i1];
                        Console.Write(d);
                    }

                    Console.WriteLine(" from " + opponent.Name + "\n");

                    if (opponent.IsEmpty())
                    {
                        if (deck.Size() < 1)
                            Console.WriteLine(opponent.Name + "'s hand is empty, oops, no more cards to draw...\n");
                        else
                        {
                            Console.WriteLine(opponent.Name + "'s hand is empty, drawing new cards...\n");
                            drewCard = 0;
                            while (deck.Size() > 0 && drewCard < 5)
                            {
                                c = deck.DealCard();
                                opponent.AddCard(c);
                                Console.WriteLine($"{opponent.Name} drew {c} from the deck.\n");
                                drewCard++;
                            }
                        }
                    }

                    samePlayersTurn = true;
                }
                else
                {
                    Console.WriteLine("{0} says: GO FISH!\n", opponent.Name);
                    samePlayersTurn = false;

                    if (deck.Size() > 0)
                    {
                        c = deck.DealCard();
                        currentPlayer.AddCard(c);
                        Console.WriteLine($"{currentPlayer.Name} drew {c} from the deck.\n");
                    }
                    else
                        Console.WriteLine("There is no card to draw.\n");

                }

                for (i = 0; i < players.Count; i++)
                    Console.WriteLine(players[i]);

                cardFromBook = currentPlayer.Hand.HasBook();
                if (cardFromBook != null)
                {
                    Console.WriteLine(currentPlayer.Name + " has a book of " + cardFromBook.Rank);
                    currentPlayer.Hand.RemoveBook(cardFromBook);
                    currentPlayer.AddScore();

                    if (currentPlayer.IsEmpty())
                    {
                        if (deck.Size() < 1)
                        {
                            Console.WriteLine(currentPlayer + "'s hand is empty, oops, no more cards to draw...\n");
                            samePlayersTurn = false;
                        }
                           
                        else
                        {
                            Console.WriteLine(currentPlayer + "'s hand is empty, drawing new cards...\n");
                            drewCard = 0;

                            while (deck.Size() > 0 && drewCard < 5)
                            {
                                c = deck.DealCard();
                                currentPlayer.AddCard(c);
                                Console.WriteLine($"{currentPlayer.Name} drew {c} from the deck.\n");
                                drewCard++;
                            }
                        }
                    }
                }



                if (GetTotalScore(players) == 13)
                    isFinished = true;
                else
                {
                    if (samePlayersTurn)
                    {
                        //if current player has no more card, switch to next player
                        while (players[whoseTurn].IsEmpty())
                        {
                            if (whoseTurn < 3)
                                whoseTurn++;
                            else
                                whoseTurn = 0;
                        }

                    }
                    else
                    {
                        //next player's turn
                        do
                        {
                            if (whoseTurn < 3)
                                whoseTurn++;
                            else
                                whoseTurn = 0;
                        } while (players[whoseTurn].IsEmpty());
                    }
                }


                Console.WriteLine();
                Console.WriteLine("Scores | " + players[0].GetScore() + " | " + players[1].GetScore() + " | " + players[2].GetScore() + " | " + players[3].GetScore());
                Console.WriteLine("Remaining cards in deck: " + deck.Size() + "\n");

                round++;
            }

            Console.WriteLine("_____________________________________________________________________________________");
            Console.WriteLine("\nGame has finally ended after {0} rounds!", round - 1);

            int highestScore = GetHighestScore(players);
            for (i = 0; i < players.Count; i++)
            {
                if (players[i].Score == highestScore)
                    Console.WriteLine($"\nCongratulation! {players[i].Name} won with score of {highestScore} ! \n");
            }

        }

        private static int GetTotalScore(List<Player> players)
        {
            int sum = 0;
            foreach (Player p in players)
                sum += p.Score;
            return sum;
        }

        private static int GetHighestScore(List<Player> players)
        {
            int highest = 0;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Score > highest)
                    highest = players[i].Score;
            }
            return highest;
        }

    }

}
 