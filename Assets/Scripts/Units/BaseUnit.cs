using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCalculator;

public class BaseUnit : MonoBehaviour
{
    public Tile OccupiedTile;
    public Faction Faction;
    public string UnitName;
    public List<BaseSoldier> soldiers;

    private void OnMouseDown()
    {
        if (GameManager.Instance.State != GameManager.GameState.Move) return;
        UnitManager.Instance.SetSelectedArmy(this);
    }

    private void OnMouseEnter()
    {
        OccupiedTile.OnMouseEnter();
    }

    private void OnMouseExit()
    {
        OccupiedTile.OnMouseExit();
    }
}
