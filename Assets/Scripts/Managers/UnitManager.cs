using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;

    public BaseUnit SelectedArmy;

    private void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnTeam1()
    {

        Debug.Log("Spawn Team 1 called");
        var unitCount = 1;

        for (int i = 0; i < unitCount; i++)
        {
            var team1ArmyPrefab = GetFactionBaseUnit<BaseTeam1>(Faction.Team1);
            Debug.Log("Team 1 Army Prefab created");
            var spawnedTeam1Army = Instantiate(team1ArmyPrefab);
            var randomSpawnTile = GridManager.Instance.GetTeam1SpawnTile();
            randomSpawnTile.SetUnit(spawnedTeam1Army);
        }
    }

    public void SpawnTeam2()
    {
        var unitCount = 1;

        for (int i = 0; i < unitCount; i++)
        {
            var team2ArmyPrefab = GetFactionBaseUnit<BaseTeam2>(Faction.Team2);
            var spawnedTeam2Army = Instantiate(team2ArmyPrefab);
            var randomSpawnTile = GridManager.Instance.GetTeam2SpawnTile();
            randomSpawnTile.SetUnit(spawnedTeam2Army);
        }
    }

    public void SpawnTeam3()
    {
        var unitCount = 1;

        for (int i = 0; i < unitCount; i++)
        {
            var team3ArmyPrefab = GetFactionBaseUnit<BaseTeam3>(Faction.Team3);
            var spawnedTeam3Army = Instantiate(team3ArmyPrefab);
            var randomSpawnTile = GridManager.Instance.GetTeam3SpawnTile();
            randomSpawnTile.SetUnit(spawnedTeam3Army);
        }
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    private T GetFactionBaseUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).First().UnitPrefab;
    }

    public void SetSelectedArmy(BaseUnit unit)
    {
        SelectedArmy = unit;
        MenuManager.Instance.ShowSelectedUnit(unit);
    }
}
