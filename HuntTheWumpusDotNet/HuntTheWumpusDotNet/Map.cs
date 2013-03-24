using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet
{
    class Cavern
    {
        public List<MapItems> MapItems;
        public Dictionary<Command.Directions, int> Paths;

        public Cavern()
        {
            MapItems = new List<MapItems>();
            Paths = new Dictionary<Command.Directions, int>();
        }
    }

    public class Map
    {
        private Dictionary<int, Cavern> cave;

        public Map()
        {
            cave = new Dictionary<int, Cavern>();
        }

        public int? WumpusCavern
        {
            get
            {
                if (cave.Any(cav => cav.Value.MapItems.Contains(MapItems.Wumpus)))
                {
                    var cavern =
                        cave.First(cav => cav.Value.MapItems.Contains(MapItems.Wumpus));
                    return cavern.Key;
                }
                return null;
            }
        }

        public int MinCavern
        {
            get { return cave.Keys.Min(); }
        }

        public int MaxCavern
        {
            get { return cave.Keys.Max();  }
        }

        public void AddPath(int start, int end, Command.Directions direction)
        {
            CavePathsFor(start)[direction] = end;
            CavePathsFor(end)[OppositeDirectionOf(direction)] = start;
        }

        public bool CanMove(int cavern, Command.Directions direction)
        {
            return CavePathsFor(cavern).ContainsKey(direction);
        }

        public int GetCavernForMove(int cavern, Command.Directions direction)
        {
            return CavePathsFor(cavern)[direction];
        }

        public List<Command.Directions> AvailableMoves(int cavern)
        {
            var paths = CavePathsFor(cavern);
            return new List<Command.Directions>(paths.Keys);
        }

        protected Dictionary<Command.Directions, int> CavePathsFor(int cavern)
        {
            if (CavernDoesntExist(cavern))
            {
               cave[cavern] = new Cavern();
            }
            return cave[cavern].Paths;
        }

        private bool CavernDoesntExist(int cavern)
        {
            return !cave.ContainsKey(cavern);
        }

        protected Command.Directions OppositeDirectionOf(Command.Directions direction)
        {
            switch (direction)
            {
                case Command.Directions.West:
                    return Command.Directions.East;
                case Command.Directions.East:
                    return Command.Directions.West;
                case Command.Directions.South:
                    return Command.Directions.North;
                case Command.Directions.North:
                    return Command.Directions.South;
                default:
                    return Command.Directions.East;
            }
        }

        public void PlaceItem(int cavern, MapItems item)
        {
            if (CavernDoesntExist(cavern))
                cave[cavern] = new Cavern();

            if (HasPlayer() && item == MapItems.Player)
                cave[(int) PlayersCurrentCavern()].MapItems.Remove(MapItems.Player);

            if (WumpusCavern != null && item == MapItems.Wumpus)
                cave[(int) WumpusCavern].MapItems.Remove(MapItems.Wumpus);

            cave[cavern].MapItems.Add(item);
        }

        public bool HasPlayer()
        {
            return cave.Any(item => item.Value.MapItems.Contains(MapItems.Player));
        }

        public int? PlayersCurrentCavern()
        {
            var cavern = cave.FirstOrDefault(item => item.Value.MapItems.Contains(MapItems.Player));
            if (!cavern.Equals(default(KeyValuePair<int, Cavern>)))
            {
                return cavern.Key;
            }

            return null;
        }

        public void MovePlayer(Command.Directions cavern)
        {
            if (HasPlayer())
            {
                var originalCavern = (int) PlayersCurrentCavern();

                if (cave[originalCavern].Paths.ContainsKey(cavern))
                {
                    var newCavern = cave[originalCavern].Paths[cavern];

                    cave[originalCavern].MapItems.Remove(MapItems.Player);
                    cave[newCavern].MapItems.Add(MapItems.Player);
                }
            }
        }

        public bool HasArrowIn(int cavern)
        {
            if (cave.ContainsKey(cavern))
                return cave[cavern].MapItems.Contains(MapItems.Arrow);                

            return false;
        }

        public bool HasWumpusIn(int cavern)
        {
            if (cave.ContainsKey(cavern))
                return cave[cavern].MapItems.Contains(MapItems.Wumpus);

            return false;
        }

        public List<MapItems> ItemsInCavern(int cavernNum)
        {
            if (cave.ContainsKey(cavernNum))
                return new List<MapItems>(cave[cavernNum].MapItems);

            return new List<MapItems>();
        }

        public int ArrowsInCavern(int cavern)
        {
            if (cave.ContainsKey(cavern))
            {
                return cave[cavern].MapItems.Count(item => item == MapItems.Arrow);
            }
            return 0;
        }

        public void Clear()
        {
            foreach (var cavern in cave)
            {
                cavern.Value.MapItems.Clear();
            }
        }
    }
}
