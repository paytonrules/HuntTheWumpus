using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet.fixtures
{
    public class RandomWumpusMovement
    {
        public int Cavern { get; set; }
        public int Count { get; set; }

        public void Execute()
        {
            Count = 0;
            for (var i = 0; i < 1000; i++)
            {
                GameDriver.Map.PlaceItem(2, MapItems.Wumpus);
                GameDriver.WumpusGame.Command(new Command
                                                  {
                                                      Order = Command.Commands.Rest
                                                  });

                if (GameDriver.Map.HasWumpusIn(Cavern))
                    Count++;
            }
        }
    }
}
