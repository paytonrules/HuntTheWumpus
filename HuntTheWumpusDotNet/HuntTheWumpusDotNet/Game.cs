using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet
{
    public class Game
    {
        private Map map;
        private Dictionary<int, List<MapItems>> players;
        private Presenter presenter;
        
        public Game(Presenter presenter)
        {
            ClearMap(); 
            this.presenter = presenter;
        }

        public int PlayersCurrentCavern
        {
            get { return players.First(cavern => cavern.Value.Contains(MapItems.Player)).Key; }
        }

        public int Quiver { get; set; }

        public void PutInCavern(MapItems mapItem, int cavern)
        {
            if (mapItem == MapItems.Player && PlayerIsPlaced())
                RemovePlayerFrom(PlayersCurrentCavern);

            if (!players.ContainsKey(cavern))
                players[cavern] = new List<MapItems>();
            players[cavern].Add(mapItem);

        }

        private bool PlayerIsPlaced()
        {
            return players.Any(player => player.Value.Contains(MapItems.Player));
        }

        private void RemovePlayerFrom(int cavern)
        {
            players[cavern].Remove(MapItems.Player);
        }
        
        public void AddPath(int start, int end, Command.AllCommands direction)
        {
            map.AddPath(start, end, direction);
        }

        public bool Do(Command.AllCommands command)
        {
            if (Command.IsMove(command))
            {
                if (map.CanMove(PlayersCurrentCavern, command))
                {
                    var newCavern = map.GetCavernForMove(PlayersCurrentCavern, command);
                    players.Remove(PlayersCurrentCavern);
                    if (!players.ContainsKey(newCavern))
                        players[newCavern] = new List<MapItems>();
                    players[newCavern].Add(MapItems.Player);
                }
                else
                {
                    presenter.InvalidMove(command);
                    return false;
                }
            }
            presenter.DisplayAvailableMoves(map.AvailableMoves(PlayersCurrentCavern));

            if (CanSmellTheWumpus())
            {
                presenter.WumpusCanSeeYou();
            }
            return true;
        }

        public bool CanSmellTheWumpus()
        {
            var availableMoves = map.AvailableMoves(PlayersCurrentCavern);
            return availableMoves.Any(MapHasWumpus);
        }

        private bool MapHasWumpus(Command.AllCommands command)
        {
            var potentialCavern = map.GetCavernForMove(PlayersCurrentCavern, command);
            return players.ContainsKey(potentialCavern) && 
                players[potentialCavern].Contains(MapItems.Wumpus);
        }

        public void ClearMap()
        {
            map = new Map();
            players = new Dictionary<int, List<MapItems>>();
        }

        public List<MapItems> GetPlayersInCavern(int cavern)
        {
            if (players.ContainsKey(cavern))
                return players[cavern];
            return null;
        }

        public int ArrowsInCavern(int cavern)
        {
            return 0;
        }
    }
}
