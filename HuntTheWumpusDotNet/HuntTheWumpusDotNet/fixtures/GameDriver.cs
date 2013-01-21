using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fit;

namespace HuntTheWumpusDotNet.fixtures
{
    public class GameDriver : ActionFixture
    {
        public static Game WumpusGame;
        public static GamePresenter Presenter;

        public GameDriver()
        {
            WumpusGame = new Game();
            Presenter = new GamePresenter(WumpusGame);
        }

        public void putInCavern(String player, int cavern)
        {
            WumpusGame.PutPlayerInCavern(cavern);
        }

        public void enterCommand(char command)
        {
            Presenter.Move(command);
        }

        public bool cavernHas(int cavern, String player)
        {
            return cavern == WumpusGame.CurrentCavern;
        }
    }
}