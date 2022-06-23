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
            var CardsForTurn2Player2 = new List<Card>();


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
                        p.ThrowACard();
                        Console.WriteLine(p.UserName + " just lost a card, after a blue slime appeared");
                    }
                ));
            Card blueslimeball = new Spell(new Effect(() =>
            {
                foreach(var card in Battle.GetInstance().getPlayerOfTurn().Floor)
                {
                    if( card is Creature)
                    {
                        Creature c = (Creature)card;
                        c.Defence += 3;
                        c.Attack += 3;
                    }
                }
    
            }), 2, new BlueFactory().BlendColor(), "Blue Slimeball Ball"); //We made the spell blue because otherwise there are not enough lands

            player1Deck.AttachCard(blueslime);
            CardsForTurn2Player1.Add(blueslime);

            player1Deck.AttachCard(blueslimeball);
            CardsForTurn2Player1.Add(blueslimeball);

            for (int i = 0; i < 25; i++)
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

            Card artefact1 = new Artefact(new Effect(() => {/* actual implementation wasn't necessary for this assignment */ }), new Effect(() => {/* actual implementation wasn't necessary for this assignment */ }), false);
            player2Deck.AttachCard(artefact1);
            CardsForTurn2Player2.Add(artefact1);

            Card counterspell = new Spell(new Effect(() => {/* actual implementation wasn't necessary for this assignment */ }), 1, new RedFactory().BlendColor(), "Counter");
            player2Deck.AttachCard(counterspell);
            CardsForTurn2Player2.Add(counterspell);
            //Player Init

            Player Arold = new Player("Arold", player1Deck);
            Player Bryce = new Player("Bryce", player1Deck);

            Battle.GetInstance().AddPlayer(Arold);
            Battle.GetInstance().AddPlayer(Bryce);

            //Start the game
            Battle.GetInstance().StartGame(7, 10);


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
            if (Bryce.Hand.Contains(CardsForTurn2Player1[2]))
            {
                Bryce.GrabACard();
                Bryce.Hand.Remove(CardsForTurn2Player1[2]);

            }
            if (Bryce.Hand.Contains(CardsForTurn2Player1[3]))
            {
                Bryce.GrabACard();
                Bryce.Hand.Remove(CardsForTurn2Player1[3]);

            }
            if (Bryce.Hand.Contains(CardsForTurn2Player1[4]))
            {
                Bryce.GrabACard();
                Bryce.Hand.Remove(CardsForTurn2Player1[4]);

            }

            Arold.Floor.Add((PermantantCard)CardsForTurn2Player1[0]);
            Arold.Floor.Add((PermantantCard)CardsForTurn2Player1[1]);



            //After turn 1 arold had 6 cards in his hand so we remove 1 card
            Arold.Hand.RemoveAt(6);

            //Set the turn to 2
            Battle.GetInstance().Turn += 2;

            //Start of Turn 2


            //Go to prepState
            Battle.GetInstance().State.NextState();
            //DrawState
            Battle.GetInstance().State.NextState();
            //Attack State
            Battle.GetInstance().State.NextState();

            Arold.PlayCard(CardsForTurn2[2]);


            Arold.PlayCard(CardsForTurn2[3]);
            Bryce.SkipCounter();
            //EndState
            Battle.GetInstance().State.NextState();

            //PrepState
            Battle.GetInstance().State.NextState();

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
