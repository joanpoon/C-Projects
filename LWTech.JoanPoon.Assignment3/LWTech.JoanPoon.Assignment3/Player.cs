using System;
namespace LWTech.JoanPoon.Assignment3
{
    public class Player
    {
        const int handOfCard = 5;
        int cardsOnHand;
        string name;
        Card[] playerHand;

        public Player(string name)
        {
            playerHand = new Card[handOfCard];
            cardsOnHand = 0;
            this.name = name;
        }

        public bool AddCardToHand(Card card)
        {
            if (cardsOnHand < 5)
            {
                playerHand[cardsOnHand++] = card;
                return true;
            }
            else
                return false;
        }


        public override string ToString()
        {
            string stringOutput = name + "'s hand of cards: \n";
            for (int i = 0; i < cardsOnHand; i++)
            {
                stringOutput += playerHand[i] + "\n";
            }

            return stringOutput;

        }
    }
}
