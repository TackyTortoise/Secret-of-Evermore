using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_Item
{
    public enum ItemType
    {
        Sword,
        None,
    }

    public ItemType Type;

    public int Amount;
}
