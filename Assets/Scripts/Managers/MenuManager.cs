using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject _selectedUnitObject, _tileInfoObject, _tileArmyInfoObject, _gameStateInfoObject, _restartButton, _exitButton, _endMoveButton, _soldiersInfoObject, _soldierInfoObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        _gameStateInfoObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Game state: {GameManager.Instance.State}";
        /**foreach (var army in UnitManager.Instance.AllArmies)
        {
            foreach (var soldier in army.soldiers)
            {
                if (soldier.gameObject.activeSelf)
                {
                    //soldier.GetComponentInChildren<TextMeshProUGUI>().text = $"Attack: {soldier.Attack} , Defense: {soldier.Defense} , Special: {soldier.SpecialAtk} , Speed: {soldier.Speed} , Health: {soldier.Health} , Moved: {soldier.Moved}";
                }
            }
        }*/
    }

    public void ShowSelectedUnits(BaseArmy army)
    {
        if (army == null)
        {
            _selectedUnitObject.SetActive(false);
            return;
        }
        _selectedUnitObject.GetComponentInChildren<TextMeshProUGUI>().text = $"{army.UnitName}";
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
            _tileArmyInfoObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Armies: \n{tile.OccupiedArmies.Count}";
            _tileArmyInfoObject.SetActive(true);
        }
        else { _tileArmyInfoObject.SetActive(false); }
    }

    public void EnableEndMoveButton()
    {
        _endMoveButton.SetActive(true);
    }

    public void DisableEndMoveButton()
    {
        _endMoveButton.SetActive(false);
    }

    private void OnEndMoveButtonClick()
    {

    }

    public GameObject GetSoldiersInfoObject()
    {
        return _soldiersInfoObject;
    }

    public void ShowSoldierInfoMenu(string soldierInfoText)
    {
        _soldierInfoObject.GetComponentInChildren<TextMeshProUGUI>().text = soldierInfoText;
        _soldierInfoObject.SetActive(true);
    }

    public void HideSoldierInfoMenu()
    {
        _soldierInfoObject.SetActive(false);
    }
}
