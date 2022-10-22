using System;
using System.Collections.Generic;

namespace BattleCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Creating Armies
            var sampleUnit1 = new Unit(1);
            var player1Army = new List<Unit>() { sampleUnit1 };
            var sampleUnit2 = new Unit(2);
            var player2Army = new List<Unit>() { sampleUnit2 };
            var participants = new List<List<Unit>> { player1Army, player2Army };
            var turn = 0;
            var firstBattle = new Battle();

            do
            {
                foreach (var player in participants)
                {
                    PromptForMove(player);
                }

                turn++;
                firstBattle.SimulateBattle(player1Army, player2Army);
                Console.WriteLine("Unit1's HP: " + sampleUnit1.Hp + " Unit2's HP: " + sampleUnit2.Hp);
                Console.WriteLine("Turn number: " + turn);
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

        private static void PromptForMove(List<Unit> army)
        {
            int i = 1;
            foreach (var unit in army)
            {
                Console.WriteLine("Enter Player " + unit.UserId + "'s #" + i + " unit's next move: ");
                unit.CurrentMove = (Moves)int.Parse(Console.ReadLine());
                i++;
            }
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
            public int Hp { get; set; } = 30;
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
