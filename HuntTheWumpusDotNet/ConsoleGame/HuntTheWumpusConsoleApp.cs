using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuntTheWumpusDotNet;
using HuntTheWumpusDotNet.Interactors;

namespace ConsoleGame
{
    class ConsoleDisplay : Display
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void Restart()
        {
        }
    }

    class HuntTheWumpusConsoleApp
    {
        private Display consoleDisplay;
        private Presenter gamePresenter;
        private Map map;
        private MapEditor editor;
        private Game game;
        
        static void Main(string[] args)
        {
            var game = new HuntTheWumpusConsoleApp();
            game.Start();
        }

        public void Start()
        {
            consoleDisplay = new ConsoleDisplay();
            gamePresenter = new GamePresenter(consoleDisplay);
            map = new Map();

            SetupMap();

            game = new Game(gamePresenter, map);
            gamePresenter.Game = game;

            RunGameLoop();
        }

        private void SetupMap()
        {
            editor = new MapEditor(map);
            editor.AddPath(1, 2, 'E');
            editor.AddPath(2, 3, 'E');
            editor.AddPath(3, 4, 'E');
            editor.AddPath(4, 5, 'E');
            editor.AddPath(6, 7, 'S');
            editor.AddPath(7, 3, 'S');
            editor.AddPath(3, 8, 'S');
            editor.AddPath(8, 9, 'S');
            editor.PutInCavern("Wumpus", 9);
            editor.PutInCavern("Arrow", 3);
            editor.PutInCavern("Arrow", 5);
            editor.PutInCavern("Arrow", 2);
            editor.PutInCavern("Arrow", 7);
            editor.PutInCavern("Player", 1);
        }

        private void RunGameLoop()
        {
            Console.WriteLine("Start Hunting The Wumpus");
            while (!game.IsOver())
            {
                var input = Console.ReadLine();

                gamePresenter.CommandPlayer(input);
            }
#if DEBUG
            Console.WriteLine("Press any key to close...");
            Console.ReadLine();
#endif
        }
    }
}
