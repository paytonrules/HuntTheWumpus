using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet.fixtures
{
    public class MakeMap
    {
        public int Start { get; set; }
        public int End { get; set; }
        public char Direction { get; set; }

        public void Execute()
        {
            GameDriver.Presenter.AddPath(Start, End, Direction);
        }
    }
}