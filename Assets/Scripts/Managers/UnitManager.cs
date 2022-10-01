using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableArmy> _units;
    private List<ScriptableSoldier> _soldiers;

    public BaseUnit SelectedArmy;
    public List<BaseUnit> AllArmies;

    private void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableArmy>("Units").ToList();
        _soldiers = Resources.LoadAll<ScriptableSoldier>("Units").ToList();
    }

    public void SpawnBlueArmy()
    {
        //spawn army
        var team1ArmyPrefab = GetFactionBaseUnit<BaseUnit>(Faction.Team1);
        var spawnedTeam1Army = Instantiate(team1ArmyPrefab);
        var randomSpawnTile = GridManager.Instance.GetTeam1SpawnTile();
        randomSpawnTile.SetUnit(spawnedTeam1Army);
        AllArmies.Add(spawnedTeam1Army);

        //spawn soldier info
        int soldierCount = 10;
        for (int i = 0; i < soldierCount; i++)
        {
            var team1SmallSoldierPrefab = GetBaseSoldierByName<BaseSoldier>("blue");
            var spawnedSoldier = Instantiate(team1SmallSoldierPrefab, MenuManager.Instance.GetSoldiersInfoObject().transform);
            spawnedSoldier.transform.localPosition = new Vector3(0, spawnedSoldier.transform.localPosition.y - 2, 0);
            spawnedSoldier.transform.localPosition = new Vector3(0, spawnedSoldier.transform.localPosition.y - i * 52, 0);
            spawnedTeam1Army.soldiers.Add(spawnedSoldier);
        }
    }

    public void SpawnRedArmy()
    {
        //spawn army
        var team2ArmyPrefab = GetFactionBaseUnit<BaseUnit>(Faction.Team2);
        var spawnedTeam2Army = Instantiate(team2ArmyPrefab);
        var randomSpawnTile = GridManager.Instance.GetTeam2SpawnTile();
        randomSpawnTile.SetUnit(spawnedTeam2Army);
        AllArmies.Add(spawnedTeam2Army);

        //spawn soldier info
        var team1SmallSoldierPrefab = GetBaseSoldierByName<BaseSoldier>("red");
        var spawnedSoldier = Instantiate(team1SmallSoldierPrefab, MenuManager.Instance.GetSoldiersInfoObject().transform);
        spawnedTeam2Army.soldiers.Add(spawnedSoldier);
    }

    public void SpawnPinkArmy()
    {
        //spawn army
        var team3ArmyPrefab = GetFactionBaseUnit<BaseUnit>(Faction.Team3);
        var spawnedTeam3Army = Instantiate(team3ArmyPrefab);
        var randomSpawnTile = GridManager.Instance.GetTeam3SpawnTile();
        randomSpawnTile.SetUnit(spawnedTeam3Army);
        AllArmies.Add(spawnedTeam3Army);

        //spawn soldier info
        var team1SmallSoldierPrefab = GetBaseSoldierByName<BaseSoldier>("pink");
        var spawnedSoldier = Instantiate(team1SmallSoldierPrefab, MenuManager.Instance.GetSoldiersInfoObject().transform);
        spawnedTeam3Army.soldiers.Add(spawnedSoldier);
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
        foreach (var army in AllArmies)
        {
            foreach (var soldier in army.soldiers)
            {
                soldier.gameObject.SetActive(false);
            }
        }
        if (unit != null)
        {
            foreach (var soldier in unit.soldiers)
            {
                soldier.GetComponentInChildren<TextMeshProUGUI>().text = $"Attack: {soldier.Attack} , Defense: {soldier.Defense} , Special: {soldier.SpecialAtk} , Speed: {soldier.Speed} , Health: {soldier.Health} , Moved: {soldier.Moved}";
                soldier.gameObject.SetActive(true);
            }
        } 
    }



    private T GetBaseSoldierByName<T>(string name) where T : BaseSoldier
    {
        return (T)_soldiers.Where(u => u.SoldierPrefab.Name.ToUpper().Contains(name.ToUpper())).First().SoldierPrefab;
    }
}
