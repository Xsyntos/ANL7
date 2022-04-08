using ANL7.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANL7.Cards
{
    class Land : PermantantCard
    {


        public override bool CheckIfAlive()
        {
            //A land cant die
            return true;
        }

        public override void UseCardInStack()
        {
            //A land is never in the stack, so this function doesn't need functionality
            return;
        }

        public override void PlayCardToStack()
        {
            //Move a land directly to the floor
            Battle.GetInstance().getPlayerOfTurn().Floor.Add(this);
        }
        public override void UseCard()
        {
            if (!this.IsUsed && Battle.GetInstance().State is AttackState)
            {
                //Add energy to the player
                this.ToggleUsed();
            }
        }

        public Land(Color color)
        {
            this.Color = color;
            this.Name = "Land of the color: " + color.ToString();
            this.NeededEnergy = 0;
        }
    }
}
