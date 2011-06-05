using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JabboServer.Game.Rooms.Pathfinding
{
    class Coord
    {
        private int iX;
        private int iY;

        internal int X
        {
            get
            {
                return iX;
            }
            set
            {
                iX = value;
            }
        }

        internal int Y
        {
            get
            {
                return iY;
            }
            set
            {
                iY = value;
            }
        }

        internal Coord(int X, int Y)
        {
            iX = X;
            iY = Y;
        }

        internal Coord()
        {
            iX = 0;
            iY = 0;
        }
    }
}
