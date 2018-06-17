using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    class Game
    {
        public Computer player1 { get; set; }
        public Player player2 { get; set; }
        public int computerships { get; set; }
        public int playerships { get; set; }

        public Game(Computer player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            computerships = 17;
            playerships = 17;
        }


        public void startgame()
        {
            player1.RandomShipsGenerator();
            player2.RandomShipsGenerator();
            player1.playermap = player2.playermap;
            player2.computermap = player1.computermap;
        }

        public string Attack(Coordinates coordinates)
        {
            string log = "";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(String.Format("Player took a shot at: {0}, {1} \n", coordinates.x, coordinates.y));
            Sea sea = player2.computermap.search(coordinates.x, coordinates.y);
            if (sea.status == 'O')
            {
                
                player2.attackingmap.replacevalue(sea, 'm');
                stringBuilder.Append("Player Missed \n");
            }
            if (sea.status != 'O')
            {
                stringBuilder.Append("Player hit a ship! \n");
                foreach (var ship in player2.ships)
                {
                    if (ship.identity == sea.status)
                    {
                        ship.leftwidth();
                        if (ship.width == 0)
                        {
                            stringBuilder.Append("You have sunk computer's ship \n");
                        }
                    }
                }
                player2.attackingmap.replacevalue(sea, 'X');
            }
            stringBuilder.Append(player1.Attack());
            stringBuilder.Append("\n");
            stringBuilder.Append(player2.DrawingMap());
            stringBuilder.Append("\n");
            
            return log = stringBuilder.ToString();
        }


    }
}
