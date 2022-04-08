using ANL7.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANL7.Cards
{
    class Creature : PermantantCard
    {
        public int Defence;
        public int Attack;
        public Effect StartEffect;
        public Effect Effect;

        public Creature(int Defence, int Attact, Color color, int neededEnergy, string name, Effect startEffect = null, Effect effect = null)
        {
            this.Defence = Defence;
            this.Attack = Attact;
            this.StartEffect = startEffect;
            this.Effect = effect;
            this.Color = color;
            this.NeededEnergy = neededEnergy;
            this.Name = name;
        }
        public override bool CheckIfAlive()
        {
            if(Defence > 0)
                return true;
            else
            {
                //Remove from floor
                Player p = Battle.GetInstance().getPlayerOfTurn();
                p.Floor.Remove(this);
                p.DiscardPile.Add(this);
                return false;
            }
        }
        public override void PlayCardToStack()
        {
            //Checks the energy
            base.PlayCardToStack();
        }
        public override void UseCard()
        {
            Battle b = Battle.GetInstance();
            if (b.State is AttackState && this.CheckIfAlive() && !this.IsUsed)
            {
                //Runs the effect
                if (Effect != null)
                    this.Effect.UseEffect();

                Battle.GetInstance().getPlayerOfTurn().getOpponent().GetDamage(this.Attack);

                ToggleUsed();
            }
        }

        public override void UseCardInStack()
        {
            base.UseCardInStack();
            if(this.StartEffect != null)
                this.StartEffect.UseEffect();

        }
    }
}
