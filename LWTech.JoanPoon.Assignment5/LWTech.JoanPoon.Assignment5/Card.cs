using System;
namespace LWTech.JoanPoon.Assignment5
{
    public enum Rank { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }
    public enum Suit { Hearts, Diamonds, Clubs, Spades }


    public class Card
    {
        public Rank Rank { get; private set; }
        public Suit Suit { get; private set; }

        public Card(Rank cardRank, Suit cardSuit)
        {
            this.Rank = cardRank;
            this.Suit = cardSuit;
        }

        public Rank GetRank()
        {
            return this.Rank;
        }

        public override string ToString()
        {
            return ("[" + Rank + " of " + Suit + "]");
        }
    }

}