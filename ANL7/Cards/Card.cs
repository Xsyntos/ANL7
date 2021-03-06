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
    abstract class Card
    {
        public Color Color;
        public string Name;
        public int NeededEnergy;

        /// <summary>
        /// Use the card inside the Stack
        /// For example: use the effect of a spell
        /// </summary>
        public abstract void UseCardInStack();

        /// <summary>
        /// move the card to the stack
        /// </summary>
        public virtual void PlayCardToStack()
        {
            //Move the card to the stack
            Battle.GetInstance().Stack.Add(this);
        }


    }
}
