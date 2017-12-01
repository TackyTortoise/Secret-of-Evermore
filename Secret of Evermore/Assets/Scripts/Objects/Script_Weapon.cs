using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_Weapon : Script_Item
{
    protected int _power = 0;
    protected float _range = 0f;

    public abstract List<Script_Character> GetHitEnemies(Script_VisualCharacter sender);

    public int GetPower()
    {
        return _power;
    }
}
