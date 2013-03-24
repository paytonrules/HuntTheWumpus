using NUnit.Framework;
using NSubstitute;

namespace HuntTheWumpusDotNet.UnitTests
{
    class GameTest
    {
        [Test]
        public void ItGetsItsPlayerCavernFromItsMap()
        {
            
        }

        [Test]
        public void ItAnnouncesWhenAPlayerFindsAnArrow()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
            
            map.AddPath(0, 1, Command.Directions.North);
            map.PlaceItem(0, MapItems.Player);
            map.PlaceItem(1, MapItems.Arrow);
            game.Command(new Command{ Order = Command.Commands.Go, 
                                      Direction = Command.Directions.North});

            presenter.Received().FoundAnArrow();
        }

        [Test]
        public void ItDoesntAnnounceWhenThePlayerDoesNotFindAnArrow()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
            
            map.AddPath(0, 1, Command.Directions.North);
            map.PlaceItem(0, MapItems.Player);
            game.Command(new Command {
                            Direction = Command.Directions.North,
                            Order = Command.Commands.Go});

            presenter.DidNotReceive().FoundAnArrow();           
        }

        [Test]
        public void ItAnnouncesWhenYouCanSmellTheWumpus()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);

            map.AddPath(0, 1, Command.Directions.West);
            map.AddPath(1, 2, Command.Directions.South);
            map.PlaceItem(0, MapItems.Player);
            map.PlaceItem(2, MapItems.Wumpus);

            game.Command(new Command {
                Direction = Command.Directions.West,
                Order = Command.Commands.Go
                });

            presenter.Received().WumpusCanSeeYou();
        }

        [Test]
        public void ItAnnouncesTheNumberOfArrowsWhenThePlayerHasOne()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
 
            map.AddPath(0, 1, Command.Directions.West);
            map.PlaceItem(0, MapItems.Player);
            map.PlaceItem(1, MapItems.Arrow);

            game.Command(new Command {
                Direction = Command.Directions.West,
                Order = Command.Commands.Go});

            presenter.Received().DisplayArrowStatus(1);
        }

        [Test]
        public void ItRemovesAnArrowFromTheQuiverWhenAShotIsFired()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
            game.SetPlayerQuiver(5);

            map.PlaceItem(0, MapItems.Player);
            game.Command( new Command {
                Order  = Command.Commands.Shoot,
                Direction = Command.Directions.East});

            Assert.AreEqual(4, game.PlayerArrows());
        }

        [Test]
        public void ItMovesAnArrowInTheCommandedDirection()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
            game.SetPlayerQuiver(1);

            map.PlaceItem(0, MapItems.Player);
            map.AddPath(0, 1, Command.Directions.East);

            game.Command(new Command {
                Direction = Command.Directions.East, 
                Order = Command.Commands.Shoot});

            Assert.AreEqual(MapItems.Arrow, map.ItemsInCavern(1)[0]);
        }

        [Test]
        public void ItContinuesMovingTheArrowUntilThereAreNoMovesInThatDirection()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
            game.SetPlayerQuiver(1);

            map.PlaceItem(0, MapItems.Player);
            map.AddPath(0, 1, Command.Directions.East);
            map.AddPath(1, 2, Command.Directions.East);

            game.Command( new Command { 
                Order = Command.Commands.Shoot, 
                Direction = Command.Directions.East});

            Assert.AreEqual(MapItems.Arrow, map.ItemsInCavern(2)[0]);           
        }

        [Test]
        public void ItAnnouncesTheArrowWasShot()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
            game.SetPlayerQuiver(1);

            map.PlaceItem(0, MapItems.Player);
            map.AddPath(0, 1, Command.Directions.East);
            
            game.Command(new Command {
                Direction = Command.Directions.East,
                Order = Command.Commands.Shoot});

            presenter.Received().ArrowWasFired();
        }

        [Test]
        public void ItDoesntShootIfThePlayerHasNoArrows()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);

            map.AddPath(0, 1, Command.Directions.East);
            map.PlaceItem(0, MapItems.Player);
            
            game.Command(new Command {
                Direction = Command.Directions.East,
                Order = Command.Commands.Shoot
            });

            presenter.DidNotReceive().ArrowWasFired();           
            Assert.AreEqual(0, map.ArrowsInCavern(1));
        }

        [Test]
        public void ItAnnouncesNoArrowsIfThePlayerHasNone()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);

            map.AddPath(0, 1, Command.Directions.East);
            map.PlaceItem(0, MapItems.Player);
            
            game.Command(new Command {
                Direction = Command.Directions.East, 
                Order = Command.Commands.Shoot});

            presenter.Received().OutOfArrows();
        }

        [Test]
        public void ItAnnouncesTheArrowKilledThePlayerIfHeShootsAWall()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
            game.SetPlayerQuiver(1);

            map.PlaceItem(0, MapItems.Player);

            game.Command(new Command
            {
                Direction = Command.Directions.East,
                Order = Command.Commands.Shoot
            });

            presenter.Received().Suicide();
        }

        [Test]
        public void ItAnnouncesGameOverOnPlayerDeath()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
            game.SetPlayerQuiver(1);

            map.PlaceItem(0, MapItems.Player);

            game.Command(new Command
            {
                Direction = Command.Directions.East,
                Order = Command.Commands.Shoot
            });

            presenter.Received().GameOver();           
        }

        [Test]
        public void ItSetsTheGameToOverOnPlayerDeath()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
            game.SetPlayerQuiver(1);

            map.PlaceItem(0, MapItems.Player);

            Assert.IsFalse(game.IsOver());
            game.Command(new Command
            {
                Direction = Command.Directions.East,
                Order = Command.Commands.Shoot
            });
           
            Assert.IsTrue(game.IsOver());
        }

        protected void KillWumpus(Game game, Map map)
        {
            game.SetPlayerQuiver(1);

            map.PlaceItem(0, MapItems.Player);
            map.PlaceItem(2, MapItems.Wumpus);
            map.AddPath(0, 1, Command.Directions.East);
            map.AddPath(0, 2, Command.Directions.East);

            game.Command(new Command
                             {
                                 Direction = Command.Directions.East,
                                 Order = Command.Commands.Shoot
                             });
        }


        [Test]
        public void ItSetsTheGameToOverOnWumpusDeath()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);

            KillWumpus(game, map);
           
            Assert.IsTrue(game.IsOver());
        }

        [Test]
        public void ItNotifiesThePresenterOfWumpusDeath()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);

            KillWumpus(game, map);

            presenter.Received().WumpusHasBeenShot();
        }

        [Test]
        public void ItAnnouncesGameOverOnWumpusDeath()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);

            KillWumpus(game, map);

            presenter.Received().GameOver();
        }

        [Test]
        public void ItNotifiesThePresenterWhenAGameIsRestarted()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
            game.Restart();

            presenter.Received().Restart();
        }

        [Test]
        public void ItClearsThePlayerPositionOnRestart()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);

            map.PlaceItem(0, MapItems.Player);
            game.Restart();

            Assert.IsNull(game.PlayersCurrentCavern);
        }

        [Test]
        public void TheGameIsNotOverIfItHasBeenRestarted()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var game = new Game(presenter, map);
            game.SetPlayerQuiver(1);
            map.PlaceItem(0, MapItems.Player);

            game.Command(new Command
                             {
                                 Order = Command.Commands.Shoot,
                                 Direction = Command.Directions.East
                             });

            game.Restart();
            Assert.IsFalse(game.IsOver());
        }

        [Test]
        public void ItMovesTheWumpusEachTurnBasedOnWumpusMover()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            map.PlaceItem(2, MapItems.Wumpus);

            var mover = Substitute.For<WumpusMover>();
            mover.Move(map).Returns(4);

            var game = new Game(presenter, map)
                           {
                               Mover = mover
                           };

            game.Command(new Command
                             {
                                 Order = Command.Commands.Rest
                             });

            Assert.IsTrue(map.HasWumpusIn(4));
        }

        [Test]
        public void ItDoesntMoveTheWumpusIfTheWumpusIsntPlaced()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var mover = Substitute.For<WumpusMover>();
            mover.Move(map).Returns(4);
            var game = new Game(presenter, map)
                           {
                               Mover = mover
                           };

            game.Command(new Command
                             {
                                 Order = Command.Commands.Rest
                             });

            Assert.IsFalse(map.HasWumpusIn(4));
        }

        [Test]
        public void ItDoesntMoveIfTheWumpusHasBeenKilled()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();
            var mover = Substitute.For<WumpusMover>();
            mover.Move(map).Returns(5);

            var game = new Game(presenter, map)
                           {
                               Mover = mover
                           };

            KillWumpus(game, map);

            Assert.IsFalse(map.HasWumpusIn(5));
        }

        // It defaults to random wumpus mover
        [Test]
        public void ItDefaultsToARandomWumpusMover()
        {
            var presenter = Substitute.For<Presenter>();
            var map = new Map();

            var game = new Game(presenter, map);

            Assert.IsInstanceOf<RandomWumpusMover>(game.Mover);
        }

    }
}
