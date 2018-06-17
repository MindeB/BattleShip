using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    class Computer
    {
        public string name { get; set; }
        public PlayerMap playermap { get; set; }
        public PlayerMap computermap { get; set; }
        public List<Ship> ships { get; set; }
        public List<Sea> aroundtarget { get; set; }
        public List<Sea> directionalAttack { get; set; }
        public Computer(string name)
        {
            this.name = name;
            playermap = new PlayerMap();
            ships = new List<Ship>();
            directionalAttack = new List<Sea>();
            aroundtarget = new List<Sea>();
            computermap = new PlayerMap();
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
                            sea = computermap.search(startrow, startcolumn);
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
                                computermap.replacevalue(s, ship.identity);
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
                            sea = computermap.search(startrow, startcolumn);
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
                                computermap.replacevalue(s, ship.identity);
                                isFree = false;
                            }
                        }
                    }


                }
            }

        }

        public List<Sea> CoordinatesAroundTarget(Coordinates coordinates)
        {

            int row = coordinates.x;
            int column = coordinates.y;
            List<Sea> listcord = new List<Sea>();
            // Adding window from which we get neighbors
            //----------------------------
            Sea sea = new Sea(row, column);
            listcord.Add(sea);
            // ---------------------------
            Sea s = new Sea();
            if (column > 1)
            {
                s = playermap.search(row, column - 1);
                if (s.status != 'm')
                    listcord.Add(s);

            }
            if (row > 1)
            {
                s = playermap.search(row - 1, column);
                if (s.status != 'm')
                    listcord.Add(s);
            }
            if (row < 10)
            {
                s = playermap.search(row + 1, column);
                if (s.status != 'm')
                    listcord.Add(s);
            }
            if (column < 10)
            {
                s = playermap.search(row, column + 1);
                if (s.status != 'm')
                    listcord.Add(s);
            }
            return listcord;
        }


        public string DrawingMap()
        {
            string builder;
            StringBuilder stringBuilder = new StringBuilder();
            Sea sea = new Sea();
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
                    stringBuilder.Append(sea.status + " ");
                    if (j == 10)
                        stringBuilder.Append("\n");
                }

            }
            return builder = stringBuilder.ToString();

        }

        public Coordinates RandomAttackCoordinates()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int x = rand.Next(1, 11);
            int y = rand.Next(1, 11);
            Coordinates coordinates = new Coordinates(x, y);
            return coordinates;
        }


        public int DirectionOfAttack(Sea s)
        {
            int direction = 0;
            // 1 horizontal 2 vertical
            if (aroundtarget[0].coordinates.x > s.coordinates.x || aroundtarget[0].coordinates.x < s.coordinates.x)
            {
                // vertical
                direction = 2;
            }
            if (aroundtarget[0].coordinates.y > s.coordinates.y || aroundtarget[0].coordinates.y < s.coordinates.y)
            {
                // horizontal
                direction = 1;
            }
            return direction;
        }

        public string SingleShot()
        {
            string log = "";
            StringBuilder stringBuilder = new StringBuilder();
            Coordinates coordinates = new Coordinates();
            Sea sea = new Sea();
            bool isOpen = true;
            while (isOpen)
            {
                coordinates = RandomAttackCoordinates();
                sea = playermap.search(coordinates.x, coordinates.y);
                if (sea.status != 'X' || sea.status != 'm')
                {
                    isOpen = false;
                }
            }
            //log = sea.status.ToString();
            stringBuilder.Append(string.Format("Computer attacked {0}, {1} \n ", sea.coordinates.x, sea.coordinates.y));
            if (sea.status == 'O')
            {
                playermap.replacevalue(sea, 'm');
                stringBuilder.Append("Computer missed \n");
            }
            else if(sea.status != 'O' && sea.status != 'm')
            {
                aroundtarget = CoordinatesAroundTarget(coordinates);
                
                foreach (var ship in ships)
                {
                    if (ship.identity == sea.status)
                    {
                        ship.leftwidth();
                        if (ship.width == 0)
                        {
                            log = "You have sunk my ship";
                            stringBuilder.Append("Computer sunk your ship \n");
                        }
                    }
                }
                playermap.replacevalue(sea, 'X');
                stringBuilder.Append("Computer hit your ship \n");
            }
            return log = stringBuilder.ToString();
        }

        public string AttackWithCoordinates()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            StringBuilder stringBuilder = new StringBuilder();
            string log = "";
            var attackid = rand.Next(aroundtarget.Count); 
            while (attackid == 0)
            {
                attackid = rand.Next(aroundtarget.Count);
            }
            //var attackid = rand.Next(aroundtarget.Count);
            Sea sea = aroundtarget[attackid];
            log = sea.status.ToString();
            if (sea.status == 'O')
            {
                aroundtarget.Remove(aroundtarget[attackid]);
                stringBuilder.Append(string.Format("Computer missed at {0}, {1} \n", sea.coordinates.x, sea.coordinates.y));
                playermap.replacevalue(sea, 'm');

            }
            else if (sea.status != 'm' && sea.status != 'O')
            {

                // 1 horizontal 2 vertical
                foreach (var ship in ships)
                {
                    if (ship.identity == sea.status)
                    {
                        ship.leftwidth();
                        if (ship.width == 0)
                        {
                            log = "Computer have sunk your ship";
                            stringBuilder.Append("Computer have sunk your ship");
                        }
                        break;
                    }
                }
                if (log == "Computer have sunk your ship")
                {
                    playermap.replacevalue(sea, 'X');
                    aroundtarget.Clear();
                }
                if (aroundtarget.Count() > 0)
                {
                    playermap.replacevalue(sea, 'X');
                    stringBuilder.Append(string.Format("Computer hit your ship at {0}, {1} \n", sea.coordinates.x, sea.coordinates.y));
                    int direction = DirectionOfAttack(aroundtarget[attackid]);
                    Sea s = aroundtarget[attackid];
                    Sea s2;
                    switch (direction)
                    {
                        case 1:
                            // Ship max size is 5 so we need to give 4 windows for each side because we don't from which part was hit 

                            for (int i = 0; i < 4; i++)
                            {
                                if (s.coordinates.y > 1)
                                {


                                    
                                    s2 = playermap.search(s.coordinates.x, s.coordinates.y - 1);

                                    if (s2.status != 'm')
                                    {
                                        s = s2;
                                        directionalAttack.Add(s2);
                                    }
                                    else break;
                                }
                            }
                            s = aroundtarget[attackid];
                            for (int i = 0; i < 4; i++)
                            {
                                if (s.coordinates.y < 10)
                                {
                                    s2 = playermap.search(s.coordinates.x, s.coordinates.y + 1);
                                    if (s2.status != 'm')
                                    {
                                        s = s2;
                                        directionalAttack.Add(s2);
                                    }
                                    else break;
                                }
                            }
                            aroundtarget.Clear();
                            directionalAttack.OrderBy(x => x.coordinates.y);
                            break;
                        case 2:
                            s = aroundtarget[attackid];
                            for (int i = 0; i < 4; i++)
                            {
                                if (s.coordinates.x > 1)
                                {
                                    
                                    s2 = playermap.search(s.coordinates.x-1, s.coordinates.y);
                                    if (s2.status != 'm')
                                    {
                                        s = s2;
                                        directionalAttack.Add(s2);
                                    }
                                    else break;
                                }
                            }
                            s = aroundtarget[attackid];
                            for (int i = 0; i < 4; i++)
                            {
                                if (s.coordinates.x < 10)
                                {
                                    
                                    s2 = playermap.search(s.coordinates.x+1, s.coordinates.y);
                                    if (s2.status != 'm')
                                    {
                                        s = s2;
                                        directionalAttack.Add(s2);
                                    }
                                    else break;
                                }
                            }
                            aroundtarget.Clear();
                            directionalAttack.OrderBy(x => x.coordinates.x);
                            break;
                    }
                }
            }
            return log = stringBuilder.ToString();
        }

        public string AttackingDirectional()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string log = "";
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int attackid = 0;

            for (int i = 0; i < directionalAttack.Count; i++)
            {
                if (i == 0 && directionalAttack.Count > 1)
                {
                    if (directionalAttack[i + 1].status == 'X')
                    {
                        attackid = i;
                    }
                }
                else if(i > 0 && i < directionalAttack.Count-1)
                {
                    if (directionalAttack[i + 1].status == 'X' || directionalAttack[i - 1].status == 'X')
                    {
                        attackid = i;
                    }
                }
            }
             
            Sea sea = directionalAttack[attackid];
            if (sea.status == 'O')
            {
                playermap.replacevalue(sea, 'm');
                directionalAttack.Remove(directionalAttack[attackid]);
                log = "Computer missed ";
                stringBuilder.Append(string.Format("Computer missed at {0}, {1}", sea.coordinates.x, sea.coordinates.y));
            }
            else
            {
                log = sea.status.ToString();
                foreach (var ship in ships)
                {
                    if (ship.identity == sea.status)
                    {
                        ship.leftwidth();
                        if (ship.width == 0)
                        {
                            directionalAttack.Clear();
                            log = String.Format("Computer have sunk your {0} ship", ship.name);
                            stringBuilder.Append(log);
                        }
                        break;
                    }
                    
                }
                playermap.replacevalue(sea, 'X');
                if (directionalAttack.Count != 0) 
                directionalAttack.Remove(directionalAttack[attackid]);
            }
            return log = stringBuilder.ToString();
        }

        public string Attack()
        {
            string log = "";
            if (aroundtarget.Count == 0 && directionalAttack.Count == 0)
            {
                log = SingleShot();
            }
            else if (aroundtarget.Count > 0)
            {
                log = AttackWithCoordinates();
            }
            else if (directionalAttack.Count > 0)
            {
                log = AttackingDirectional();
            }
            return log;
        }

    }


}