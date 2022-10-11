using System;
using System.Collections.Generic;
using System.Text;
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
            var player1AttackStrength = SumStrengthOfAPlayer(firstPlayerUnits, Moves.Attack);
            Console.WriteLine("Player1AttackRoll: " + player1AttackStrength);
            var player2AttackStrength = SumStrengthOfAPlayer(secondPlayerUnits, Moves.Attack);
            Console.WriteLine("Player2AttackRoll: " + player2AttackStrength);

            // Get DefenseStrength
            var player1DefenseStrength = SumStrengthOfAPlayer(firstPlayerUnits, Moves.Defense);
            Console.WriteLine("Player1DefenseRoll: " + player1DefenseStrength);

            var player2DefenseStrength = SumStrengthOfAPlayer(firstPlayerUnits, Moves.Defense);
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

        private int SumStrengthOfAPlayer(List<Unit> units, Moves whatAreWeSuming)
        {
            int baseValue = 0;
            int defendingUnitsNumber = 0;
            foreach (var unit in units)
            {
                if (whatAreWeSuming == Moves.Attack)
                {
                    baseValue += unit.Attack;
                }
                if (whatAreWeSuming == Moves.Defense)
                {
                    baseValue += unit.Defense;
                }
                if (unit.CurrentMove == whatAreWeSuming)
                {
                    baseValue++;

                    if (whatAreWeSuming == Moves.Attack)
                    {
                        baseValue++;
                    }
                }
            }
            if (whatAreWeSuming == Moves.Defense)
            {
                baseValue += defendingUnitsNumber;
            }
            return baseValue;
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
                unit.Hp -= damage;
                Console.WriteLine("Player " + unit.UserId + "'s remaining HP is: " + unit.Hp);

            }
        }
    }
}
