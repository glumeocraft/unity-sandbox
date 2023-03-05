using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    public List<BaseArmy> OccupiedArmies;
    public string TileName;
    public bool Playable;

    private void Awake()
    {
        OccupiedArmies = new List<BaseArmy>();
    }
    public void OnMouseEnter()
    {
        if (Playable)
        {
            _highlight.SetActive(true);
            MenuManager.Instance.ShowHoveredTileInfo(this);
        }
    }

    public void OnMouseExit()
    {
        if (Playable)
        {
            _highlight.SetActive(false);
            MenuManager.Instance.ShowHoveredTileInfo(null);
        }

    }

    //TODO: refactor!
    private void OnMouseDown()
    {
        if (Playable)
        {
            if (GameManager.Instance.State != GameManager.GameState.Move || !UnitManager.Instance.SelectedSoldierForMovement) return;
            var selectedUnit = UnitManager.Instance.SelectedSoldierForMovement;
            var selectedArmy = UnitManager.Instance.SelectedArmy;
            
            if (!OccupiedArmies.Find(unit => unit.Faction == selectedArmy.Faction))
            {
                Debug.Log("no own faction army there!");

                //spawn army
                var armyPrefab = UnitManager.Instance.GetFactionBaseArmy<BaseArmy>(selectedArmy.Faction);
                var spawnedArmy = Instantiate(armyPrefab);
                SetUnit(spawnedArmy);
                UnitManager.Instance.AllArmies.Add(spawnedArmy);
                spawnedArmy.AddSoldier(selectedUnit);
            }
            else 
            {
                var armyToMoveSoldierTo = OccupiedArmies.Find(a => a.Faction == selectedArmy.Faction);
                armyToMoveSoldierTo.AddSoldier(selectedUnit);
            }




            foreach (var soldier in selectedArmy.soldiers)
            {
                soldier.gameObject.SetActive(false);
            }

            selectedArmy.RemoveSoldier(selectedUnit);
            Destroy(selectedUnit.gameObject);
            if (selectedArmy.soldiers.Count < 1)
            {
                selectedArmy.OccupiedTile.RemoveOccupyingArmy(selectedArmy);
                Destroy(selectedArmy.gameObject);
            }
            UnitManager.Instance.SetSelectedArmy(null);
            UnitManager.Instance.SelectedSoldierForMovement = null;


            /**if (OccupiedArmies.Select())
            SetUnit(selectedArmy);
            foreach (var soldier in selectedArmy.soldiers)
            {
                soldier.gameObject.SetActive(false);
            }
            UnitManager.Instance.SetSelectedArmy(null);
            */
        }

    }

    public void SetUnit(BaseArmy unit)
    {
        if (unit.OccupiedTile != null) 
        {
            unit.OccupiedTile.OccupiedArmies.Remove(unit);
            /**
            int count = 0;
            foreach (var remainingUnit in unit.OccupiedTile.OccupiedArmies)
            {
                
                Debug.Log($"{remainingUnit.UnitName}: I remained here!");
                AdjustRemainingArmyPosOnTile(remainingUnit, count);
                count++;
            }
            */
        }

        OccupiedArmies.Add(unit);
        SetArmiesPosOnTile(unit);

        unit.OccupiedTile = this;
    }

    public string GetOccupiedUnitsNames()
    {
        return string.Join("\n", OccupiedArmies.Select(x => x.UnitName));
    }

    /// <summary>
    /// Sets unit position to be always clickable and if there are multiple armies there make them not overlap.
    /// </summary>
    private void AdjustRemainingArmyPosOnTile(BaseArmy army, int count)
    {
        army.transform.position = army.OccupiedTile.transform.position + new Vector3(0, 0, -0.1f);
        if (count == 1)
        {
            army.transform.position = army.transform.position + new Vector3(0.25f, 0, 0);
        }
        else if (count == 2)
        {
            army.transform.position = army.transform.position + new Vector3(-0.25f, 0, 0);
        }
    }

    /// <summary>
    /// Sets unit position to be always clickable and if there are multiple armies there make them not overlap.
    /// </summary>
    private void SetArmiesPosOnTile(BaseArmy army)
    {
        army.transform.position = transform.position + new Vector3(0, 0, -0.1f);
        if (OccupiedArmies.Count == 1) return;


        var horizontalPosAdjustment = -0.25f;
        foreach(var occupiedArmy in OccupiedArmies)
        {
            occupiedArmy.transform.position = transform.position;
            occupiedArmy.transform.position = occupiedArmy.transform.position + new Vector3(horizontalPosAdjustment, 0, -0.1f);
            horizontalPosAdjustment += 0.25f;
        }

        /**if (OccupiedArmies.Count == 2)
        {
            unit.transform.position = unit.transform.position + new Vector3(0.25f, 0, 0);
        }
        else if (OccupiedArmies.Count == 3)
        {
            unit.transform.position = unit.transform.position + new Vector3(-0.25f, 0, 0);
        }*/
    }

    public void RemoveOccupyingArmy(BaseArmy army)
    {
        OccupiedArmies.Remove(army);
    }
}
