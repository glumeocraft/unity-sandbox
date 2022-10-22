﻿using System;
using System.Collections.Generic;

namespace BattleCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {

            // TEszt:
            // lefuttatjuk a harcok x szer, beadva hogy mit csinál végig, és végeredményben átlagban mennyi lett a:
            // nulla sebzés, nulla sebzés mind2nél, 1 körös kill (fatality), hány körig tartot, átlag hp különbség, hp difference
            // specialt lefejleszteni!
            //teszt eset amokikor előre megadjuk, hogy mik a lépések
            // sok játékosnál összekéne adni az attackRollokat *0.65% és defenseRoll is!
            // Creating Armies
            var sampleUnit1 = new convictor(1);
            var player1Army = new List<Unit>() { sampleUnit1 };
            var sampleUnit2 = new doogariteAcolytes(2);
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

        public class Unit : IUnit
        {
            public Unit(int userId)
            {
                UserId = userId;
            }

            public int AttackValue { get; set; } = 3;
            public int DefenseValue { get; set; } = 2;
            public int ArmorValue { get; set; } = 1;
            public int SpecialValue { get; set; } = 1;
            public int Hp { get; set; } = 30;
            public int UserId { get; set; }
            public string Egyeb { get; set; }
            public bool NewEntry { get; set; } = false;
            public int Direction { get; set; } = 1;
            public Moves CurrentMove { get; set; } = Moves.Attack;

        }

        public class doogariteAcolytes : Unit
        {
            public doogariteAcolytes(int userId) : base(userId)
            {
                
                AttackValue= 8;
                DefenseValue = 2;
                ArmorValue = 3;
            }
        }

        public class praetorians : Unit
        {
            public praetorians(int userId) : base(userId)
            {
                AttackValue = 6;
                DefenseValue = 4;
                ArmorValue = 3;
            }
        }
        public class convictor : Unit
        {
            public convictor(int userId) : base(userId)
            {
                AttackValue = 10;
                DefenseValue = 3;
                ArmorValue = 6;
            }
        }

        public class avaregeJoe : Unit
        {
            public avaregeJoe(int userId) : base(userId)
            {
                AttackValue = 7;
                DefenseValue = 3;
                ArmorValue = 4;
            }
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