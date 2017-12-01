using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_Item
{
    public enum ItemType
    {
        Weapon,
        Armor,
        None,
    }

    public ItemType Type;
    public String Name { get; protected set; }
    public int Amount;

    public int AttackBoost { get; protected set; }
    public int DefenseBoost { get; protected set; }

    public bool Equiped = false;
}
