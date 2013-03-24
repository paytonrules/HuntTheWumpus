using System;
using System.Collections.Generic;

namespace HuntTheWumpusDotNet
{

    public class GamePresenter : Presenter
    {
        public GameController Game { get; set; }
        private readonly Display console;

        public GamePresenter(Display console)
        {
            this.console = console;
        }

        public bool CommandPlayer(String commandString)
        {
            var command = ConvertStringToCommand(commandString);
            var succesfulCommand = Game.Command(command);
            if (!succesfulCommand)
                console.WriteMessage(String.Format("I don't know how to {0}.", commandString));

            return succesfulCommand;
        }

        public void InvalidDirection(Command.Directions command)
        {
            var message = String.Format("You can't go {0} from here.", ConvertDirectionToString(command));
            console.WriteMessage(message);
        }

        public void DisplayAvailableDirections(List<Command.Directions> availableMoves)
        {
            console.WriteMessage(String.Format("You can go {0} from here.", MovesAsString(availableMoves)));
        }

        public void WumpusCanSeeYou()
        {
            console.WriteMessage("You smell the Wumpus.");
        }

        public void FoundAnArrow()
        {
            console.WriteMessage("You found an arrow.");
        }

        public void ArrowWasFired()
        {
            console.WriteMessage("The arrow flies away in silence.");
        }

        public void OutOfArrows()
        {
            console.WriteMessage("You don't have any arrows.");
        }

        public void Suicide()
        {
            console.WriteMessage("The arrow bounced off the wall and killed you.");
        }

        public void GameOver()
        {
            console.WriteMessage("Game over.");
        }

        public void Restart()
        {
            console.Restart();
        }

        public void WumpusHasBeenShot()
        {
            console.WriteMessage("You have killed the Wumpus.");
        }

        public void DisplayArrowStatus(int arrows)
        {
            switch(arrows)
            {
                case 0:
                    console.WriteMessage("You have no arrows.");
                    break;
                case 1:
                    console.WriteMessage("You have 1 arrow.");
                    break;
                default:
                    console.WriteMessage(String.Format("You have {0} arrows.", arrows));
                    break;
            }
            
        }

        private static String MovesAsString(List<Command.Directions> availableMoves)
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

        private static Command ConvertStringToCommand(String direction)
        {
            var command = new Command();
            var convertedString = direction.Trim().ToUpperInvariant();
            if (convertedString.StartsWith("GO "))
            {
                command.Order = Command.Commands.Go;
                var directionString = convertedString.Substring(3);
                var convertedChar = directionString[0];
                command.Direction = ConvertCharToDirection(convertedChar);
                return command;
            }

            if (convertedString.StartsWith("SHOOT "))
            {
                command.Order = Command.Commands.Shoot;
                var directionString = convertedString.Substring(6);
                var convertedChar = directionString[0];
                command.Direction = ConvertCharToDirection(convertedChar);
                return command;
            }

            if (convertedString.StartsWith("S ") && convertedString.Substring(2).Length > 0)
            {
                command.Order = Command.Commands.Shoot;
                var directionString = convertedString.Substring(2);
                var convertedChar = directionString[0];
                command.Direction = ConvertCharToDirection(convertedChar);
                return command;
            }

            if (convertedString.StartsWith("S") 
                && convertedString.Length > 1
                && convertedString[1] != 'O')
            {
                command.Order = Command.Commands.Shoot;
                var convertedChar = convertedString[1];
                command.Direction = ConvertCharToDirection(convertedChar);
                return command;               
            }

            if ( convertedString.StartsWith("S") ||
                convertedString.StartsWith("W") ||
                convertedString.StartsWith("N") ||
                convertedString.StartsWith("E"))
            {
               command.Order = Command.Commands.Go;
               command.Direction = ConvertCharToDirection(convertedString[0]);
            }

            if (convertedString.StartsWith("R"))
            {
                command.Order = Command.Commands.Rest;
            }

            return command;
        }

        public static Command.Directions ConvertCharToDirection(char direction)
        {
            switch (Char.ToUpper(direction))
            {
                case 'E':
                    return Command.Directions.East;
                case 'W':
                    return Command.Directions.West;
                case 'N':
                    return Command.Directions.North;
                case 'S':
                    return Command.Directions.South;
                default:
                    return Command.Directions.Invalid;
            }
        }

        private static String ConvertDirectionToString(Command.Directions direction)
        {
            switch (direction)
            {
                case Command.Directions.East:
                    return "east";
                case Command.Directions.West:
                    return "west";
                case Command.Directions.North:
                    return "north";
                case Command.Directions.South:
                    return "south";
                default:
                    throw new ArgumentException("Invalid Direction");
            }
        }
    }
}