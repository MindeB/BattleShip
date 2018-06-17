using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    public class Sea
    {
        public Coordinates coordinates { get; set; }
        public char status { get; set; }

        public Sea()
        {
            
        }

        public Sea(int x, int y)
        {
            coordinates = new Coordinates(x,y);
            status = 'O';
        }

        public Sea(int x, int y, char status)
        {
            coordinates = new Coordinates(x, y);
            this.status = status;
        }


    }
}
