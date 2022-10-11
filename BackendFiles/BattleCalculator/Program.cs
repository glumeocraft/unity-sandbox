using System;
using System.Collections.Generic;

namespace BattleCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {

            /* Console.WriteLine("Please enter the number of users: ");
            var userNumber = Console.ReadLine();

            Console.WriteLine("Enter the number of unit for user 1#");
            var user1NumberOFUnit = int.Parse(Console.ReadLine());

            for (int i = 0; i < user1NumberOFUnit; i++)
            {
                var unit = new Unit(i);
                Console.WriteLine("Enter the attributes of the #" + i + "unit, enter it's Attack value:");
                unit.Attack = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the attributes of the #" + i + "unit, enter it's Defense value:");
                unit.Defense = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the attributes of the #" + i + "unit, enter it's Special value:");
                unit.Special = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the attributes of the #" + i + "unit, enter it's Direction value:");
                unit.Direction = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the attributes of the #" + i + "unit. Enter it's NewEntry true or false?:");
                unit.NewEntry = bool.Parse(Console.ReadLine());

                // mit csinálsz velük

                Console.WriteLine("THANKS NEXT USER%%!!!!");
            }

            Console.WriteLine("Enter the number of unit for user 2#");
            var user2NumberOFUnit = int.Parse(Console.ReadLine());

            for (int i = 0; i < user2NumberOFUnit; i++)
            {
                var unit = new Unit(i);
                Console.WriteLine("Enter the attributes of the #" + i + "unit, enter it's Attack value:");
                unit.Attack = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the attributes of the #" + i + "unit, enter it's Defense value:");
                unit.Defense = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the attributes of the #" + i + "unit, enter it's Special value:");
                unit.Special = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the attributes of the #" + i + "unit, enter it's Direction value:");
                unit.Direction = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the attributes of the #" + i + "unit. Enter it's NewEntry true or false?:");
                unit.NewEntry = bool.Parse(Console.ReadLine());

                // mit csinálsz velük
                Console.WriteLine("THANKS NEXT USER%%!!!!");
            }
            */
            var sampleUnit1 = new Unit(1);
            var sampleUnit2 = new Unit(2);
            var turn = 0;
            var firstBattle = new Battle();
            do
            {
                turn++;
                firstBattle.SimulateBattle(new List<Unit>() { sampleUnit1 }, new List<Unit> { sampleUnit2 });
                Console.WriteLine("Unit1's HP: " + sampleUnit1.Hp + " Unit2's HP: " + sampleUnit2.Hp);
                Console.WriteLine("Turn #:  " + turn);
                if (sampleUnit1.Hp <= 0)
                {
                    Console.WriteLine("Player 1 Died!");
                }
                if (sampleUnit2.Hp <= 0)
                {
                    Console.WriteLine("Player 2 Died!");
                }
            } while (sampleUnit1.Hp > 0 && sampleUnit2.Hp > 0);
            Console.ReadLine();


        }

        public class Unit
        {
            public Unit(int userId)
            {
                UserId = userId;
            }

            public int Attack { get; set; } = 2;
            public int Defense { get; set; } = 1;
            public int Armor { get; set; } = 1;
            public int Special { get; set; } = 1;
            public int Hp { get; set; } = 10;
            public int UserId { get; set; }
            public string Egyeb { get; set; }
            public bool NewEntry { get; set; } = false;
            public int Direction { get; set; } = 1;
            public Moves CurrentMove { get; set; } = Moves.Attack;

        }

        public enum Moves
        {
            Attack = 1,
            Defense = 2,
            Special = 3,
            Retreat = 4
        }
        public enum Terrain
        {
            Plain = 1,
            Mountain = 2,
            Marsh = 3,
            Camp = 4
        }

    }
}
