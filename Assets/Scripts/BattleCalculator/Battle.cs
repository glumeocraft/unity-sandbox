using System;
using System.Collections.Generic;
using static BattleCalculator.Program;

namespace BattleCalculator
{
    public class Battle
    {
        Random random = new Random();
        public Terrain Terrain { get; set; } = Terrain.Plain;

        public List<List<IUnit>> SimulateBattle(List<IUnit> firstPlayerUnits, List<IUnit> secondPlayerUnits)
        {
            // Get AttackPower
            var player1AttackPower = SumAttackPower(firstPlayerUnits);
            Console.WriteLine("Player1AttackPower: " + player1AttackPower);
            var player2AttackPower = SumAttackPower(secondPlayerUnits);
            Console.WriteLine("Player2AttackPower " + player2AttackPower);

            // Get DefensePower
            var player1DefensePower = SumDefensePower(firstPlayerUnits);
            Console.WriteLine("Player1DefensePower: " + player1DefensePower);
            
            var player2DefensePower = SumDefensePower(secondPlayerUnits);
            Console.WriteLine("Player2DefensePower: " + player2DefensePower);

            // Get SpecialPower
            var player1SpecialPower = SumSpecialPower(firstPlayerUnits);
            Console.WriteLine("Player1 SpecialPower: " + player1SpecialPower);

            var player2SpecialPower = SumSpecialPower(secondPlayerUnits);
            Console.WriteLine("Player2 SpecialPower: " + player2SpecialPower);

            var PlayerWithAdvantage = CalculateAdvantage(player1SpecialPower, player2SpecialPower);



            // Calculate Attacks, Defense
            var player1AttackRoll = CalculateAttackRoll(player1AttackPower);
            Console.WriteLine("Player1AttackRoll: " + player1AttackRoll);

            var player2AttackRoll = CalculateAttackRoll(player2AttackPower);
            Console.WriteLine("Player2AttackRoll: " + player2AttackRoll);


            var player1DefenseRoll = CalculateDefenseOrArmorRoll(player1DefensePower);
            Console.WriteLine("Player1DefenseRoll: " + player1DefenseRoll);

            var player2DefenseRoll = CalculateDefenseOrArmorRoll(player2DefensePower);
            Console.WriteLine("Player2DefenseRoll: " + player2DefenseRoll);

            // 11. Calculate Hit
            var player1Hit = player1AttackRoll - player2DefenseRoll;

            Console.WriteLine("Player1Hit: " + player1Hit);

            var player2Hit = player2AttackRoll - player1DefenseRoll;
            Console.WriteLine("Player2Hit: " + player2Hit);

            // 12. Determine target unit(s) todo: Egyelőre mindig az első egységet választja a listában
            var player1Target = firstPlayerUnits[0];
            var player2Target = secondPlayerUnits[0];

            // 15. Apply Special effects on target(s) Instant/Delayed: most vagy következő körben 1 damage

            // calculate for firstPLayer
            var specialType = DetermineSpecialTypeForAPlayer(PlayerWithAdvantage, firstPlayerUnits[0].Player);
            CalculateSpecialDamageForAPlayer(player2Target, specialType);

            // 2nd player special move
            var specialTypeForSecondPlayer = DetermineSpecialTypeForAPlayer(PlayerWithAdvantage, secondPlayerUnits[0].Player);
            CalculateSpecialDamageForAPlayer(player1Target, specialTypeForSecondPlayer);

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

        private int SumSpecialPower(List<IUnit> units)
        {
            var specialPower = 0;

            foreach (var unit in units)
            {
                if (unit.CurrentMove == Moves.Special)
                {
                    specialPower += unit.SpecialValue;
                }
            }
            return specialPower;
        }

        private Player CalculateAdvantage(int firstPLayerSpecial, int secondPLayerSpecial)
        {
            if (firstPLayerSpecial > secondPLayerSpecial)
            {
                return Player.FirstPlayer;
            }
            else if (secondPLayerSpecial > firstPLayerSpecial)
            {
                return Player.SecondPlayer;
            }
            else return Player.NoOne;
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

        public void CalculateSpecialDamageForAPlayer(IUnit unit, SpecialEffectType attackerSpecialType)
        {
            var damage = 0;
            // Apply DelayedSpecial
            if (unit.HasDelayedSpecial)
            {
                damage++;
                unit.HasDelayedSpecial = false;
            }
            // Apply Current Instant Special
            if (attackerSpecialType == SpecialEffectType.Instant)
            {
                damage++;
            }
            // Add Current Delayed Special to uit
            if (attackerSpecialType == SpecialEffectType.Delayed)
            {
                unit.HasDelayedSpecial = true;
            }

            unit.Hp -= damage;
            Console.WriteLine("Player " + unit.UserId + "'s remaining HP is: " + unit.Hp);
        }

        public SpecialEffectType DetermineSpecialTypeForAPlayer(Player playerWithAdvantage, Player currentPlayer)
        {
            if (playerWithAdvantage == currentPlayer) // HA A (nyert) akkor Instant támadás, ha B akkor Delayed
            {
                return SpecialEffectType.Instant;
            }
            else return SpecialEffectType.Delayed;
        }
    }
}
public enum Player
{
    NoOne = 0,
    FirstPlayer = 1,
    SecondPlayer = 2,
    ThirdPlayer  = 3
}

public enum SpecialEffectType
{
    Instant = 0,
    Delayed = 1,
}