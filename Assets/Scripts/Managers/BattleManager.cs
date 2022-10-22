using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    
    private void Awake()
    {
        Instance = this;
        //GetComponent<BattleCalculator.Battle>().SimulateBattle();
    }

    public void StartFights()
    {
        foreach (var tile in GridManager.Instance.GetBattleTiles())
        {
            List<BattleCalculator.Program.Unit> player1Units = new List<BattleCalculator.Program.Unit>();
            List<BattleCalculator.Program.Unit> player2Units = new List<BattleCalculator.Program.Unit>();
            Debug.Log($"battle started on: {tile.TileName}");
            foreach (var army in tile.OccupiedArmies)
            {
                var playerArmy = army.soldiers;
                List<BattleCalculator.Program.Unit> playerUnits = new List<BattleCalculator.Program.Unit>();
                foreach (var soldier in playerArmy)
                {
                    BattleCalculator.Program.Unit unit = new BattleCalculator.Program.Unit(((int)army.Faction));
                    unit.Attack = soldier.Attack;
                    unit.Defense = soldier.Defense;
                    unit.Armor = soldier.Armor;
                    unit.Hp = soldier.Health;
                    playerUnits.Add(unit);
                }
                
            }
            GameManager.Instance.UpdateGameState(GameManager.GameState.Move);
        }
        
    }
}
