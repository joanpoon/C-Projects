using System;

namespace LWTech.JoanPoon.Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Joan Poon \t\t\t\t Assignment 4");
            Console.WriteLine("Go Fish game simulation with 4 computer players");
            Console.WriteLine("----------------------------------------------------------\n\n");

            /*
            Console.WriteLine("Players Introduction:");
            Console.WriteLine("Player 1: Donald chooses a random player to ask for the rank of his first card.");
            Console.WriteLine("Player 2: Pooh chooses a random player to ask for a random rank.");
            Console.WriteLine("Player 3: Joan chooses a random player to ask for a rank the opponent has, but he doesnt have.");
            Console.WriteLine("Player 4: Mickey chooses a random player to ask for a rank the opponent requested during their turn.");
            Console.WriteLine("\n----------------------------------------------------------\n\n");

            Player[] players = { new PlayerRandomFirst("Donald"), new PlayerRandomRandom("Pooh"), new PlayerRemember("Joan"), new PlayerCheat("Mickey") };
            */


            Console.WriteLine("Players Introduction: (without any cheaters)");
            Console.WriteLine("Player 1: Donald chooses a random player to ask for the rank of his first card.");
            Console.WriteLine("Player 2: Pooh chooses a random player to ask for a random rank.");
            Console.WriteLine("Player 3: chooses a random player to ask for the rank of his first card.");
            Console.WriteLine("Player 4: Mickey chooses a random player to ask for a random rank.");
            Console.WriteLine("\n----------------------------------------------------------\n\n");
            Player[] players = { new PlayerRandomFirst("Donald"), new PlayerRandomRandom("Pooh"), new PlayerRandomFirst("Joan"), new PlayerRandomRandom("Mickey") };




            //To test normal game play with cheater that remember player's last requested rank
            //Player[] players = { new PlayerRandomFirst("Donald"), new PlayerRandomRandom("Pooh"), new PlayerRemember("Joan"), new PlayerRandomRandom("Mickey") };

            //To test normal game play with cheater that ask for a card he/she doesnt have
            //Player[] players = { new PlayerRandomFirst("Donald"), new PlayerRandomRandom("Pooh"), new PlayerCheater("Joan"), new PlayerRandomRandom("Mickey") };


            Deck deck = new Deck();
            deck.ShuffleCard();
            deck.CutCard();
            int i, j; 

            for (i = 0; i < players.Length; i++)
                for (j = 0; j < 5; j++)
                    players[i].AddCard(deck.DealCard());


            //display player hand and check if any player already has a book at the same time
            for (i = 0; i < players.Length; i++)
            {
                Console.WriteLine(players[i]);
                if (players[i].Hand.HasBook())
                    Console.WriteLine(players[i].Name + "!");
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
                Player opponent = players[whoseTurn].ChoosePlayerToAsk(players);
                Rank requestRank = players[whoseTurn].ChooseRankToAskFor();
                int rankCount = opponent.Hand.GetRankCount(requestRank);
                int drewCard;
                samePlayersTurn = false;
                Card c;

                Console.WriteLine("_____________________________________________________________________________________");
                Console.WriteLine($"\nRound {round} : {players[whoseTurn].Name}'s turn.");
                Console.WriteLine("{0} says: {1} ! Give me all your {2} !",players[whoseTurn].Name, opponent.Name, requestRank);

                if(rankCount > 0)
                {
                    Card[] takenCards = opponent.Hand.GiveCardsToOpponent(requestRank, rankCount, players[whoseTurn]);
                    Console.Write(players[whoseTurn].Name + " took ");
                    for (i = 0; i < takenCards.Length; i++)
                        Console.Write(takenCards[i]);
                    Console.WriteLine(" from " + opponent.Name + "\n");

                    if(opponent.IsEmpty())
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
                        players[whoseTurn].AddCard(c);
                        Console.WriteLine($"{players[whoseTurn].Name} drew {c} from the deck.\n");
                    }
                    else
                        Console.WriteLine("There is no card to draw.\n");

                }

                for (i = 0; i < players.Length; i++)
                    Console.WriteLine(players[i]);



                if(players[whoseTurn].Hand.HasBook())
                {
                    Console.WriteLine(players[whoseTurn].Name + "!");
                    players[whoseTurn].AddScore();

                    if (players[whoseTurn].IsEmpty())
                    {
                        if (deck.Size() < 1)
                            Console.WriteLine(players[whoseTurn] + "'s hand is empty, oops, no more cards to draw...\n");
                        else
                        {
                            Console.WriteLine(players[whoseTurn] + "'s hand is empty, drawing new cards...\n");
                            drewCard = 0;

                            while (deck.Size() > 0 && drewCard < 5)
                            {
                                c = deck.DealCard();
                                players[whoseTurn].AddCard(c);
                                Console.WriteLine($"{players[whoseTurn].Name} drew {c} from the deck.\n");
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
            Console.WriteLine("\nGame has finally ended after {0} rounds!", round-1);

            int highestScore = GetHighestScore(players);
            for (i = 0; i < players.Length; i++)
            {
                if (players[i].Score == highestScore)
                    Console.WriteLine($"\nCongratulation! {players[i].Name} won with score of {highestScore} ! \n");
            }

        }
        public static int GetTotalScore(Player[] players)
        {
            int sum = 0;
            foreach (Player p in players)
                sum += p.Score;
            return sum;
        }

        public static int GetHighestScore(Player[] players)
        {
            int highest = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].Score > highest)
                    highest = players[i].Score;
            }
            return highest;
        }
    }

}
