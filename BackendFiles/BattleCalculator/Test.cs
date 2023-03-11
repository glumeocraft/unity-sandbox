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
        int battleRestarted = 0; // One or both of units died so their HP returned to 30

        //[Theory(DisplayName = "Should run battlecalculator 1 v 1")]
        //[InlineData(10, Moves.Defense, Moves.Defense)]
        //[InlineData(1, Moves.Attack, Moves.Attack)]
        public void RunBattleCalculator(int turns, Moves player1Move, Moves player2Move)
        {
            // Arrange
            // Unit választás
            var player1Unit1 = new convictor(1);
            player1Unit1.CurrentMove = player1Move;
            var player2Unit1 = new avaregeJoe(2);
            player2Unit1.CurrentMove = player2Move;
            // Armies
            var player1Army = new List<IUnit> { player1Unit1 };
            var player2Army = new List<IUnit> { player2Unit1 };
            // Terrain

            // Act
            var battle = new Battle();
            int deadCount = 0;
            var finalTurnNumbers = new List<int>(); // Hány körig tartott a csata
            var player1DeadCount = 0;
            var player2DeadCount = 0;
            var nothingHappenedTurn = 0;
            var instaKill = 0;
            var notDamaged = 0; // Azon egységek száma, akik nem sérültek meg egy harc körben (összeadódik a 2 játékos sértetlen egységeinek száma körönként)
            var player1Unit1Hp = player1Army[0].Hp;
            var player2Unit1Hp = player2Army[0].Hp;
            for (int turn = 0; turn < turns; turn++)
            {
                var player1NotDamaged = false;
                var player2NotDamaged = false;
                var resultArmies = battle.SimulateBattle(player1Army, player2Army);
                var hpDifferenceForPlayer1 = 0; // Mindig a PLayer1Hp - Player2Hp tehát ha pozitív akkor a csata végén player1nek a maradék élete, ha negatív, akkor azt mutatja hogy mennyi élettel élte túl player2
                foreach (var army in resultArmies)
                {


                    foreach (var unit in army)
                    {

                        if (unit.UserId == 1)  // Ha Nem változott Player1 HP-ja
                        {
                            if (player1Unit1Hp == unit.Hp)
                            {
                                notDamaged++;
                                player1NotDamaged = true;
                            }
                            player1Unit1Hp = unit.Hp;
                        }
                        if (unit.UserId == 2) // Ha Nem változott Player2 HP-ja
                        {
                            if (player2Unit1Hp == unit.Hp)
                            {
                                notDamaged++;
                                player2NotDamaged = true;
                            }
                            player2Unit1Hp = unit.Hp;
                        }
                        if (player1NotDamaged || player2NotDamaged) // alapból false
                        {
                            nothingHappenedTurn++;
                        }

                        if (unit.Hp <= 0) // meghalt az egység , 1 v 1-ben ez a csata vége
                        {
                            deadCount++;
                            Console.WriteLine("Player#" + unit.UserId + " unit died");
                            if (unit.UserId == 1)
                            {
                                player1DeadCount++;
                                hpDifferenceForPlayer1 += unit.Hp - player2Army[0].Hp;

                                player1Unit1Hp = 30;
                                player1Unit1.Hp = 30;
                                player2Unit1Hp = 30;
                                player2Unit1.Hp = 30;
                                RestartBattle(unit, player2Army[0]);
                            }
                            else
                            {
                                player2DeadCount++;
                                player1Unit1Hp = 30;
                                player1Unit1.Hp = 30;
                                player2Unit1Hp = 30;
                                player2Unit1.Hp = 30;
                                hpDifferenceForPlayer1 = player1Army[0].Hp - unit.Hp;
                                RestartBattle(player1Army[0], unit);
                            }
                            if (turn == 1) // InstaKill
                            {
                                instaKill++;
                            }
                            finalTurnNumbers.Add(turn);
                        }


                    }

                }

                Console.WriteLine("# of battles " + battleRestarted + 1 + ".");
                Console.WriteLine("HP difference for Player1: " + hpDifferenceForPlayer1 + ".");
                Console.WriteLine("Final turn count: " + finalTurnNumbers + ".");
                Console.WriteLine("InstaKill count: " + instaKill + ".");
                Console.WriteLine("Nothing Happened turns count: " + nothingHappenedTurn + ".");
                Console.WriteLine("Turns in Player1 not damaged: " + player1NotDamaged + ".");
                Console.WriteLine("Turns in Player2 not damaged: " + player2NotDamaged + ".");
                Console.WriteLine("Player1 death count: " + player1DeadCount + ".");
                Console.WriteLine("Player2 death count: " + player2DeadCount + ".");
                Console.WriteLine("All death count: " + deadCount + ".");
                Console.ReadLine();
                // TEszt:
                // lefuttatjuk a harcok x szer, beadva hogy mit csinál végig, és végeredményben átlagban mennyi lett a:
                // nulla sebzés, nulla sebzés mind2nél, 1 körös kill (fatality), hány körig tartot (lehet átlagolni a végén), átlag hp különbség (végén átlagolni)
                // Táblázatba kiírni az eredményeket. excellel.
                //
                //teszt eset amokikor előre megadjuk, hogy mik a lépések
                // sok játékosnál összekéne adni az attackRollokat *0.65% és defenseRoll is!
                // Assert

            }
            Assert.True(battleRestarted > 0);

        }

        public void RestartBattle(IUnit player1Unit, IUnit player2Unit)
        {
            battleRestarted++;
            player1Unit.Hp = 30;
            player2Unit.Hp = 30;
        }
    }
}