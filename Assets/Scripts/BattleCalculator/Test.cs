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
        [InlineData(1161)]
        [InlineData(1)]
        public void RunBattleCalculator(int value)
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
            var resultArmies = battle.SimulateBattle(player1Army, player2Army);


            // Assert

        }
    }

}