using System;

namespace LWTech.JoanPoon.Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Joan Poon \t\t\t\t Assignment 3 ");
            Console.WriteLine("Deck of 52 playing cards ");
            Console.WriteLine("\nSpecial thanks to genius user 'hughdbrown' at: ");
            Console.WriteLine("https://stackoverflow.com/questions/1150646/card-shuffling-in-c-sharp");
            Console.WriteLine("_______________________________________________________\n\n");

            Deck deck1 = new Deck();

            Player amy = new Player("Amy");
            Player joan = new Player("Joan");
            Player david = new Player("David");
            Player mary = new Player("Mary");

            Console.WriteLine("New deck:\n");
            Console.WriteLine(deck1.ToString());
            Console.WriteLine("\n\nThere are {0} cards in the deck.", deck1.GetNumberOfCards());
            Console.WriteLine("_____________________________________________________");


            deck1.ShuffleCard();
            Console.WriteLine("\n\nShuffled deck using Fisher Yates shuffle algorithm:\n");
            Console.WriteLine(deck1.ToString());
            Console.WriteLine("There are {0} cards in the deck.", deck1.GetNumberOfCards());
            Console.WriteLine("_____________________________________________________");


            Console.WriteLine("\n\nDeck was cut at cut point : {0} \n", deck1.CutCard());
            Console.WriteLine("Deck after cutting:\n");
            Console.WriteLine(deck1.ToString());
            Console.WriteLine("There are {0} cards in the deck.", deck1.GetNumberOfCards());
            Console.WriteLine("_____________________________________________________");

            Console.WriteLine("\n\nDealing 4 hands of 5 cards each to 4 players...\n");

            for (int i = 0; i < 5; i++)
            {
                amy.AddCardToHand(deck1.DealCard());
                joan.AddCardToHand(deck1.DealCard());
                david.AddCardToHand(deck1.DealCard());
                mary.AddCardToHand(deck1.DealCard());
            }

            Console.WriteLine(amy.ToString() + "\n\n");
            Console.WriteLine(joan.ToString() + "\n\n");
            Console.WriteLine(david.ToString() + "\n\n");
            Console.WriteLine(mary.ToString() + "\n\n");


            Console.WriteLine("\n_____________________________________________________");
            Console.WriteLine("\nThere are {0} cards left in the deck.", deck1.GetNumberOfCards());
            Console.WriteLine("Remaining deck:\n");
            Console.WriteLine(deck1.ToString());


            Console.ReadLine();
        }


    }

}
