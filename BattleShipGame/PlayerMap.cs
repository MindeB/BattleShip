using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    public class PlayerMap
    {
        public List<Sea> sea { get; set; }

        public PlayerMap()
        {
            sea = new List<Sea>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    sea.Add(new Sea(i,j));
                }
            }

        }

        public Sea search(int x, int y)
        {
            Sea s1 = new Sea();
            foreach (var s in sea)
            {
                if (s.coordinates.x == x && s.coordinates.y == y)
                {
                    s1 = s;
                    break;
                }
            }
            return s1;
        }

        public void replacevalue(Sea s1, char status)
        {
            foreach (var s in sea)
            {
                if (s1.coordinates.x == s.coordinates.x && s1.coordinates.y == s.coordinates.y)
                {
                    s.status = status;
                    break;
                }
            }
        }

    }
}
