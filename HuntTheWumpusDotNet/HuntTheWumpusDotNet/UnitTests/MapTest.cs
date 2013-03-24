using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace HuntTheWumpusDotNet.UnitTests
{
    class MapTest
    {
        [Test]
        public void ItDetectsIfTheMapHasAPlayer()
        {
            var map = new Map();

            map.AddPath(0, 1, Command.Directions.West);
            map.PlaceItem(0, MapItems.Player);

            Assert.IsTrue(map.HasPlayer());
        }

        [Test]
        public void ItDetectsIfTheMapDoesntHaveAPlayer()
        {
            var map = new Map();

            map.AddPath(0, 1, Command.Directions.West);

            Assert.IsFalse(map.HasPlayer());
        }

        [Test]
        public void ItReturnsNullIfThereIsNoCavernWithAPlayerInIt()
        {
            var map = new Map();

            Assert.IsNull(map.PlayersCurrentCavern());
        }

        [Test]
        public void ItReturnsThePlayersCurrentCavernIfThereIsOne()
        {
            var map = new Map();

            map.PlaceItem(1, MapItems.Player);

            Assert.AreEqual(1, map.PlayersCurrentCavern());
        }

        [Test]
        public void ItCanMoveThePlayer()
        {
            var map = new Map();
            map.AddPath(0, 1, Command.Directions.South);
            map.PlaceItem(0, MapItems.Player);

            map.MovePlayer(Command.Directions.South);

            Assert.AreEqual(1, map.PlayersCurrentCavern());
        }

        [Test]
        public void ItCantMoveThePlayerWhenThereIsNoPlayer()
        {
            var map = new Map();
            map.AddPath(0, 1, Command.Directions.South);

            map.MovePlayer(Command.Directions.South);

            Assert.IsNull(map.PlayersCurrentCavern());
        }

        [Test]
        public void ItCantMoveThePlayerAnInvalidDirection()
        {
            var map = new Map();
            map.AddPath(0, 1, Command.Directions.East);
            map.PlaceItem(0, MapItems.Player);

            map.MovePlayer(Command.Directions.South);

            Assert.AreEqual(0, map.PlayersCurrentCavern());
        }

        [Test]
        public void ItCanSayIfItAHasAnArrowInACavern()
        {
            var map = new Map();
           
            Assert.IsFalse(map.HasArrowIn(1));

            map.PlaceItem(1, MapItems.Arrow);
            Assert.IsTrue(map.HasArrowIn(1));
        }

        [Test]
        public void ItDoesntMistakeAnEmptyCaveForAnArrow()
        {
            var map = new Map();
            map.AddPath(0, 1, Command.Directions.West);

            Assert.IsFalse(map.HasArrowIn(0));
        }

        [Test]
        public void ItDoesntMistakeAnyMapItemForAnArrow()
        {
            var map = new Map();
            map.PlaceItem(1, MapItems.Wumpus);

            Assert.IsFalse(map.HasArrowIn(1));
        }

        [Test]
        public void ItMovesThePlayerIfThePlayerIsPlacedAgain()
        {
            var map = new Map();
            map.PlaceItem(0, MapItems.Player);
            map.PlaceItem(1, MapItems.Player);

            Assert.AreEqual(1, map.PlayersCurrentCavern());
        }

        [Test]
        public void ItCanSayIfItAHasAWumpusInACavern()
        {
            var map = new Map();
           
            Assert.IsFalse(map.HasWumpusIn(1));

            map.PlaceItem(1, MapItems.Wumpus);
            Assert.IsTrue(map.HasWumpusIn(1));
        }

        [Test]
        public void ItDoesntMistakeAnEmptyCaveForAWumpus()
        {
            var map = new Map();
            map.AddPath(0, 1, Command.Directions.West);

            Assert.IsFalse(map.HasWumpusIn(0));
        }

        [Test]
        public void ItDoesntMistakeAnyMapItemForAWumpus()
        {
            var map = new Map();
            map.PlaceItem(1, MapItems.Arrow);

            Assert.IsFalse(map.HasWumpusIn(1));
        }

        [Test]
        public void ItReturnsTheListOfItemsInACavern()
        {
            var map = new Map();

            map.PlaceItem(1, MapItems.Arrow);
            map.PlaceItem(1, MapItems.Arrow);
            map.PlaceItem(1, MapItems.Wumpus);

            var items = map.ItemsInCavern(1);

            var expectedMapItems = new List<MapItems>
                                       {
                                           MapItems.Arrow,
                                           MapItems.Arrow,
                                           MapItems.Wumpus
                                       };
            Assert.IsTrue(items.SequenceEqual(expectedMapItems));
        }

        [Test]
        public void ItReturnsAnImmutableListOfItems()
        {
            var map = new Map();

            map.PlaceItem(1, MapItems.Arrow);

            var items = map.ItemsInCavern(1);
            items.Add(MapItems.Player);

            Assert.AreEqual(1, map.ItemsInCavern(1).Count);
        }

        [Test]
        public void ItReturnsAnEmptyListIfTheCavernDoesntExist()
        {
            var map = new Map();

            var items = map.ItemsInCavern(1);

            Assert.AreEqual(0, items.Count);
        }

        [Test]
        public void ItReturnsTheNumberOfArrowsInACavern()
        {
            var map = new Map();

            var count = map.ArrowsInCavern(0);
            Assert.AreEqual(0, count);

            map.PlaceItem(0, MapItems.Arrow);
            map.PlaceItem(0, MapItems.Arrow);
            
            count = map.ArrowsInCavern(0);
            Assert.AreEqual(2, count);
        }

        [Test]
        public void ItClearsTheItemsOnClear()
        {
            var map = new Map();

            map.PlaceItem(2, MapItems.Player);
            map.PlaceItem(1, MapItems.Wumpus);

            map.Clear();

            Assert.AreEqual(0, map.ItemsInCavern(1).Count);
            Assert.AreEqual(0, map.ItemsInCavern(2).Count);
        }

        [Test]
        public void ItDoesntClearThePathsOnClear()
        {
            var map = new Map();

            map.AddPath(0, 1, Command.Directions.East);

            map.Clear();

            Assert.AreEqual(Command.Directions.East, map.AvailableMoves(0)[0]);
        }

        [Test]
        public void ItOnlyAllowsOneWumpus()
        {
            var map = new Map();

            map.PlaceItem(1, MapItems.Wumpus);
            map.PlaceItem(2, MapItems.Wumpus);

            Assert.IsFalse(map.HasWumpusIn(1));
            Assert.IsTrue(map.HasWumpusIn(2));
        }

        [Test]
        public void ItAllowsAnotherItemWithTheWumpus()
        {
             var map = new Map();

            map.PlaceItem(1, MapItems.Wumpus);
            map.PlaceItem(1, MapItems.Arrow);

            Assert.IsTrue(map.HasWumpusIn(1));
            Assert.AreEqual(2, map.ItemsInCavern(1).Count);
        }
        
        [Test]
        public void ItStoresItsLowestNumberedCavern()
        {
            var map = new Map();

            map.AddPath(3, 4, Command.Directions.East);
            map.PlaceItem(2, MapItems.Player);

            Assert.AreEqual(2, map.MinCavern);
        }

        [Test]
        public void ItRetrievesItsHighestNumberedCavern()
        {
            var map = new Map();

            map.AddPath(3, 4, Command.Directions.East);
            map.PlaceItem(2, MapItems.Player);

            Assert.AreEqual(4, map.MaxCavern);
        }
    }
}
