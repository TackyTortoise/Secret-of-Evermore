using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_Weapon : Script_Item
{
    protected float _range = 0f;

    public abstract List<Script_Character> GetHitEnemies(Script_VisualCharacter sender);

    protected Script_Weapon()
    {
        Type = ItemType.Weapon;
    }
}
