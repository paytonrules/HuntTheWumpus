using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet.fixtures
{
    public class GameEditor
    {
        private Game game;
        public GameEditor(Game game)
        {
            this.game = game;
        }

        public void PutInCavern(String player, int cavern)
        {
            game.PutInCavern((MapItems) Enum.Parse(typeof (MapItems), Capitalize(player)), cavern);
        }

        static private String Capitalize(String player)
        {
            return char.ToUpper(player[0]) + player.Substring(1);
        }
    }
}
