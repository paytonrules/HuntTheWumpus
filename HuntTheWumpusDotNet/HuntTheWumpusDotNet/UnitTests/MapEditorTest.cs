using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuntTheWumpusDotNet.Interactors;
using NUnit.Framework;

namespace HuntTheWumpusDotNet.UnitTests
{
    class MapEditorTest
    {
        [Test]
        public void ItPutsAnItemInItsMap()
        {
            var map = new Map();
            var editor = new MapEditor(map);

            editor.PutInCavern("Wumpus", 1);

            Assert.AreEqual(MapItems.Wumpus, map.ItemsInCavern(1)[0]);
        }

        [Test]
        public void ItAddsAPathUsingACharacter()
        {
            var map = new Map();
            var editor = new MapEditor(map);

            editor.AddPath(0, 1, 'w');

            Assert.AreEqual(Command.Directions.West, map.AvailableMoves(0)[0]);
        }
    }
}
