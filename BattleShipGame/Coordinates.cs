using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    public class Coordinates
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordinates()
        {
        }

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


    }
}
