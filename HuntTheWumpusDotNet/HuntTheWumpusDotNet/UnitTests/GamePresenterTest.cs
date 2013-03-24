using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NSubstitute;

namespace HuntTheWumpusDotNet.UnitTests
{
    class GamePresenterTest
    {
        [Test]
        public void ItWritesNoArrowsWhenThereAreZeroArrows()
        {
            var console = Substitute.For<Display>();
            var presenter = new GamePresenter(console);

            presenter.DisplayArrowStatus(0);

            console.Received().WriteMessage("You have no arrows.");
        }

        [Test]
        public void ItWritesTheMessageForOneArrow()
        {
            var console = Substitute.For<Display>();
            var presenter = new GamePresenter(console);

            presenter.DisplayArrowStatus(1);

            console.Received().WriteMessage("You have 1 arrow.");
        }

        [Test]
        public void ItWritesTheMessageForManyArrows()
        {
            var console = Substitute.For<Display>();
            var presenter = new GamePresenter(console);

            presenter.DisplayArrowStatus(5);

            console.Received().WriteMessage("You have 5 arrows.");
        }

        [Test]
        public void ItCommandsShootingAnArrow()
        {
            var console = Substitute.For<Display>();
            var game = Substitute.For<GameController>();
            var presenter = new GamePresenter(console) {Game = game};

            presenter.CommandPlayer("shoot n");

            game.Received().Command(
                Arg.Is<Command>(com => com.Direction == Command.Directions.North && 
                                com.Order == Command.Commands.Shoot));
        }

        [Test]
        public void ItCommandsShootingAnArrowInTheRightDirection()
        {
            var console = Substitute.For<Display>();
            var game = Substitute.For<GameController>();
            var presenter = new GamePresenter(console) {Game = game};

            presenter.CommandPlayer("shoot e");

            game.Received().Command(Arg.Is<Command>(com => com.Direction == Command.Directions.East));
        }     
    }
}
