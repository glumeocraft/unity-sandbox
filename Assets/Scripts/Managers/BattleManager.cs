using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCalculator;

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
            var player1Units = new List<IUnit>();
            var player2Units = new List<IUnit>();
            Debug.Log($"battle started on: {tile.TileName}");
            foreach (var army in tile.OccupiedArmies)
            {
                Debug.Log($"one army in {tile.OccupiedArmies.Count}");
                var playerArmy = army.soldiers;
                foreach (var soldier in playerArmy)
                {
                    soldier.UserId = (int)army.Faction;
                    if (army.Faction == Faction.Team1)
                    {
                        player1Units.Add(soldier);
                    }
                    else if (army.Faction == Faction.Team2)
                    {
                        player2Units.Add(soldier);
                    }
                }

            }
            var battleCalc = new Battle();
            Debug.Log("starting battle");
            battleCalc.SimulateBattle(player1Units, player2Units);
            
        }

        
        GameManager.Instance.UpdateGameState(GameManager.GameState.Move);
    }
}
