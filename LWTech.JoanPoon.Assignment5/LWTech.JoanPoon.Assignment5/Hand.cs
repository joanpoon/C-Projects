using System;
using System.Collections.Generic;

namespace LWTech.JoanPoon.Assignment5
{
    public class Hand
    {
        public List<Card> cards { get; private set; }

        public Hand()
        {
            cards = new List<Card>();
        }

        public int Size()
        {
            return cards.Count;
        }


        public void AddCard(Card card)
        {
            if (card == null) throw new Exception();
            cards.Add(card);
        }

        public List<Card> RemoveCard(Card card)
        {
            if (cards.Count == 0) throw new Exception();
            cards.Remove(card);
            return cards;
        }


        public Card HasBook()
        {
            if (cards.Count == 0) throw new Exception();

            for (int i = 0; i < cards.Count; i++)
            {
                Card c = cards[i];
                int n = 0;
                for (int j = 0; j < cards.Count; j++)
                    if (c.Rank == cards[j].Rank)
                        n++;
                if (n == 4)
                    return c;

            }
            return null;
        }

        public int GetRankCount(Rank rank)
        {
            if (rank.Equals(null) || cards.Count == 0) throw new Exception();

            int count = 0;
            for (int i = 0; i < cards.Count; i++)
            {
                Card c = cards[i];
                if (c.Rank == rank)
                    count++;
            }

            return count;
        }

        public void RemoveBook(Card card)
        {
            if (card == null|| cards.Count == 0) throw new Exception();
            List<Card> updatedHand = new List<Card>();

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Rank != card.Rank)
                    updatedHand.Add(cards[i]);
            }

            cards = updatedHand;
        }


        public List<Card> GiveCardsToOpponent(Rank rank, Player opponent)
        {
            if (rank.Equals(null) || cards.Count == 0 || opponent == null) throw new Exception();

            List<Card> tradedCards = new List<Card>();

            for (int i = 0; i < cards.Count; i++)
            {
                Card c = cards[i];
                if (c.Rank == rank)
                {
                    opponent.AddCard(c);
                    this.RemoveCard(c);
                    tradedCards.Add(c);
                }

            }
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