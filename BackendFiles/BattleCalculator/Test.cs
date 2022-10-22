using System;
using System.Collections.Generic;
using System.Text;
using static BattleCalculator.IUnit;
using Xunit;
using static BattleCalculator.Program;

namespace BattleCalculator
{
    public class Test
    {

        [Theory(DisplayName = "Should run battlecalculator")]
        [InlineData(10)]
        [InlineData(1)]
        public void RunBattleCalculator(int turns)
        {
            // Arrange
            // Unit választás
            var player1Unit1 = new convictor(1);
            var player2Unit1 = new avaregeJoe(2);
            // Armies
            var player1Army = new List<Unit> { player1Unit1 };
            var player2Army = new List<Unit> { player2Unit1 };
            // Terrain

            // Act
            var battle = new Battle();
            int deadCount;
            for (int i = 0; i < turns; i++)
            {
                var resultArmies = battle.SimulateBattle(player1Army, player2Army);
                foreach (var army in resultArmies)
                {
                    foreach (var unit in army)
                    {
                        if (unit.Hp <= 0)
                        {

                        }
                    }
                    

                }
                
            }


            // Assert

        }
    }

}