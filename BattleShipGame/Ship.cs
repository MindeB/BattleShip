using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    public class Ship
    {
        public string name { get; set; }
        public int width { get; set; }
        public int shot { get; set; }
        public char identity { get; set; }

        public Ship(string name, int width, char identity)
        {
            this.name = name;
            this.width = width;
            this.identity = identity;
            shot = 0;
        }

        public void leftwidth()
        {
            width -= 1;
        }




    }
}
