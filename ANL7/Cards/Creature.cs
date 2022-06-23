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
        public override void UseCard()
        {
            Battle b = Battle.GetInstance();
            if (b.State is AttackState && this.CheckIfAlive() && !this.IsUsed)
            {
                //Runs the effect
                if (Effect != null)
                    this.Effect.UseEffect();

                CreatureAttack attack = new CreatureAttack(Attack, $"{this.Name} Attack");
                Battle.GetInstance().Stack.Add(attack);

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

    //We decided to make the attack of a creature a card because it must be counterable
    class CreatureAttack : InstantCard
    {
        public int attackdamage;

        public override void UseCardInStack()
        {
            Battle.GetInstance().getPlayerOfTurn().getOpponent().GetDamage(attackdamage);
        }

        public CreatureAttack(int attackdamage, string name)
        {
            this.attackdamage = attackdamage;
            this.Name = name;
            this.Color = new BlueFactory().BlendColor();
            this.NeededEnergy = 0;
        }
    }
}
