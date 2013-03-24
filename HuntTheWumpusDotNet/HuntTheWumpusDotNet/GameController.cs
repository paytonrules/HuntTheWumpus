using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet
{
    public interface GameController
    {
        bool Command(Command command);
   }
}
