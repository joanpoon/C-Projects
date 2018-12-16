using System;
namespace LWTech.JoanPoon.Assignment4
{
    public class Deck
    {
        Card[] deck;
        private static Random rng = new Random();

        public Deck()
        {
            Array suits = Enum.GetValues(typeof(Suit));
            Array ranks = Enum.GetValues(typeof(Rank));

            int size = suits.Length * ranks.Length;
            deck = new Card[size];

            int i = 0;
            foreach (Suit suit in suits)
            {
                foreach (Rank rank in ranks)
                {
                    Card card = new Card(rank, suit);
                    deck[i++] = card;
                }
            }
        }

        public void DisplayDeck()
        {
            for (int i = 0; i < deck.Length; i++)
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

            for (int n = deck.Length - 1; n > 0; --n)
            {
                int k = rng.Next(n + 1);
                Card temp = deck[n];
                deck[n] = deck[k];
                deck[k] = temp;

            }
        }

        public Card DealCard()
        {
            if (Size() == 0) return null;

            Card card = deck[Size() - 1];
            Array.Resize(ref deck, Size() - 1);

            return card;
        }

        public int CutCard()
        {
            int cutPoint = rng.Next(0, deck.Length - 1);
            Card[] cutDeck = new Card[deck.Length];

            for (int i = 0; i < deck.Length; i++)
            {
                cutDeck[i] = deck[cutPoint];
                if (cutPoint == deck.Length - 1)
                    cutPoint = 0;
                else
                    cutPoint++;
            }

            deck = cutDeck;
            return cutPoint;
        }

        public int Size()
        {
            return deck.Length;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < deck.Length; i++)
            {
                if (i % 3 == 0 && i != 0)
                    str += deck[i] + "\n";
                else
                    str += deck[i] + "\t";
            }
            return str;
        }
    }
}
