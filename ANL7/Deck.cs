using ANL7.Cards;
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
    class Deck
    {
        private List<Card> cards;

        public Deck(List<Card> cards)
        {
            this.cards = cards;
        }
        public Deck() : this(new List<Card>()) { }

        /// <summary>
        /// Add a card to deck
        /// </summary>
        /// <param name="card"></param>
        public void AttachCard(Card card) => this.cards.Add(card);

        /// <summary>
        /// Remove a card from the deck
        /// </summary>
        /// <param name="card"></param>
        public void DetachCard(Card card) => this.cards.Remove(card);

        /// <summary>
        /// Pick a random card from the deck and remove it
        /// </summary>
        /// <returns>A random card</returns>
        public Card PickCard()
        {
            if (this.cards.Count > 0)
            {
                int r = new Random().Next(0, this.cards.Count);
                Card c = this.cards[r];
                this.cards.Remove(c);

                return c;
            }
            //If the deck is empty the player will receive a spell with neededEnergy -1.
            return new Spell(new Effect(()=> { return; }), -1 , new BlueFactory().BlendColor(), "Empty");
        }

    }
}
