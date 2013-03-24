using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet
{
    public class Game : GameController
    {
        private Map map;
        private readonly Presenter presenter;
        private readonly HumanPlayer player;
        private bool gameRunning;
        public WumpusMover Mover { get; set; }
        
        public Game(Presenter presenter, Map map)
        {
            this.map = map;
            this.presenter = presenter;
            player = new HumanPlayer();
            Mover= new RandomWumpusMover();
            gameRunning = true;
        }

        public int? PlayersCurrentCavern
        {
            get { return map.PlayersCurrentCavern(); }
        }

        private bool PlayerIsPlaced()
        {
            return map.HasPlayer();
        }

        public bool Command(Command command)
        {
           if (command.Order != HuntTheWumpusDotNet.Command.Commands.Invalid)
            {
                if (command.Order == HuntTheWumpusDotNet.Command.Commands.Shoot)
                {
                    TryToShootArrow(command.Direction);
                }
                else if (command.Order == HuntTheWumpusDotNet.Command.Commands.Go)
                {
                    return MovePlayer(command.Direction);
                }
                else if (command.Order == HuntTheWumpusDotNet.Command.Commands.Rest)
                {
                    Rest();
                }

                if (gameRunning && map.WumpusCavern != null)
                    map.PlaceItem(Mover.Move(map), MapItems.Wumpus);
     
                return true;
            }

            return false;
        }

        protected bool MovePlayer(Command.Directions command)
        {
            if (PlayersCurrentCavern != null)
            {
                if (map.CanMove((int) PlayersCurrentCavern, command))
                {
                    map.MovePlayer(command);
                }
                else
                {
                    presenter.InvalidDirection(command);
                    return false;
                }
            }

            DisplayAvailableDirections();

            if (CanSmellTheWumpus())
            {
                presenter.WumpusCanSeeYou();
            }

            PickUpAnyArrows();
            DisplayArrowStatus();

            return true;
        }

        protected void TryToShootArrow(Command.Directions direction)
        {
            if (player.OutOfArrows())
            {
                presenter.OutOfArrows();
                return;
            }

            if (PlayerIsPlaced())
            {
                player.Shoot();
                var arrowsCavern = FlyArrowToCavern(direction);

                if (arrowsCavern == PlayersCurrentCavern)
                {
                    presenter.Suicide();
                    GameOver();
                }
                else if (map.HasWumpusIn(arrowsCavern))
                {
                    presenter.WumpusHasBeenShot();
                    GameOver();
                }
                else
                    presenter.ArrowWasFired();
            }
        }

        private void GameOver()
        {
            gameRunning = false;
            presenter.GameOver();
        }

        private int FlyArrowToCavern(Command.Directions direction)
        {
            var arrowsCavern = (int) PlayersCurrentCavern;

            while (map.AvailableMoves(arrowsCavern).Contains(direction) &&
                   !map.HasWumpusIn(arrowsCavern))
            {
                arrowsCavern = map.GetCavernForMove(arrowsCavern, direction);
            }
            map.PlaceItem(arrowsCavern, MapItems.Arrow);
            return arrowsCavern;
        }

        protected void Rest()
        {
            DisplayAvailableDirections();
            DisplayArrowStatus();
        }

        protected void DisplayAvailableDirections()
        {
            if (PlayersCurrentCavern != null)
            {
                presenter.DisplayAvailableDirections(map.AvailableMoves((int) PlayersCurrentCavern));
            }
        }

        protected void DisplayArrowStatus()
        {
            presenter.DisplayArrowStatus(player.Quiver);
        }

        protected bool CanSmellTheWumpus()
        {
            if (PlayersCurrentCavern == null)
                return false;
            var availableMoves = map.AvailableMoves((int) PlayersCurrentCavern);
            return availableMoves.Any(MoveRunsIntoWumpus);
        }

        protected bool MoveRunsIntoWumpus(Command.Directions command)
        {
            var potentialCavern = map.GetCavernForMove((int) PlayersCurrentCavern, command);

            return map.HasWumpusIn(potentialCavern);
        }

        protected void PickUpAnyArrows()
        {
            if (PlayerIsPlaced() && map.HasArrowIn((int)map.PlayersCurrentCavern()))
            {
                player.PickUpArrow();
                presenter.FoundAnArrow();
            }
        }

        public void SetPlayerQuiver(int arrows)
        {
            player.Quiver = arrows;
        }

        public int PlayerArrows()
        {
            return player.Quiver;
        }

        public bool IsOver()
        {
            return !gameRunning;
        }

        public void Restart()
        {
            map.Clear();
            gameRunning = true;
            presenter.Restart();
        }
    }
}
