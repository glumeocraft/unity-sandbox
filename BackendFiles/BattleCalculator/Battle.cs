using System;
using System.Collections.Generic;
using static BattleCalculator.Program;

namespace BattleCalculator
{
    public class Battle
    {
        Random random = new Random();
        public Terrain Terrain { get; set; } = Terrain.Plain;

        public void SimulateBattle(List<Unit> firstPlayerUnits, List<Unit> secondPlayerUnits)
        {
            // Get AttackStrength
            var player1AttackStrength = SumAttackRoll(firstPlayerUnits);
            Console.WriteLine("Player1AttackRoll: " + player1AttackStrength);
            var player2AttackStrength = SumAttackRoll(secondPlayerUnits);
            Console.WriteLine("Player2AttackRoll: " + player2AttackStrength);

            // Get DefenseStrength
            var player1DefenseStrength = SumDefenseRoll(firstPlayerUnits);
            Console.WriteLine("Player1DefenseRoll: " + player1DefenseStrength);

            var player2DefenseStrength = SumDefenseRoll(secondPlayerUnits);
            Console.WriteLine("Player2DefenseRoll: " + player2DefenseStrength);

            // Calculate Attacks, Defense
            var player1Attack = CalculateAttack(player1AttackStrength);
            Console.WriteLine("Player1Attack: " + player1Attack);

            var player2Attack = CalculateAttack(player2AttackStrength);
            Console.WriteLine("Player2Attack: " + player2Attack);


            var player1Defense = CalculateDefenseOrArmor(player1DefenseStrength);
            Console.WriteLine("Player1Defense: " + player1Defense);

            var player2Defense = CalculateDefenseOrArmor(player2DefenseStrength);
            Console.WriteLine("Player2Defense: " + player2Defense);

            // Calculate Hit
            var player1Hit = player1Attack - player2Defense;

            Console.WriteLine("Player1Hit: " + player1Hit);

            var player2Hit = player2Attack - player1Defense;
            Console.WriteLine("Player2Hit: " + player2Hit);


            // Calculate Damage
            CalculateDamageForAPLayer(firstPlayerUnits, player2Hit);
            CalculateDamageForAPLayer(secondPlayerUnits, player1Hit);

        }

        public int SumAttackRoll(List<Unit> units)
        {
            int attackRoll = 0;
            foreach (var unit in units)
            {
                attackRoll += unit.Attack;
                if (unit.CurrentMove == Moves.Attack)
                {
                    attackRoll += 2;
                }
            }
            return attackRoll;
        }

        public int SumDefenseRoll(List<Unit> units)
        {
            int defendingUnitsNumber = 0;
            int defenseRoll = 0;
            foreach (var unit in units)
            {
                defenseRoll += unit.Defense;
                if (unit.CurrentMove == Moves.Defense)
                {
                    defendingUnitsNumber++;
                }
            }
            defenseRoll += defendingUnitsNumber;
            return defenseRoll;
        }

        private int CalculateAttack(int attackStrength)
        {
            int attack = 0;
            for (int i = 0; i < attackStrength; i++)
            {
                attack += random.Next(1, 6);
            }
            return attack;
        }

        private int CalculateDefenseOrArmor(int defenseStrength)
        {
            int defense = 0;
            for (int i = 0; i < defenseStrength; i++)
            {
                defense += random.Next(1, 4);
            }
            return defense;
        
        }

        private void CalculateDamageForAPLayer(List<Unit> units, int enemyHit)
        {
            foreach (var unit in units)
            {
                var resistance = unit.Armor;
                if (unit.CurrentMove == Moves.Defense)
                {
                    resistance++;
                }
                resistance = CalculateDefenseOrArmor(resistance);
                var damage = enemyHit - resistance;
                Console.WriteLine("Player " + unit.UserId + "'s Damage: " + damage);
                Console.WriteLine("Player " + unit.UserId + "'s Resistance: " + resistance);

                // Damaging unit
                if (damage < 0)
                {
                    damage = 0;
                }
                unit.Hp -= damage;
                Console.WriteLine("Player " + unit.UserId + "'s remaining HP is: " + unit.Hp);
            }
        }
    }
}
