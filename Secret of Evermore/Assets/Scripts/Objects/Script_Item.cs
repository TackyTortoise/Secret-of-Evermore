using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_Item
{
    protected enum ItemType
    {
        Sword,
        None,
    }

    protected ItemType _type;

    private int _amount;
}
