using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_Weapon : Script_Item
{
    public enum WeaponType
    {
        Sword,
        Spear,
        Axe,
        None
    }

    public WeaponType WeapType { get; protected set; }

    protected float _range = 0f;

    public abstract List<GameObject> GetHitObjects(Script_VisualCharacter sender);

    protected Script_Weapon()
    {
        Type = ItemType.Weapon;
    }
}
