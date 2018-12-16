using System;
namespace LWTech.JoanPoon.Assignment3
{

    public class Card
    {
        public Rank rank { get; private set; }
        public Suit suit { get; private set; }

        public Card(Rank cardRank,Suit cardSuit)
        {
            rank = cardRank;
            suit = cardSuit;
        }

        public override string ToString()
        {
            return rank + " of " + suit;
        }
    }
}
