using ANL7.Cards;
using ANL7.Game;
using System;
using System.Collections.Generic;

/*
 * Steven Rietberg 1008478
 * Yannick de Vreede 1009289
 */

namespace ANL7
{
    class Program
    {
        static void Main(string[] args)
        {
            //Just a reference for the cards that are needed for turn 2
            var CardsForTurn2Player1 = new List<Card>();
            // 0 = Blue land
            // 1 = Blue land
            // 2 = Blue land
            // 3 = Blue Slime
            // 4 = Blue Ball
            // 5 = Red counter

            var CardsForTurn2Player2 = new List<Card>();
            //0 = Blue land
            //1 = Blue land
            //2 = Red land
            //3 = Red land
            //4 = Artefact
            //5 = Counter




            //Preparing Deck for player 1
            Deck player1Deck = new Deck();

            for(int i = 0; i < 3; i++ )
            {
                Card c = new Land(new BlueFactory().BlendColor());
                player1Deck.AttachCard(c);
                CardsForTurn2Player1.Add(c);
            }


            Card blueslime = new Creature(2,2,new BlueFactory().BlendColor(),2,"Blue slime",
                    new Effect(()=> {
                        Player p = Battle.GetInstance().getPlayerOfTurn().getOpponent();
                        p.Hand.RemoveAt(p.Hand.Count - 1);
                        Console.WriteLine(p.UserName + " just lost a card, after a blue slime appeared");
                    }
                ));
            Card blueslimeball = new Spell(new Effect(() => {}), 2, new BlueFactory().BlendColor(), "Blue Slimeball Ball"); //We made the spell blue because otherwise there are not enough lands
            ((Spell)blueslimeball).Effect = new Effect(() =>
            {
                foreach (var card in Battle.GetInstance().getPlayerOfTurn().Floor)
                {
                    if (card is Creature)
                    {
                        Creature c = (Creature)card;
                        c.Defence += 3;
                        c.Attack += 3;
                    }
                }
                Battle b = Battle.GetInstance();
                if (b.Stack[b.Stack.IndexOf(blueslimeball) + 1] is CreatureAttack)
                    ((CreatureAttack)b.Stack[b.Stack.IndexOf(blueslimeball) + 1]).attackdamage += 3;
            });
            player1Deck.AttachCard(blueslime);
            CardsForTurn2Player1.Add(blueslime);

            player1Deck.AttachCard(blueslimeball);
            CardsForTurn2Player1.Add(blueslimeball);

            Card redcounter = new Spell(new Effect(() => { }), 1, new BlueFactory().BlendColor(), "Red Spell absorver");
            ((Spell)redcounter).Effect = new Effect(()=> {
                Battle b = Battle.GetInstance();
                if (b.Stack[b.Stack.IndexOf(redcounter) + 1].Color is Red && b.Stack[b.Stack.IndexOf(redcounter) + 1] is Spell)
                    ((Spell)b.Stack[b.Stack.IndexOf(redcounter) + 1]).Effect = new Effect(() => { });
            });

            player1Deck.AttachCard(redcounter);
            CardsForTurn2Player1.Add(redcounter);

            for (int i = 0; i < 24; i++)
            {
                Card c = new Spell(new Effect(()=> 
                {
                    Console.WriteLine("Hey! I am Mr Mee6! I am a dummy card :D!");
                }),
                1,new BlueFactory().BlendColor(), "Mee6");
                player1Deck.AttachCard(c);
            }

            Deck player2Deck = new Deck();

            for (int i = 0; i < 26; i++)
            {
                Card c = new Spell(new Effect(() =>
                {
                    Console.WriteLine("Hey! I am Mr Mee6! Existance is Pain!");
                }),
                1, new BlueFactory().BlendColor(), "Mee6");
                player2Deck.AttachCard(c);
            }

            for (int i = 0; i < 2; i++)
            {
                Card d = new Land(new BlueFactory().BlendColor());
                player2Deck.AttachCard(d);
                CardsForTurn2Player2.Add(d);
            }

            for (int i = 0; i < 2; i++)
            {
                Card d = new Land(new RedFactory().BlendColor());
                player2Deck.AttachCard(d);
                CardsForTurn2Player2.Add(d);
            }

            Card artefact1 = new Artefact(new Effect(() => {/* actual implementation wasn't necessary for this assignment (will be hardcoded) */ }), new Effect(() => {/* actual implementation wasn't necessary for this assignment */ }), false,2);
            player2Deck.AttachCard(artefact1);
            CardsForTurn2Player2.Add(artefact1);

            Card counterspell = new Spell(new Effect(() => {   }), 1, new RedFactory().BlendColor(), "Counter");
            ((Spell)counterspell).Effect = new Effect(() => {
                Battle b = Battle.GetInstance();
                for (int i = 0; i < b.Stack.IndexOf(counterspell); i++)
                {
                    if (b.Stack[i] is CreatureAttack)
                    {
                        ((CreatureAttack)b.Stack[i]).attackdamage = 0;
                    }
                }
            
            });
            player2Deck.AttachCard(counterspell);
            CardsForTurn2Player2.Add(counterspell);
            //Player Init

            Player Arold = new Player("Arold", player1Deck);
            Player Bryce = new Player("Bryce", player1Deck);

            Battle.GetInstance().AddPlayer(Arold);
            Battle.GetInstance().AddPlayer(Bryce);

            //Start the game
            Battle.GetInstance().StartGame(7, 10);
            Battle b = Battle.GetInstance();

            //Cheating the land cards to the floor
            if (Arold.Hand.Contains(CardsForTurn2Player1[0]))
            {
                Arold.GrabACard();
                Arold.Hand.Remove(CardsForTurn2Player1[0]);
            }

            if (Arold.Hand.Contains(CardsForTurn2Player1[1]))
            {
                Arold.GrabACard();
                Arold.Hand.Remove(CardsForTurn2Player1[1]);
            }

            if (Arold.Hand.Contains(CardsForTurn2Player1[2]))
            {
                Arold.Hand[Arold.Hand.IndexOf(CardsForTurn2Player1[2])] = Arold.Hand[0];
                Arold.Hand[0] = CardsForTurn2Player1[2];
            }
            else
            {
                Arold.Deck.AttachCard(Arold.Hand[0]);
                Arold.Hand[0] = CardsForTurn2Player1[2];
            }

            if (Arold.Hand.Contains(CardsForTurn2Player1[3]))
            {
                Arold.Hand[Arold.Hand.IndexOf(CardsForTurn2Player1[3])] = Arold.Hand[1];
                Arold.Hand[1] = CardsForTurn2Player1[3];
            }
            else
            {
                Arold.Deck.AttachCard(Arold.Hand[1]);
                Arold.Hand[1] = CardsForTurn2Player1[3];
            }

            if (Arold.Hand.Contains(CardsForTurn2Player1[4]))
            {
                Arold.Hand[Arold.Hand.IndexOf(CardsForTurn2Player1[4])] = Arold.Hand[2];
                Arold.Hand[2] = CardsForTurn2Player1[4];
            }
            else
            {
                Arold.Deck.AttachCard(Arold.Hand[2]);
                Arold.Hand[2] = CardsForTurn2Player1[4];
            }

            if (Arold.Hand.Contains(CardsForTurn2Player1[5]))
            {
                Arold.Hand[Arold.Hand.IndexOf(CardsForTurn2Player1[5])] = Arold.Hand[3];
                Arold.Hand[3] = CardsForTurn2Player1[5];
            }
            else
            {
                Arold.Deck.AttachCard(Arold.Hand[3]);
                Arold.Hand[3] = CardsForTurn2Player1[5];
            }

            Arold.GrabACard();
            

            Arold.Floor.Add((Land)CardsForTurn2Player1[0]);
            Arold.Floor.Add((Land)CardsForTurn2Player1[1]);
            Arold.Hand.RemoveAt(7);
            Arold.Hand.RemoveAt(6);


            if (Bryce.Hand.Contains(CardsForTurn2Player2[0]))
            {
                Bryce.GrabACard();
                Bryce.Hand.Remove(CardsForTurn2Player2[0]);

            }
            if (Bryce.Hand.Contains(CardsForTurn2Player2[1]))
            {
                Bryce.GrabACard();
                Bryce.Hand.Remove(CardsForTurn2Player2[1]);

            }
            if (Bryce.Hand.Contains(CardsForTurn2Player2[2]))
            {
                Bryce.GrabACard();
                Bryce.Hand.Remove(CardsForTurn2Player2[2]);

            }
            if (Bryce.Hand.Contains(CardsForTurn2Player2[3]))
            {
                Bryce.GrabACard();
                Bryce.Hand.Remove(CardsForTurn2Player2[3]);

            }

            if (Bryce.Hand.Contains(CardsForTurn2Player2[4]))
            {
                Bryce.Hand[Bryce.Hand.IndexOf(CardsForTurn2Player2[4])] = Bryce.Hand[0];
                Bryce.Hand[0] = CardsForTurn2Player2[4];
            }
            else
            {
                Bryce.Deck.AttachCard(Bryce.Hand[0]);
                Bryce.Hand[0] = CardsForTurn2Player2[4];
            }

            if (Bryce.Hand.Contains(CardsForTurn2Player2[5]))
            {
                Bryce.Hand[Bryce.Hand.IndexOf(CardsForTurn2Player2[5])] = Bryce.Hand[1];
                Bryce.Hand[1] = CardsForTurn2Player2[5];
            }
            else
            {
                Bryce.Deck.AttachCard(Bryce.Hand[1]);
                Bryce.Hand[1] = CardsForTurn2Player2[5];
            }




            Bryce.GrabACard();
            Bryce.Floor.Add((Land)CardsForTurn2Player2[0]);
            Bryce.Floor.Add((Land)CardsForTurn2Player2[1]);
            Bryce.Floor.Add((Land)CardsForTurn2Player2[2]);
            Bryce.Floor.Add((Land)CardsForTurn2Player2[3]);
            Bryce.Hand.RemoveAt(7);
            Bryce.Hand.RemoveAt(6);
            Bryce.Hand.RemoveAt(5);
            Bryce.Hand.RemoveAt(4);




            //Set the turn to 2
            Battle.GetInstance().Turn += 2;

            //Start of Turn 2


            //Go to prepState
            Battle.GetInstance().State.NextState();
            //DrawState
            Battle.GetInstance().State.NextState();
            //Attack State
            Battle.GetInstance().State.NextState();

            Arold.PlayCard(CardsForTurn2Player1[2]);
            Arold.PlayCard(CardsForTurn2Player1[3]);

            Bryce.SkipCounter();
            //EndState
            Battle.GetInstance().State.NextState();

            //PrepState
            Battle.GetInstance().State.NextState();

            //DrawState
            Battle.GetInstance().State.NextState();
            //Attack State
            Battle.GetInstance().State.NextState();

            Bryce.PlayCard(artefact1);

            Arold.SkipCounter();
            //EndState
            Battle.GetInstance().State.NextState();

            //PrepState
            Battle.GetInstance().State.NextState();

            //DrawState
            Battle.GetInstance().State.NextState();
            //Arold wasnt allowed to grap a card because the artefact
            Arold.Hand.RemoveAt(Arold.Hand.Count - 1);
            //Attack State
            Battle.GetInstance().State.NextState();

            Arold.Floor[Arold.Floor.IndexOf((Creature)CardsForTurn2Player1[3])].UseCard();
            Arold.PlayCard(CardsForTurn2Player1[4]);
            Bryce.PlayCard(counterspell);
            Arold.PlayCard(redcounter);
            Bryce.SkipCounter();
            Bryce.Life += 2;

            //EndState
            Battle.GetInstance().State.NextState();

            //PrepState
            Battle.GetInstance().State.NextState();

            Bryce.Floor.Remove((Artefact)artefact1);

            //DrawState
            Battle.GetInstance().State.NextState();

            //Attack State
            Battle.GetInstance().State.NextState();
            Bryce.SkipCounter();

            //EndState
            Battle.GetInstance().State.NextState();

            //Ending the game
            Battle.GetInstance().State = new FinishedState(Arold);

            //Break point
            Console.WriteLine("");




        }
    }
}
