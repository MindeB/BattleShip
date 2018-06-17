using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("Please enter your name: ");
            string name = "";
            name = Console.ReadLine();
            Player player1 = new Player(name);
            Computer player2 = new Computer(name);
            Game game = new Game(player2, player1);
            game.startgame();
            Coordinates coordinates = new Coordinates();
            int x;
            int y;
            while (player1.ships.Sum(item => item.width) != 0 || player2.ships.Sum(item => item.width) != 0)
            {
                Console.WriteLine("Please choose your coordinates:");
                Console.WriteLine("X: ");
                x = int.Parse(Console.ReadLine());
                Console.WriteLine("Y: ");
                y = int.Parse(Console.ReadLine());

                while (x < 0 || x > 10 || y < 0 || y > 10)
                {
                    Console.WriteLine("Invalid input!");
                    Console.WriteLine("Please choose your coordinates:");
                    Console.WriteLine("X: ");
                    x = int.Parse(Console.ReadLine());
                    Console.WriteLine("Y: ");
                    y = int.Parse(Console.ReadLine());
                }
                coordinates.x = x;
                coordinates.y = y;
                Console.Clear();
                Console.WriteLine(game.Attack(coordinates));
            }
            if (player1.ships.Sum(item => item.width) == 0)
            {
                Console.WriteLine(string.Format("{0} won!"), player1.name);
            }
            else
            {
                Console.WriteLine("Computer won!");
            }
           
        }
    }
}
