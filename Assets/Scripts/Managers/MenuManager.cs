using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject _selectedUnitObject, _tileInfoObject, _tileArmyInfoObject;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowSelectedUnit(BaseUnit unit)
    {
        if (unit == null)
        {
            _selectedUnitObject.SetActive(false);
            return;
        }
        _selectedUnitObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Selected unit: {unit.UnitName}";
        _selectedUnitObject.SetActive(true);
    }

    public void ShowHoveredTileInfo(Tile tile)
    {
        if (tile == null)
        {
            _tileInfoObject.SetActive(false);
            _tileArmyInfoObject.SetActive(false);
            return;
        }

        _tileInfoObject.GetComponentInChildren<TextMeshProUGUI>().text = tile.TileName;
        _tileInfoObject.SetActive(true);

        if (tile.OccupiedArmies.Count > 0)
        {
            _tileArmyInfoObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Armies: \n{tile.GetOccupiedUnitsNames()}";
            _tileArmyInfoObject.SetActive(true);
        }
        else { _tileArmyInfoObject.SetActive(false); }
    }
}
