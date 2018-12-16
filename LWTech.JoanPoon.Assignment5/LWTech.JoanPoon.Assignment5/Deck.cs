using System;
using System.Collections.Generic;

namespace LWTech.JoanPoon.Assignment5
{
    public class Deck
    {
        private Stack<Card> deck;
        private static Random rng = new Random();

        public Deck()
        {
            deck = new Stack<Card>();


            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    Card card = new Card(rank, suit);
                    deck.Push(card);
                }
            }
        }

        public void DisplayDeck()
        {
            int i = 1; // for formatting
            foreach (Card c in deck)
            {
                Console.Write(c + "\t");
                i++;
                if (i % 4 == 0)
                    Console.WriteLine();
            }
        }

        public void ShuffleCard()
        {
            if (deck.Count == 0) throw new Exception();
            //Fisher Yates Shuffle Algorithm
            //Source: https://stackoverflow.com/questions/1150646/card-shuffling-in-c-sharp
            Card[] cardArray = deck.ToArray();


            for (int n = deck.Count - 1; n > 0; --n)
            {
                int k = rng.Next(n + 1);
                Card temp = cardArray[n];
                cardArray[n] = cardArray[k];
                cardArray[k] = temp;
            }

            deck.Clear();

            foreach( Card c in cardArray)
            {
                deck.Push(c);
            }
        }

        public Card DealCard()
        {
            if (deck.Count == 0) throw new Exception();

            return deck.Pop();
        }

        public int CutCard()
        {
            if (deck.Count == 0) throw new Exception();

            Card[] cardArray = deck.ToArray();

            int cutPoint = rng.Next(0, cardArray.Length - 1);
            Card[] cutDeck = new Card[cardArray.Length];

            for (int i = 0; i < cardArray.Length; i++)
            {
                cutDeck[i] = cardArray[cutPoint];
                if (cutPoint == cardArray.Length - 1)
                    cutPoint = 0;
                else
                    cutPoint++;
            }

            deck.Clear();

            foreach (Card c in cardArray)
            {
                deck.Push(c);
            }

            return cutPoint;
        }

        public int Size()
        {
            return deck.Count;
        }

        public override string ToString()
        {
            string str = "";
            int i = 1; // for formatting
            foreach (Card c in deck)
            {
                str += c + "\t";
                if(i % 4 == 0)
                    str += "\n";
            }
            return str;
        }
    }
}