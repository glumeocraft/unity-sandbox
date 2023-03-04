using BattleCalculator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSoldier : MonoBehaviour, IUnit
{
    public int Speed, Armor, Attack, Special, Health, Defense;
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

    void Awake()
    {
        ArmorValue = Armor;
        AttackValue = Attack;
        DefenseValue = Defense;
        Hp = Health;
    }

    public void SetSoldierForMovement()
    {
        if (RemainingActions < 1) return;
        UnitManager.Instance.SelectedSoldierForMovement = this;
    }

}
