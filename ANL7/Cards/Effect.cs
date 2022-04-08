using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
