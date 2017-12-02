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
        Consumable,
        None,
    }

    public ItemType Type;
    public String Name { get; protected set; }
    public int Amount = 1;

    public int AttackBoost { get; protected set; }
    public int DefenseBoost { get; protected set; }

    public bool Equiped = false;

    public Script_VisualItemUI UIItem = null;
} 

