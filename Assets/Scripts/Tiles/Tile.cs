using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    public List<BaseUnit> OccupiedArmies;
    public string TileName;

    private void Awake()
    {
        OccupiedArmies = new List<BaseUnit>();
    }
    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
        MenuManager.Instance.ShowHoveredTileInfo(this);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
        MenuManager.Instance.ShowHoveredTileInfo(null);
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedArmies.Clear();
        OccupiedArmies.Add(unit);
        unit.OccupiedTile = this;
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.State != GameManager.GameState.Move || UnitManager.Instance.SelectedArmy == null) return;
        SetUnit(UnitManager.Instance.SelectedArmy);
        UnitManager.Instance.SetSelectedArmy(null);
    }

    public string GetOccupiedUnitsNames()
    {
        return string.Join("\n", OccupiedArmies.Select(x => x.UnitName));
    }
}
