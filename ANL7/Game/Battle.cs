using ANL7.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANL7
{
    class Battle
    {
        private static Battle Instance;

        public Player[] players = new Player[2];
        public int Turn = 2;
        public int PlayerTurn = 0;
        public List<Card> Stack = new List<Card>();
        public BattleState State;

        public static Battle GetInstance()
        {
            if (Instance == null)
                Instance = new Battle();

            return Instance;
        }

        private Battle(){}

        /// <summary>
        /// Reset the stack
        /// </summary>
        public void FlushStack()
        {
            Stack = new List<Card>();
        }

        /// <summary>
        /// Resolve all the cards in the stack
        /// </summary>
        public void NotifyStack()
        {
            foreach(Card card in Stack)
            {
                card.UseCardInStack();
                this.NextPlayer();
            }
            this.NextPlayer();
            this.FlushStack();
        }

        /// <summary>
        /// Get the player of the turn
        /// </summary>
        /// <returns></returns>
        public Player getPlayerOfTurn()
        {
            return this.players[this.PlayerTurn];
        }

        /// <summary>
        /// Give the turn to the other player
        /// </summary>
        /// <returns></returns>
        public Player NextPlayer()
        {
            this.PlayerTurn++;
            if (PlayerTurn > this.players.Length - 1)
                this.PlayerTurn = 0;

            return (getPlayerOfTurn());
        }

        /// <summary>
        /// Adds a player to the game
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(Player player)
        {
            if (this.players[0] == null)
                this.players[0] = player;
            else
                this.players[1] = player;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(player.UserName + " joined the game!");
            Console.ForegroundColor = ConsoleColor.White;

        }

        /// <summary>
        /// Starts the game
        /// </summary>
        /// <param name="startAmoutCard"></param>
        /// <param name="Life"></param>
        public void StartGame(int startAmoutCard, int Life)
        {
            if (players[0] == null && players[1] == null)
                Console.WriteLine("There are not enough players!");
            else
                this.State = new InitState(startAmoutCard, Life);
        }
}
}
