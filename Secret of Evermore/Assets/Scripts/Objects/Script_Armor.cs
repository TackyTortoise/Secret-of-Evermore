using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Armor : Script_Item
{
    public enum ArmorType
    {
        Chest,
        Legs,
        Helm
    }

    public ArmorType Type { get; private set; }

    public Script_Armor(String name, ArmorType type, int defenseBonus)
    {
        Name = name;
        Type = type;
        DefenseBoost = defenseBonus;
    }
}
