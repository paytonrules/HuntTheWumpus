using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fit;

namespace HuntTheWumpusDotNet.fixtures
{
    public class MakeMap : ColumnFixture
    {
        public int Start { get; set; }
        public int End { get; set; }
        public char Direction { get; set; }

        public override void Execute()
        {
            GameDriver.Presenter.AddPath(Start, End, Direction);
        }
    }
}