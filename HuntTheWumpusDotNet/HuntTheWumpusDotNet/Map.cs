using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet
{
    public class Map
    {
        private readonly Dictionary<int, Dictionary<Command.AllCommands, int>> cavernPaths;

        public Map()
        {
            cavernPaths = new Dictionary<int, Dictionary<Command.AllCommands, int>>();
        }

        public void AddPath(int start, int end, Command.AllCommands direction)
        {
            CavernPathsFor(start)[direction] = end;
            CavernPathsFor(end)[OppositeDirectionOf(direction)] = start;
        }

        public bool CanMove(int cavern, Command.AllCommands direction)
        {
            return CavernPathsFor(cavern).ContainsKey(direction);
        }

        public int GetCavernForMove(int cavern, Command.AllCommands direction)
        {
            return CavernPathsFor(cavern)[direction];
        }

        public List<Command.AllCommands> AvailableMoves(int cavern)
        {
            var paths = CavernPathsFor(cavern);
            return new List<Command.AllCommands>(paths.Keys);
        }

        protected Dictionary<Command.AllCommands, int> CavernPathsFor(int cavern)
        {
            if (!cavernPaths.ContainsKey(cavern))
            {
               cavernPaths[cavern] = new Dictionary<Command.AllCommands, int>();
            }
            return cavernPaths[cavern];
        }

        protected Command.AllCommands OppositeDirectionOf(Command.AllCommands direction)
        {
            switch (direction)
            {
                case Command.AllCommands.West:
                    return Command.AllCommands.East;
                case Command.AllCommands.East:
                    return Command.AllCommands.West;
                case Command.AllCommands.South:
                    return Command.AllCommands.North;
                case Command.AllCommands.North:
                    return Command.AllCommands.South;
                default:
                    return Command.AllCommands.East;
            }
        }
    }
}
