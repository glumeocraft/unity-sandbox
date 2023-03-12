using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using BattleCalculator;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableArmy> _units;
    private List<ScriptableSoldier> _soldiers;

    public BaseSoldier SelectedSoldierForMovement;
    public BaseArmy SelectedArmy;
    public List<BaseArmy> AllArmies;

    private void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableArmy>("Units").ToList();
        _soldiers = Resources.LoadAll<ScriptableSoldier>("Units").ToList();
    }

    public void SpawnBlueArmy()
    {
        //spawn army
        var team1ArmyPrefab = GetFactionBaseArmy<BaseArmy>(Faction.Team1);
        var spawnedTeam1Army = Instantiate(team1ArmyPrefab);
        var randomSpawnTile = GridManager.Instance.GetTeam1SpawnTile();
        randomSpawnTile.SetUnit(spawnedTeam1Army);
        AllArmies.Add(spawnedTeam1Army);


        //spawn soldier info
        var team1SmallSoldierPrefab = GetBaseSoldierByName<BaseSoldier>("blue");
        team1SmallSoldierPrefab.Player = Player.FirstPlayer;
        spawnedTeam1Army.AddSoldier(team1SmallSoldierPrefab);
        spawnedTeam1Army.AddSoldier(team1SmallSoldierPrefab);
        spawnedTeam1Army.AddSoldier(team1SmallSoldierPrefab);
        spawnedTeam1Army.AddSoldier(team1SmallSoldierPrefab);
        spawnedTeam1Army.AddSoldier(team1SmallSoldierPrefab);
        spawnedTeam1Army.AddSoldier(team1SmallSoldierPrefab);

    }

    public void SpawnRedArmy()
    {
        //spawn army
        var team2ArmyPrefab = GetFactionBaseArmy<BaseArmy>(Faction.Team2);
        var spawnedTeam2Army = Instantiate(team2ArmyPrefab);
        var randomSpawnTile = GridManager.Instance.GetTeam2SpawnTile();
        randomSpawnTile.SetUnit(spawnedTeam2Army);
        AllArmies.Add(spawnedTeam2Army);

        //spawn soldier info
        var team2SmallSoldierPrefab = GetBaseSoldierByName<BaseSoldier>("red");
        team2SmallSoldierPrefab.Player = Player.SecondPlayer;
        spawnedTeam2Army.AddSoldier(team2SmallSoldierPrefab);
        spawnedTeam2Army.AddSoldier(team2SmallSoldierPrefab);
        spawnedTeam2Army.AddSoldier(team2SmallSoldierPrefab);
    }

    public void SpawnPinkArmy()
    {
        //spawn army
        var team3ArmyPrefab = GetFactionBaseArmy<BaseArmy>(Faction.Team3);
        var spawnedTeam3Army = Instantiate(team3ArmyPrefab);
        var randomSpawnTile = GridManager.Instance.GetTeam3SpawnTile();
        randomSpawnTile.SetUnit(spawnedTeam3Army);
        AllArmies.Add(spawnedTeam3Army);

        //spawn soldier info
        var team1SmallSoldierPrefab = GetBaseSoldierByName<BaseSoldier>("pink");
        spawnedTeam3Army.AddSoldier(team1SmallSoldierPrefab);
    }

    /**
    private T GetRandomUnit<T>(Faction faction) where T : BaseArmy
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }
    **/
    public T GetFactionBaseArmy<T>(Faction faction) where T : BaseArmy
    {
        return (T)_units.Where(u => u.Faction == faction).First().UnitPrefab;
    }


    public void SetSelectedArmy(BaseArmy unit)
    {
        SelectedArmy = unit;
        MenuManager.Instance.ShowSelectedUnits(unit);
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
                //soldier.GetComponentInChildren<TextMeshProUGUI>().text = $"Attack: {soldier.AttackValue} , Defense: {soldier.DefenseValue} , Special: {soldier.SpecialValue} , Speed: {soldier.Speed} , Health: {soldier.Hp} , Moved: {soldier.RemainingActions}";
                soldier.gameObject.SetActive(true);
            }
        } 
    }



    private T GetBaseSoldierByName<T>(string name) where T : BaseSoldier
    {
        return (T)_soldiers.Where(u => u.SoldierPrefab.Name.ToUpper().Contains(name.ToUpper())).First().SoldierPrefab;
    }

    public void RemoveDeadSoldiers()
    {
        foreach (var army in AllArmies)
        {
            foreach(var soldier in army.soldiers)
            {
                Debug.Log($"soldier hp is {soldier.Hp}");
                if (soldier.Hp <= 0)
                {
                 
                    soldier.IsDead = true;
                    Debug.Log($"marked soldier for dead");
                }
            }
            army.RemoveDeadSoldiers();
            
        }

        

    }

    public void RemoveEmptyArmies()
    {
        foreach (var army in AllArmies)
        {
            Debug.Log($"soldier count is {army.soldiers.Count}");
            if (army.soldiers.Count <= 0)
            {
                Debug.Log($"destroy army");
                army.ToBeRemoved = true;
                //Destroy(army);
            }
        }
        CleanAllArmies();
    }

    private void CleanAllArmies()
    {
        AllArmies.RemoveAll(army => army.ToBeRemoved);
    }
}
