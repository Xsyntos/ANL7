using ANL7.Cards;
using ANL7.Game;
using System;
using System.Collections.Generic;

namespace ANL7
{
    class Program
    {
        static void Main(string[] args)
        {
            //Just a reference for the cards that are needed for turn 2
            var CardsForTurn2 = new List<Card>();
            bool NotAdded = true;

            //Preparing Deck for player 1
            Deck player1Deck = new Deck();

            for(int i = 0; i < 3; i++ )
            {
                Card c = new Land(Color.Blue);
                player1Deck.AttachCard(c);
                CardsForTurn2.Add(c);
            }
            for (int i = 0; i < 3; i++)
            {
                Card c = new Creature(2,2,Color.Blue,2,"Blue slime",
                    new Effect(()=> {
                        Player p = Battle.GetInstance().getPlayerOfTurn().getOpponent();
                        p.ThrowACard();
                        Console.WriteLine(p.UserName + " just lost a card, after a blue slime appeared");
                    }
                ));
                player1Deck.AttachCard(c);
                if (NotAdded)
                {
                    CardsForTurn2.Add(c);
                    NotAdded = false;
                }
            }

            for (int i = 0; i < 24; i++)
            {
                Card c = new Spell(new Effect(()=> 
                {
                    Console.WriteLine("Hey! I am Mr Mee6! Existance is Pain!");
                }),
                1,Color.Blue, "Mee6");
                player1Deck.AttachCard(c);
            }

            Deck player2Deck = new Deck();

            for (int i = 0; i < 29; i++)
            {
                Card c = new Spell(new Effect(() =>
                {
                    Console.WriteLine("Hey! I am Mr Mee6! Existance is Pain!");
                }),
                1, Color.Blue, "Mee6");
                player2Deck.AttachCard(c);
            }

            Card d = new Land(Color.Blue);
            player2Deck.AttachCard(d);
            CardsForTurn2.Add(d);

            //Player Init

            Player Arold = new Player("Arold", player1Deck);
            Player Bryce = new Player("Bryce", player1Deck);

            Battle.GetInstance().AddPlayer(Arold);
            Battle.GetInstance().AddPlayer(Bryce);

            //Start the game
            Battle.GetInstance().StartGame(7, 10);


            //Cheating the land cards to the floor
            if (Arold.Hand.Contains(CardsForTurn2[0]))
            {
                Arold.GrabACard();
                Arold.Hand.Remove(CardsForTurn2[0]);
            }

            if (Arold.Hand.Contains(CardsForTurn2[1]))
            {
                Arold.GrabACard();
                Arold.Hand.Remove(CardsForTurn2[1]);

            }
            if (Bryce.Hand.Contains(CardsForTurn2[4]))
            {
                Bryce.GrabACard();
                Bryce.Hand.Remove(CardsForTurn2[4]);

            }
            Arold.Floor.Add((PermantantCard)CardsForTurn2[0]);
            Arold.Floor.Add((PermantantCard)CardsForTurn2[1]);
            Bryce.Floor.Add((PermantantCard)CardsForTurn2[4]);

            //Cheating the creaturecard 
            if (!Arold.Hand.Contains(CardsForTurn2[3]))
                Arold.Hand[3] = CardsForTurn2[3];
            if (!Arold.Hand.Contains(CardsForTurn2[2]))
                Arold.Hand[2] = CardsForTurn2[2];

            //After turn 1 arold had 6 cards in his hand so we remove 1 card
            Arold.Hand.RemoveAt(6);

            //Set the turn to 2
            Battle.GetInstance().Turn += 2;

            //Start of Turn 2
            Battle b = Battle.GetInstance();


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
