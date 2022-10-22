using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Army", menuName = "Scriptable Army")]
public class ScriptableArmy : ScriptableObject
{
    public Faction Faction;
    public BaseUnit UnitPrefab;
}

public enum Faction
{
    Team1 = 1,
    Team2 = 2,
    Team3 = 3
}




