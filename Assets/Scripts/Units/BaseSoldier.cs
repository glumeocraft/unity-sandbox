using BattleCalculator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSoldier : MonoBehaviour, IUnit
{
    public int Speed, Armor, Attack, Special, MaxHealth, Defense;
    public string Name;
    public int RemainingActions;
   
    
   
    public int ArmorValue { get; set; }
    public int AttackValue { get; set; }
    public Program.Moves CurrentMove { get; set; }
    public int DefenseValue { get; set; }
    public int Direction { get; set; }
    public string Egyeb { get; set; }
    public bool NewEntry { get; set; }
    public int SpecialValue { get; set; }
    public int UserId { get; set; }
    public int Hp { get; set; }
    public int MaxHP { get; set; }
    public Player Player { get; set; }
    public bool HasDelayedSpecial { get; set; }
    public bool IsDead { get; set; }



    void Awake()
    {
        ArmorValue = Armor;
        AttackValue = Attack;
        DefenseValue = Defense;
        MaxHP = MaxHealth;
        Debug.Log("hp set to max hp");
        Hp = MaxHealth;
        IsDead = false;

    }

    public void SetSoldierForMovement()
    {
        if (RemainingActions < 1) return;
        UnitManager.Instance.SelectedSoldierForMovement = this;
    }

    public void ShowSoldierInfo()
    {
        var soldierInfoString = $"Attack: {Attack} , Defense: {Defense} , Special: {SpecialValue} , Speed: {Speed} , Health: {Hp}";
        MenuManager.Instance.ShowSoldierInfoMenu(soldierInfoString);
    }

    public void HideSoldierInfo()
    {
        MenuManager.Instance.HideSoldierInfoMenu();
    }

}
