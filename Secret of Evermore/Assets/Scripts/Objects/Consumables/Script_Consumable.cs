using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_Consumable : Script_Item
{
    protected Script_Consumable()
    {
        Type = ItemType.Consumable;
    }

    public abstract void Use();
}
