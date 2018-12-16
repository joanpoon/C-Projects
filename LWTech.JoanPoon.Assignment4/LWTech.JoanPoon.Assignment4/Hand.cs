using System;
namespace LWTech.JoanPoon.Assignment4
{
    public class Hand
    {
        public Card[] cards;

        public Hand()
        {
            cards = new Card[0];
        }

        public int Size()
        {
            return cards.Length;
        }

        public Card[] GetCards()
        {
            Card[] cardsCopy = new Card[cards.Length];
            Array.Copy(cards, cardsCopy, cards.Length);
            return cards;
        }

        public void AddCard(Card card)
        {
            Array.Resize(ref cards, Size() + 1);
            cards[Size() - 1] = card;
        }

        public Card[] RemoveCard(Card card)
        {
            Card[] newCards = new Card[cards.Length - 1];

            int i = 0;
            foreach (Card c in cards)
            {
                if (c != card)
                    newCards[i++] = c;
            }
            cards = newCards;
            return cards;
        }


        public bool HasBook()
        {
            if (cards == null) throw new Exception();

            foreach (Card c in cards)
            {
                int n = 0;
                foreach (Card d in cards)
                    if (c.Rank == d.Rank)
                        n++;
                if (n == 4)
                {
                    RemoveBook(c.Rank, 4);
                    return true;
                }
            }
            return false;
        }

        public int GetRankCount(Rank rank)
        {
            int count = 0;
            foreach(Card c in cards)
            {
                if (c.Rank == rank)
                    count++;
            }

            return count;
        }

        public Card[] RemoveBook(Rank rank, int count)
        {
            Card[] newCards = new Card[cards.Length-count];

            Console.Write($"\nFound a book of {rank} from ");

            int i = 0;
            foreach (Card c in cards)
            {
                if (c.Rank != rank)
                    newCards[i++] = c;
            }
            cards = newCards;
            return cards;
        }

        public Card[] GiveCardsToOpponent (Rank rank, int count, Player opponent)
        {
            Card[] newCards = new Card[cards.Length - count];
            Card[] tradedCards = new Card[count];

            int i = 0;
            int j = 0;

            foreach (Card c in cards)
            {
                if (c.Rank != rank)
                    newCards[i++] = c;
                else
                {
                    opponent.AddCard(c);
                    tradedCards[j++] = c;
                }
                    
            }
            cards = newCards;
            return tradedCards;
        }

        public override string ToString()
        {
            string s = "[";
            string comma = "";
            foreach (Card c in cards)
            {
                s += comma + c.ToString();
                comma = ", ";
            }
            s += "]";

            return s;
        }

    }

}

