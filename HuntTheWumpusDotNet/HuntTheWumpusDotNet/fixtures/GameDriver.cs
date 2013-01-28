using System;
using System.Collections.Generic;
using System.Linq;

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
            if (messages.Count > 0)
                return messages.Last();
            return "";
        }
    }

    public class GameDriver
    {
        public static Game WumpusGame;
        public static GamePresenter Presenter;
        public static GameEditor Editor;
        private readonly MockDisplay mockDisplay;

        public GameDriver()
        {
            mockDisplay = new MockDisplay();
            Presenter = new GamePresenter(mockDisplay);
            WumpusGame = new Game(Presenter);
            Editor = new GameEditor(WumpusGame);
            Presenter.Game = WumpusGame;
        }

        public void putInCavern(String player, int cavern)
        {
            Editor.PutInCavern(player, cavern);
        }

        public bool enterCommand(String command)
        {
            return Presenter.CommandPlayer(command);
        }

        public String cavernHas(int cavern)
        {
            var players = WumpusGame.GetPlayersInCavern(cavern);
            if (players != null)
                return String.Format("{0}", String.Join(",", players.ConvertAll(item => item.ToString()).ToArray()));
            return "";
        }

        public void clearMap()
        {
            WumpusGame.ClearMap();
        }

        public String messageWasPrinted()
        {
            return mockDisplay.LastMessage();
        }

        public void freezeWumpus()
        {
            
        }

        public void setQuiverTo(int num)
        {
            WumpusGame.Quiver = num;
        }
    }
}