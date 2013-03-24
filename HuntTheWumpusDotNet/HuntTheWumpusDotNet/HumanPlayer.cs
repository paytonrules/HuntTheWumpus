using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet
{
    class HumanPlayer
    {
        public int Quiver { get; set; }

        public bool OutOfArrows()
        {
            return (Quiver <= 0);
        }

        public void Shoot()
        {
            Quiver--;
        }

        public void PickUpArrow()
        {
            Quiver++;
        }
    }
}
