using ANL7.Cards;
using ANL7.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Steven Rietberg 1008478
 * Yannick de Vreede 1009289
 */

namespace ANL7
{
    class Player
    {
        public string UserName;
        public Deck Deck;
        public int Life;
        public List<Card> Hand;
        public List<Card> DiscardPile;

        public List<PermantantCard> Floor;

        public Creature Defender;

        public Player(string Username, Deck deck)
        {
            this.UserName = Username;
            this.Deck = deck;
            this.Life = 10;
            this.Hand = new List<Card>();
            this.DiscardPile = new List<Card>();
            this.Floor = new List<PermantantCard>();
        }
    
        public Player(string Username, List<Card> deck) : this(Username, new Deck(deck)) {}

        /// <summary>
        /// Pick a random card from the deck
        /// </summary>
        public void GrabACard()
        {
            Card c = Deck.PickCard();
            if(c is Spell)
            {
                Spell s = (Spell)c;
                if (s.Color.ColorValue == "Blue" && s.NeededEnergy == -1)
                {
                    //Change the state of the game to the finished state
                    Battle.GetInstance().State = new FinishedState(this.getOpponent());
                    return;
                }
            }
            Hand.Add(c);
        }

        /// <summary>
        /// Throw a random card to the Discardpile
        /// </summary>
        public void ThrowACard()
        {
            int r = new Random().Next(0, this.Hand.Count);
            Card c = this.Hand[r];
            this.Hand.Remove(c);
            this.DiscardPile.Add(c);
        }
        /// <summary>
        /// Deal damage to this Player
        /// </summary>
        /// <param name="dmg"></param>
        public void GetDamage(int dmg)
        {
            if (Defender != null)
            {
                if (Defender.Defence > dmg)
                {
                    Defender.Defence -= dmg;
                    Console.WriteLine("The Defender of " + this.UserName + " is damaged by " + dmg);
                }
                else
                {
                    Console.WriteLine("The Defender of " + this.UserName + "died");
                    Console.WriteLine("" + this.UserName + " is damaged by " + (dmg - Defender.Defence));

                    this.Life -= (dmg - Defender.Defence);

                    Defender = null;
                }
            }
            else
            {
                this.Life -= dmg;
                Console.WriteLine("" + this.UserName + " is damaged by " + (dmg));
                if(Life <= 0)
                {
                    Battle.GetInstance().State = new FinishedState(this.getOpponent());
                }
            }
        }

        /// <summary>
        /// Check if the player has enough energy of a certain color
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool CheckEnegry(int amount, string color)
        {
            int AmountOnFloor = 0;
            foreach(PermantantCard card in Floor)
            {
                if(card is Land && !card.IsUsed && (card.Color.ColorValue == color || color == ""))
                {
                    AmountOnFloor++;
                }
            }

            return (AmountOnFloor >= amount);
        }

        /// <summary>
        /// Use a specific amount of energy of a specific color
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="color"></param>
        private void UseEnergy(int amount, string color)
        {
            int i = 0;
            foreach(PermantantCard card in Floor)
            {
                if (i == amount)
                    break;

                if (card is Land && !card.IsUsed && (card.Color.ColorValue == color || color == ""))
                {
                    card.UseCard();
                    i++;
                }
            }
        }
        /// <summary>
        /// Play a card from the hand
        /// </summary>
        /// <param name="card"></param>
        public void PlayCard(Card card)
        {
            if (Battle.GetInstance().State is AttackState && Battle.GetInstance().getPlayerOfTurn() == this)
            {
                if (Hand.Contains(card))
                {
                    if (card is Land)
                    {
                        Hand.Remove(card);
                        card.PlayCardToStack();
                    }
                    else
                    {
                        if (CheckEnegry(card.NeededEnergy, card.Color.ColorValue))
                        {
                            Hand.Remove(card);
                            card.PlayCardToStack();
                            UseEnergy(card.NeededEnergy, card.Color.ColorValue);
                            var p = Battle.GetInstance().NextPlayer();
                            Console.WriteLine(p.UserName + " do you want to counter?");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You don't have this Card in your hand!");
                }
            }
            else
            {
                Console.WriteLine("It is not your turn!");
            }
        }

        /// <summary>
        /// Skip the attack/counter
        /// </summary>
        public void SkipCounter()
        {
            if(Battle.GetInstance().getPlayerOfTurn() == this && Battle.GetInstance().State is AttackState)
            {
                if(Battle.GetInstance().Stack.Count == 0)
                {
                    Console.WriteLine(this.UserName + " didn't want to attack!");

                }
                else
                {
                    Console.WriteLine(this.UserName + " didn't want to counter!");

                }
                Battle battle = Battle.GetInstance();
                battle.NextPlayer();
                battle.NotifyStack();
            }
            else
            {
                Console.WriteLine("It is not your turn!");
            }
        }

        /// <summary>
        /// Gets the opponent of the player
        /// </summary>
        /// <returns></returns>
        public Player getOpponent()
        {
            Battle b = Battle.GetInstance();
            foreach(Player p in b.players)
            {
                if (p != this)
                    return p;
            }
            return new Player("DefaultNoob69", new List<Card>());
        }
        /// <summary>
        /// Resets the cards on the floor
        /// And display the floors of the players
        /// </summary>
        public void ResetFloorCards()
        {
            foreach(PermantantCard pc in Floor )
            {
                if (pc.IsUsed)
                    pc.ToggleUsed();
                else
                    pc.CheckIfAlive();
            }

            Console.WriteLine(this.UserName + "'s floor has:");
            foreach(PermantantCard pc in Floor)
            {
                Console.WriteLine(pc.Name);
            }
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(getOpponent().UserName + "'s floor has:");

            foreach (PermantantCard pc in getOpponent().Floor)
            {
                Console.WriteLine(pc.Name);
            }
            Console.WriteLine("--------------------------------------------------");

        }

    }
}
