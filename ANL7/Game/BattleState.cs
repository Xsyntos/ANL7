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

namespace ANL7.Game
{
    abstract class BattleState
    {
        public abstract void NextState();
    }

    class InitState : BattleState
    {
        public InitState(int amountofcards, int Life)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine("The game starts!");
            Console.ForegroundColor = ConsoleColor.White;


            for (int i = 0; i < amountofcards; i++)
            {
                foreach(Player player in Battle.GetInstance().players)
                {
                    player.GrabACard();
                }
            }

            foreach (Player player in Battle.GetInstance().players)
            {
                player.Life = Life;
            }
        }
        public override void NextState()
        {
            Battle.GetInstance().State = new PrepState();

        }
    }

    class PrepState : BattleState
    {
        public PrepState()
        {


            Battle b = Battle.GetInstance();
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine("Turn " + b.Turn / 2);
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("It is " + b.getPlayerOfTurn().UserName + " his turn!");
            Console.ForegroundColor = ConsoleColor.White;


            b.getPlayerOfTurn().ResetFloorCards();

            foreach(PermantantCard c in b.getPlayerOfTurn().Floor)
            {
                c.UseCard();
            }

        }
        public override void NextState()
        {
            Battle.GetInstance().State = new DrawingState();

        }
    }

    class DrawingState : BattleState
    {
        public DrawingState()
        {
            Battle b = Battle.GetInstance();
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("" + b.getPlayerOfTurn().UserName + " picks a card!");
            Console.ForegroundColor = ConsoleColor.White;


            b.getPlayerOfTurn().GrabACard();

        }
        public override void NextState()
        {
            Battle.GetInstance().State = new AttackState();
        }
    }

    class AttackState : BattleState
    {
        public AttackState()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("" + Battle.GetInstance().getPlayerOfTurn().UserName + " is attacking!");
            Console.ForegroundColor = ConsoleColor.White;


        }
        public override void NextState()
        {
            Battle.GetInstance().State = new EndState();


        }
    }

    class EndState : BattleState
    {
        public EndState()
        {
            Player p = Battle.GetInstance().getPlayerOfTurn();
            while(p.Hand.Count > 7)
            {
                p.ThrowACard();
            }
            Battle.GetInstance().NextPlayer();
            Battle.GetInstance().Turn++;
        }
        public override void NextState()
        {
            Battle.GetInstance().State = new PrepState();
        }
    }
    class FinishedState : BattleState
    {
        public FinishedState(int winner)
        {
            Battle b = Battle.GetInstance();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            Console.WriteLine("The winner of the game is: " + b.players[winner].UserName);
            Console.ForegroundColor = ConsoleColor.White;

        }
        public FinishedState(Player winner)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            Console.WriteLine("The winner of the game is: " + winner.UserName);
            Console.ForegroundColor = ConsoleColor.White;

        }
        public override void NextState()
        {
            //End the execution
            return;
        }
    }

}
