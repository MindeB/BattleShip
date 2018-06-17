using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    class Player
    {
        public string name { get; set; }
        public PlayerMap playermap { get; set; }
        public PlayerMap attackingmap { get; set; }
        public PlayerMap computermap { get; set; }
        public List<Ship> ships { get; set; }

        public Player(string name)
        {
            this.name = name;
            playermap = new PlayerMap();
            attackingmap = new PlayerMap();
            ships = new List<Ship>();
            ships.Add(new Ship("Carrier", 5, 'C'));
            ships.Add(new Ship("BattleShip", 4, 'B'));
            ships.Add(new Ship("Cruiser", 3, 'R'));
            ships.Add(new Ship("Submarine", 3, 'S'));
            ships.Add(new Ship("Destroyer", 2, 'D'));
        }

        public void RandomShipsGenerator()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            foreach (var ship in ships)
            {

                bool isFree = true;
                while (isFree)
                {
                    int startrow = rand.Next(1, 11);
                    int startcolumn = rand.Next(1, 11);
                    int endrow = startrow;
                    int endcolumn = startcolumn;
                    int orientation = rand.Next(0, 2);
                    // 0 for vertical
                    // 1 for horizontal
                    List<Sea> PositionsForShips = new List<Sea>();
                    int count = 0;
                    if (orientation == 0)
                    {
                        Sea sea = new Sea();
                        for (int i = 1; i <= ship.width; i++)
                        {
                            if (startrow > 10)
                            {
                                break;
                            }
                            sea = playermap.search(startrow, startcolumn);
                            startrow++;
                            // if it is occupied then increases value
                            if (sea.status != 'O') count++;
                            PositionsForShips.Add(sea);
                        }

                        // Checks if there are any occupied spaces and checks if row number didn't go out of boundaries
                        if (count != 0 || PositionsForShips.Count != ship.width)
                        {
                            count = 0;
                            PositionsForShips.Clear();
                        }
                        else
                        {
                            foreach (var s in PositionsForShips)
                            {
                                playermap.replacevalue(s, ship.identity);
                                isFree = false;
                            }
                        }
                    }
                    if (orientation == 1)
                    {
                        Sea sea = new Sea();
                        for (int i = 1; i <= ship.width; i++)
                        {
                            if (startcolumn > 10)
                            {
                                break;
                            }
                            sea = playermap.search(startrow, startcolumn);
                            startcolumn++;
                            // if it is occupied then increases value
                            if (sea.status != 'O') count++;
                            PositionsForShips.Add(sea);
                        }

                        // Checks if there are any occupied spaces and checks if row number didn't go out of boundaries
                        if (count != 0 || PositionsForShips.Count != ship.width)
                        {
                            count = 0;
                            PositionsForShips.Clear();
                        }
                        else
                        {
                            foreach (var s in PositionsForShips)
                            {
                                playermap.replacevalue(s, ship.identity);
                                isFree = false;
                            }
                        }
                    }


                }
            }

        }

        public string DrawingMap()
        {
            string builder;
            StringBuilder stringBuilder = new StringBuilder();
            Sea sea = new Sea();
            stringBuilder.Append("Attacking Map: \n");
            stringBuilder.Append("  1 2 3 4 5 6 7 8 9 10 \n");

            for (int i = 1; i <= 10; i++)
            {
                if (i == 10)
                    stringBuilder.Append(i);
                else
                    stringBuilder.Append(i + " ");
                for (int j = 1; j <= 10; j++)
                {
                    sea = attackingmap.search(i, j);
                    
                    stringBuilder.Append(sea.status + " ");
                    if (j == 10)
                        stringBuilder.Append("\n");
                }

            }
            stringBuilder.Append("\n");
            stringBuilder.Append("Player Map: \n");
            stringBuilder.Append("  1 2 3 4 5 6 7 8 9 10 \n");

            for (int i = 1; i <= 10; i++)
            {
                if (i == 10)
                    stringBuilder.Append(i);
                else
                    stringBuilder.Append(i + " ");
                for (int j = 1; j <= 10; j++)
                {
                    sea = playermap.search(i, j);
                    if (j == 10)
                        stringBuilder.Append(sea.status);
                    else stringBuilder.Append(sea.status + " ");
                    if (j == 10)
                        stringBuilder.Append("\n");
                }

            }
            return builder = stringBuilder.ToString();

        }
    }
}
