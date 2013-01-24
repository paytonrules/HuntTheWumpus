using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fitlibrary;

namespace HuntTheWumpusDotNet.fixtures
{
    public class MockDisplay : Display
    {
        public List<String> messages;

        public MockDisplay()
        {
            messages = new List<String>();
        }

        public void WriteMessage(string message)
        {
            messages.Add(message);
        }

        public String LastMessage()
        {
            return messages.Last();
        }
    }

    public class GameDriver : DoFixture
    {
        public static Game WumpusGame;
        public static GamePresenter Presenter;
        private readonly MockDisplay mockDisplay;

        public GameDriver()
        {
            mockDisplay = new MockDisplay();
            Presenter = new GamePresenter(mockDisplay);
            WumpusGame = new Game(Presenter);
            Presenter.Game = WumpusGame;
        }

        public void putInCavern(String player, int cavern)
        {
            WumpusGame.PutPlayerInCavern(cavern);
        }

        public bool enterCommand(String command)
        {
            return Presenter.CommandPlayer(command);
        }

        public String cavernHas(int cavern)
        {
            if (cavern == WumpusGame.CurrentCavern)
                return "player";

            return "";
        }

        public String messageWasPrinted()
        {
            return mockDisplay.LastMessage();
        }
    }
}