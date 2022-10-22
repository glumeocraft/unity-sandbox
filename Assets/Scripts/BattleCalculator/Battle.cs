using System;
using System.Collections.Generic;
using static BattleCalculator.Program;
using UnityEngine;

namespace BattleCalculator
{
    public class Battle : MonoBehaviour
    {
        System.Random random = new System.Random();
        public Program.Terrain Terrain { get; set; } = Program.Terrain.Plain;

        public List<List<IUnit>> SimulateBattle(List<IUnit> firstPlayerUnits, List<IUnit> secondPlayerUnits)
        {
            // Get AttackStrength
            var player1AttackPower = SumAttackPower(firstPlayerUnits);
            Console.WriteLine("Player1AttackPower: " + player1AttackPower);
            var player2AttackPower = SumAttackPower(secondPlayerUnits);
            Console.WriteLine("Player2AttackPower " + player2AttackPower);

            // Get DefenseStrength
            var player1DefensePower = SumDefensePower(firstPlayerUnits);
            Console.WriteLine("Player1DefensePower: " + player1DefensePower);

            var player2DefensePower = SumDefensePower(secondPlayerUnits);
            Console.WriteLine("Player2DefensePower: " + player2DefensePower);

            // Calculate Attacks, Defense
            var player1AttackRoll = CalculateAttackRoll(player1AttackPower);
            Console.WriteLine("Player1AttackRoll: " + player1AttackRoll);

            var player2AttackRoll = CalculateAttackRoll(player2AttackPower);
            Console.WriteLine("Player2AttackRoll: " + player2AttackRoll);


            var player1DefenseRoll = CalculateDefenseOrArmorRoll(player1DefensePower);
            Console.WriteLine("Player1DefenseRoll: " + player1DefenseRoll);

            var player2DefenseRoll = CalculateDefenseOrArmorRoll(player2DefensePower);
            Console.WriteLine("Player2DefenseRoll: " + player2DefenseRoll);

            // Calculate Hit
            var player1Hit = player1AttackRoll - player2DefenseRoll;

            Console.WriteLine("Player1Hit: " + player1Hit);

            var player2Hit = player2AttackRoll - player1DefenseRoll;
            Console.WriteLine("Player2Hit: " + player2Hit);


            // Calculate Damage
            CalculateDamageForAPlayer(firstPlayerUnits, player2Hit);
            CalculateDamageForAPlayer(secondPlayerUnits, player1Hit);
            return new List<List<IUnit>> { firstPlayerUnits, secondPlayerUnits };
        }

        public int SumAttackPower(List<IUnit> units)
        {
            int attackPower = 0;
            foreach (var unit in units)
            {
                attackPower += unit.AttackValue;
                if (unit.CurrentMove == Moves.Attack)
                {
                    attackPower += 2;
                    attackPower += SuperAttack(unit.UserId);
                    
                }
            }
            if (units.Count > 1)
            {
                var multiUnitModifier = 1.3;
                attackPower = (int)Math.Round((attackPower / units.Count) * multiUnitModifier);
            }
            
            return attackPower;
        }

        private int SuperAttack(int userId)
        {
            var randomRoll = random.Next(1, 101);
            if (randomRoll < 11)
            {
                Console.WriteLine("S S S SUPERATTACK!!! +2 AttackRoll for Player#" + userId);
                return 2;
            }
            return 0;
        }

        public int SumDefensePower(List<IUnit> units)
        {
            int defendingUnitsNumber = 0;
            int defensePower = 0;
            foreach (var unit in units)
            {
                if (unit.CurrentMove == Moves.Defense)
                {
                    defendingUnitsNumber++;
                }
            }
            foreach (var unit in units)
            {
                defensePower += unit.DefenseValue;
                if (unit.CurrentMove == Moves.Defense)
                {
                    defensePower += defendingUnitsNumber;
                }
            }
            defensePower =+ (1 * defendingUnitsNumber);
            if (units.Count > 1)
            {
                var multiUnitModifier = 1.3;
                defensePower = (int)Math.Round((defensePower / units.Count) * multiUnitModifier);
            }

            return defensePower;
        }

        private int CalculateAttackRoll(int attackPower)
        {
            int attackRoll = 0;
            for (int i = 0; i < attackPower; i++)
            {
                attackRoll += random.Next(1, 6);
            }
            return attackRoll;
        }

        private int CalculateDefenseOrArmorRoll(int defensePower)
        {
            int defense = 0;
            for (int i = 0; i < defensePower; i++)
            {
                defense += random.Next(1, 4);
            }
            return defense;
        
        }

        private void CalculateDamageForAPlayer(List<IUnit> units, int enemyHit)
        {
            foreach (var unit in units)
            {
                var armorRoll = unit.ArmorValue;
                if (unit.CurrentMove == Moves.Defense)
                {
                    armorRoll++;
                }
                armorRoll = CalculateDefenseOrArmorRoll(armorRoll);
                var damage = enemyHit - armorRoll;
                Console.WriteLine("Player " + unit.UserId + "'s Damage: " + damage);
                Console.WriteLine("Player " + unit.UserId + "'s ArmorRoll: " + armorRoll);

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
