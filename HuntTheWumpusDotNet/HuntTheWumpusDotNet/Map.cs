using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet
{
    public class Map
    {
        private readonly Dictionary<int, Dictionary<Direction, int>> cavernPaths;

        public Map()
        {
            cavernPaths = new Dictionary<int, Dictionary<Direction, int>>();
        }

        public void AddPath(int start, int end, Direction direction)
        {
            if (!cavernPaths.ContainsKey(start))
            {
               cavernPaths[start] = new Dictionary<Direction, int>();
            }
            cavernPaths[start][direction] = end;

            if (!cavernPaths.ContainsKey(end))
            {
               cavernPaths[end] = new Dictionary<Direction, int>();
            }

            cavernPaths[end][OppositeDirectionOf(direction)] = start;
        }

        public int Move(int cavern, Direction direction)
        {
            return cavernPaths[cavern][direction];
        }

        protected Direction OppositeDirectionOf(Direction direction)
        {
            switch (direction)
            {
                case Direction.West:
                    return Direction.East;
                case Direction.East:
                    return Direction.West;
                case Direction.South:
                    return Direction.North;
                case Direction.North:
                    return Direction.South;
                default:
                    return Direction.East;
            }
        }
    }
}
