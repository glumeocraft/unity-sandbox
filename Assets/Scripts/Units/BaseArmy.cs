using System.Collections.Generic;
using UnityEngine;

public class BaseArmy : MonoBehaviour
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

    public void AddSoldier(BaseSoldier soldier)
    {
        //add soldier to the menu
        var spawnedSoldier = Instantiate(soldier, MenuManager.Instance.GetSoldiersInfoObject().transform);
        //set soldier position in the menu based on other soldiers
        //spawnedSoldier.transform.localPosition = new Vector3(0, spawnedSoldier.transform.localPosition.y - 2, 0);
        spawnedSoldier.transform.localPosition = new Vector3(0, spawnedSoldier.transform.localPosition.y - soldiers.Count * 52, -0.1f);
        soldiers.Add(spawnedSoldier);
    }
}
