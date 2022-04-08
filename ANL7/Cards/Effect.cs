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
    class Effect
    {
        private Action effect;
        public Effect(Action action)
        {
            this.effect = action;
        }

        public void UseEffect()
        {
            effect();
        }
    }
}
