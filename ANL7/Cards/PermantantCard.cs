using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Steven Rietberg 1008478
 * Yannick de Vreede 1009289
 */

namespace ANL7.Cards
{
    abstract class PermantantCard : Card
    {
        public bool IsUsed = true;
        /// <summary>
        /// Toggle the use variable
        /// </summary>
        public void ToggleUsed()
        {
            this.CheckIfAlive();
            this.IsUsed = !this.IsUsed;
        }

        public override void UseCardInStack()
        {
            Battle.GetInstance().getPlayerOfTurn().Floor.Add(this);
        }
        /// <summary>
        /// Use the card from the floor
        /// </summary>
        public abstract void UseCard();
        /// <summary>
        /// Checks if the card is alive
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckIfAlive();
        

    }
}
