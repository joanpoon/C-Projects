using System;
using System.Diagnostics.Contracts;

namespace LWTech.JoanPoon.Assignment3
{
    public class Deck
    {
        Card[] deck;
        const int NumberOfCards = 52;
        int cardsOnHand;
        static Random rng = new Random();

        public Deck()
        {
            deck = new Card[NumberOfCards];
            int counter = 0;

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    deck[counter++] = new Card(rank, suit);
                }
            }

            cardsOnHand = 52;
        }

        public void DisplayDeck()
        {
            for (int i = 0; i < cardsOnHand; i++)
            {
                Console.Write(deck[i].ToString() + "\t");
                if (i % 4 == 0)
                    Console.WriteLine();
            }
        }

        public void ShuffleCard()
        {
            //Fisher Yates Shuffle Algorithm
            //Source: https://stackoverflow.com/questions/1150646/card-shuffling-in-c-sharp

            for (int n = cardsOnHand - 1; n > 0; --n)
            {
                int k = rng.Next(n + 1);
                Card temp = deck[n];
                deck[n] = deck[k];
                deck[k] = temp;

            }
        }

        public Card DealCard()
        {
            if (cardsOnHand > 0)
            {
                cardsOnHand--;
                return deck[cardsOnHand];

            }
            else throw new Exception();
        }

        public int CutCard()
        {
            int cutPoint = rng.Next(0, NumberOfCards - 1);
            Card[] cutDeck = new Card[NumberOfCards];

            for (int i = 0; i < NumberOfCards; i++)
            {
                cutDeck[i] = deck[cutPoint];
                if (cutPoint == NumberOfCards-1)
                    cutPoint = 0;
                else
                    cutPoint++;
            }

            deck = cutDeck;
            return cutPoint;
        }

        public int GetNumberOfCards()
        {
            return cardsOnHand;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < cardsOnHand; i++)
            {
                if (i % 3 == 0 && i != 0)
                    str += deck[i].ToString() + "\n";
                else
                    str += deck[i].ToString() + "\t";
            }
            return str;
        }
    }

}