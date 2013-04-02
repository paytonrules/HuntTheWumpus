using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet
{
    public interface Presenter
    {
        void InvalidDirection(Command.Directions command);
        void DisplayAvailableDirections(List<Command.Directions> availableMoves);
        void WumpusCanSeeYou();
        void DisplayArrowStatus(int numArrows);
        void FoundAnArrow();
        void ArrowWasFired();
        void OutOfArrows();
        void Suicide();
        void GameOver();
        void Restart();
        void WumpusHasBeenShot();
        GameController Game { get; set; }
        bool CommandPlayer(String commandString);
    }
}
