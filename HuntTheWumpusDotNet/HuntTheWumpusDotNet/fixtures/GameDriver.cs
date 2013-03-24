using System;
using System.Collections.Generic;
using System.Linq;
using HuntTheWumpusDotNet.Interactors;

namespace HuntTheWumpusDotNet.fixtures
{

    public class GameDriver
    {
        public static Game WumpusGame;
        public static GamePresenter Presenter;
        public static MapEditor Editor;
        public static MockDisplay Display;
        public static Map Map;

        public GameDriver()
        {
            Display = new MockDisplay();
            Presenter = new GamePresenter(Display);
            Map = new Map();
            WumpusGame = new Game(Presenter, Map);
            Editor = new MapEditor(Map);
            Presenter.Game = WumpusGame;
        }
        
        public void RestartGame()
        {
            WumpusGame.Restart();
        }

        public void PutInCavern(String item, int cavern)
        {
            Editor.PutInCavern(item, cavern);
        }

        public bool EnterCommand(String command)
        {
            return Presenter.CommandPlayer(command);
        }

        public string CavernHas(int cavern)
        {
            return String.Join(",",
                       Map.ItemsInCavern(cavern).Select(item => item.ToString()));
        }

        public void freezeWumpus()
        {
            
        }

        public bool MessageWasPrinted(String message)
        {
            return Display.Messages.Contains(message);
        }

        public void SetQuiverTo(int num)
        {
            WumpusGame.SetPlayerQuiver(num);
        }

        public int ArrowsInCavern(int cavern)
        {
            return Map.ArrowsInCavern(cavern);
        }

        public int ArrowsInQuiver()
        {
            return WumpusGame.PlayerArrows();
        }

        public bool GameTerminated()
        {
            return WumpusGame.IsOver();
        }
    }
}