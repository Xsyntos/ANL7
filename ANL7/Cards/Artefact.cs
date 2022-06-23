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
    class Artefact : PermantantCard
    {
        public bool Alive = true;
        public Effect Effect;
        public Effect ReturnEffect;


        public Artefact(Effect effect, Effect returneffect, bool alive, int cost)
        {
            this.Effect = effect;
            this.ReturnEffect = returneffect;
            this.Color = new NeutralFactory().BlendColor();
            this.Alive = alive;
            this.NeededEnergy = cost;
        }

        public override bool CheckIfAlive()
        {
            return Alive;
        }

        public override void UseCard()
        {

        }

        public override void UseCardInStack()
        {
            base.UseCardInStack();
            this.Effect.UseEffect();
        }
    }
}
