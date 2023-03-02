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

    private void OnMouseDown()
    {
        if (Playable)
        {
            if (GameManager.Instance.State != GameManager.GameState.Move || UnitManager.Instance.SelectedArmy == null) return;
            var selectedArmy = UnitManager.Instance.SelectedArmy;
            SetUnit(selectedArmy);
            foreach (var soldier in selectedArmy.soldiers)
            {
                soldier.gameObject.SetActive(false);
            }
            UnitManager.Instance.SetSelectedArmy(null);
        }

    }

    public void SetUnit(BaseArmy unit)
    {
        if (unit.OccupiedTile != null) 
        {
            unit.OccupiedTile.OccupiedArmies.Remove(unit);
            int count = 0;
            foreach (var remainingUnit in unit.OccupiedTile.OccupiedArmies)
            {
                
                Debug.Log($"{remainingUnit.UnitName}: I remained here!");
                AdjustRemainingUnitPosition(remainingUnit, count);
                count++;
            }
        }

        OccupiedArmies.Add(unit);
        SetUnitPosition(unit);

        unit.OccupiedTile = this;
    }

    public string GetOccupiedUnitsNames()
    {
        return string.Join("\n", OccupiedArmies.Select(x => x.UnitName));
    }

    private void AdjustRemainingUnitPosition(BaseArmy unit, int count)
    {
        unit.transform.position = unit.OccupiedTile.transform.position + new Vector3(0, 0, -0.1f); ;
        if (count == 1)
        {
            unit.transform.position = unit.transform.position + new Vector3(0.25f, 0, 0);
        }
        else if (count == 2)
        {
            unit.transform.position = unit.transform.position + new Vector3(-0.25f, 0, 0);
        }
    }

    private void SetUnitPosition(BaseArmy unit)
    {
        unit.transform.position = transform.position + new Vector3(0, 0, -0.1f);
        if (OccupiedArmies.Count == 2)
        {
            unit.transform.position = unit.transform.position + new Vector3(0.25f, 0, 0);
        }
        else if (OccupiedArmies.Count == 3)
        {
            unit.transform.position = unit.transform.position + new Vector3(-0.25f, 0, 0);
        }
    }
}
