using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet
{
    public class Game
    {
        private readonly Map map;
        public int CurrentCavern { get; set; }

        public Game()
        {
            map = new Map();
        }

        public void PutPlayerInCavern(int cavern)
        {
            CurrentCavern = cavern;
        }

        public void AddPath(int start, int end, Direction direction)
        {
            map.AddPath(start, end, direction);
        }

        public void Move(Direction direction)
        {
            CurrentCavern = map.Move(CurrentCavern, direction);
        }
    }
}
