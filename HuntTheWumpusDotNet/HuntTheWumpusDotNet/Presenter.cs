﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet
{
    public interface Presenter
    {
        void InvalidMove(Command.AllCommands command);
    }
}
