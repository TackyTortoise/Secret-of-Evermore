using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Key : Script_Consumable
{
    public Script_Key()
    {
        Name = "Key";
    }

    public override void Use()
    {
        //Empty as key can not be used by player
    }
}
