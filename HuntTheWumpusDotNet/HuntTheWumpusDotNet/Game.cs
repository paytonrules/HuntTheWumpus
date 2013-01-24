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
        private Presenter presenter;

        public Game(Presenter presenter)
        {
            map = new Map();
            this.presenter = presenter;
        }

        public void PutPlayerInCavern(int cavern)
        {
            CurrentCavern = cavern;
        }

        public void AddPath(int start, int end, Command.AllCommands direction)
        {
            map.AddPath(start, end, direction);
        }

        public bool Do(Command.AllCommands command)
        {
            if (IsMove(command))
            {
                if (map.CanMove(CurrentCavern, command))
                {
                    CurrentCavern = map.GetCavernForMove(CurrentCavern, command);
                    return true;
                }
                else
                {
                    presenter.InvalidMove(command);
                    return false;
                }
            }
            return true;
        }

        public bool IsMove(Command.AllCommands command)
        {
            return command >= Command.AllCommands.East && command <= Command.AllCommands.South;
        }
    }
}
