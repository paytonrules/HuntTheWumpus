using System;
using System.Collections.Generic;

namespace HuntTheWumpusDotNet
{
    public class GamePresenter : Presenter
    {
        public Game Game { get; set; }
        private readonly Display console;

        public GamePresenter(Display console)
        {
            this.console = console;
        }

        public void AddPath(int start, int end, char direction)
        {
            Game.AddPath(start, end, ConvertCharToDirection(direction));
        }

        public bool CommandPlayer(String commandString)
        {
            var command = ConvertStringToCommand(commandString);
            
            if (command == Command.AllCommands.Invalid)
            {
                console.WriteMessage(String.Format("I don't know how to {0}.", commandString));
                return false;
            }

            return Game.Do(command);
        }

        public void InvalidMove(Command.AllCommands command)
        {
            var message = String.Format("You can't go {0} from here.", ConvertDirectionToString(command));
            console.WriteMessage(message);
        }

        public void DisplayAvailableMoves(List<Command.AllCommands> availableMoves)
        {
            console.WriteMessage(String.Format("You can go {0} from here.", MovesAsString(availableMoves)));
        }

        public void WumpusCanSeeYou()
        {
            console.WriteMessage("You smell the Wumpus.");
        }

        private static String MovesAsString(List<Command.AllCommands> availableMoves)
        {
            var availableMovesAsStrings = availableMoves.ConvertAll(ConvertDirectionToString);
            switch(availableMovesAsStrings.Count)
            {
                case 1:
                    return String.Format("{0}", availableMovesAsStrings[0]);
                case 2:
                    return String.Format("{0} and {1}", 
                                            availableMovesAsStrings[0],
                                            availableMovesAsStrings[1]);
                case 3:
                    return String.Format("{0}, {1} and {2}", 
                                            availableMovesAsStrings[0],
                                            availableMovesAsStrings[1], 
                                            availableMovesAsStrings[2]);
                case 4:
                    return String.Format("{0}, {1}, {2} and {3}", 
                                            availableMovesAsStrings[0],
                                            availableMovesAsStrings[1], 
                                            availableMovesAsStrings[2],
                                            availableMovesAsStrings[3]);
                default:
                    return "";
            }
        }

        private static Command.AllCommands ConvertStringToCommand(String direction)
        {
            var convertedString = direction.Trim().ToUpperInvariant();
            if (convertedString.StartsWith("GO "))
                convertedString = convertedString.Substring(3);

            var convertedChar = convertedString[0];
            return ConvertCharToDirection(convertedChar);
        }

        private static Command.AllCommands ConvertCharToDirection(char direction)
        {
            switch (direction)
            {
                case 'E':
                    return Command.AllCommands.East;
                case 'W':
                    return Command.AllCommands.West;
                case 'N':
                    return Command.AllCommands.North;
                case 'S':
                    return Command.AllCommands.South;
                case 'R':
                    return Command.AllCommands.Rest;
                default:
                    return Command.AllCommands.Invalid;
            }
        }

        private static String ConvertDirectionToString(Command.AllCommands direction)
        {
            switch (direction)
            {
                case Command.AllCommands.East:
                    return "east";
                case Command.AllCommands.West:
                    return "west";
                case Command.AllCommands.North:
                    return "north";
                case Command.AllCommands.South:
                    return "south";
                default:
                    throw new ArgumentException("Invalid Direction");
            }
        }
    }
}