using System;

namespace LWTech.JoanPoon.Assignment4
{
    public abstract class Player
    {
        public string Name { get; private set; }
        public Hand Hand { get; private set; }
        public int Score { get; private set; }
        public Rank LastRequestedRank;

        protected static Random rng = new Random();

        // Other Properties/member variables go here

        public Player(string name)
        {
            this.Name = name;
            this.Hand = new Hand();
            this.Score = 0;
        }

        public abstract Player ChoosePlayerToAsk(Player[] players);
        public abstract Rank ChooseRankToAskFor();

        // Other Player methods go here

        public bool IsEmpty()
        {
            if (Hand.Size() == 0)
                return true;
            else
                return false;
        }

        public void AddScore()
        {
            this.Score++;
        }

        public override string ToString()
        {
            string s = Name + "'s Hand: ";
            s += Hand.ToString();
            return s;
        }


        public string GetScore()
        {
            return Name + " : " + Score;
        }

        public void AddCard(Card c)
        {
            Hand.AddCard(c);
        }

    }

    public class PlayerRandomFirst : Player
    {
        public PlayerRandomFirst(string name) : base(name)
        {
        }

        public override Player ChoosePlayerToAsk(Player[] players)
        {
            bool isValid = false;
            int n;
            while (!isValid)
            {
                n = rng.Next(players.Length);
                if (!players[n].IsEmpty() && !players[n].Name.Equals(this.Name))
                    return players[n];
            }

            throw new Exception(); //should not happen
        }

        public override Rank ChooseRankToAskFor()
        {
            LastRequestedRank = Hand.cards[0].GetRank();
            return Hand.cards[0].GetRank();

        }
    }

    public class PlayerRandomRandom : Player
    {
        public PlayerRandomRandom(string name) : base(name)
        {
        }

        public override Player ChoosePlayerToAsk(Player[] players)
        {
            bool isValid = false;
            int n;
            while (!isValid)
            {
                n = rng.Next(players.Length);
                if (!players[n].IsEmpty() && !players[n].Name.Equals(this.Name))
                    return players[n];
            }

            throw new Exception(); //should not happen
        }

        public override Rank ChooseRankToAskFor()
        {
            int i;

            if (Hand.Size() == 1)
                i = 0;
            else
                i = rng.Next(Hand.Size());

            LastRequestedRank = Hand.cards[i].GetRank();
            return Hand.cards[i].GetRank();

        }
    }

    public class PlayerCheat : Player
    {
        Player opponent;

        public PlayerCheat(string name) : base(name)
        {
        }

        public override Player ChoosePlayerToAsk(Player[] players)
        {
            bool isValid = false;
            int n;

            while (!isValid)
            {
                n = rng.Next(players.Length);
                if (!players[n].IsEmpty() && !players[n].Name.Equals(this.Name))
                {
                    this.opponent = players[n];
                    return players[n];
                }
            }

            throw new Exception(); //should not happen
        }

        public override Rank ChooseRankToAskFor()
        {
            LastRequestedRank = opponent.Hand.cards[0].GetRank();
            return opponent.Hand.cards[0].GetRank();
        }
    }

    public class PlayerRemember : Player
    {
        Player opponent;

        public PlayerRemember(string name) : base(name)
        {
        }

        public override Player ChoosePlayerToAsk(Player[] players)
        {
            bool isValid = false;
            int n;

            while (!isValid)
            {
                n = rng.Next(players.Length);
                if (!players[n].IsEmpty() && !players[n].Name.Equals(this.Name))
                {
                    this.opponent = players[n];
                    return players[n];
                }
            }

            throw new Exception(); //should not happen
        }

        public override Rank ChooseRankToAskFor()
        {
            return opponent.LastRequestedRank;
        }
    }

    public class PlayerFirstPlayer : Player
    {
        public PlayerFirstPlayer(string name) : base(name + "Left")
        {
        }

        public override Player ChoosePlayerToAsk(Player[] players)
        {
            Player player = this;
            int i = 0;

            while( player == this || !player.IsEmpty())
            {
                player = players[i++];
            }
            return player;
        }

        public override Rank ChooseRankToAskFor()
        {
            LastRequestedRank = Hand.cards[0].Rank;
            return Hand.cards[0].Rank;

        }
    }

}
