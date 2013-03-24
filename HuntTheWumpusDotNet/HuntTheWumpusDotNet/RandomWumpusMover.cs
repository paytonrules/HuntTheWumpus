using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet
{
    class RandomWumpusMover : WumpusMover
    {
        private Random generator;

        public RandomWumpusMover()
        {
            generator = new Random(); 
        }

        public int Move(Map map)
        {
            return generator.Next(map.MinCavern, map.MaxCavern + 1);
        }
    }
}
