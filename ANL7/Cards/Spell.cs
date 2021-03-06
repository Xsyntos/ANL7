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
    class Spell : InstantCard
    {

        public override void UseCardInStack()
        {
            this.Effect.UseEffect();
            Battle.GetInstance().getPlayerOfTurn().DiscardPile.Add(this);
        }

        public Spell(Effect effect, int neededEnergy, Color color, string Name)
        {
            this.Effect = effect;
            this.NeededEnergy = neededEnergy;
            this.Color = color;
            this.Name = Name;
        }
    }
}
