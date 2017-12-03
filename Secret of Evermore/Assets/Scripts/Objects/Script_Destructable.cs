using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Destructable : MonoBehaviour
{
    public Script_Weapon.WeaponType TypeToDestroy = Script_Weapon.WeaponType.None;

    public void Destruct(Script_Weapon sendWeapon)
    {
        if (sendWeapon == null)
            return;
        if (sendWeapon.WeapType == TypeToDestroy || TypeToDestroy == Script_Weapon.WeaponType.None)
        {
            Destroy(gameObject);
        }
    }
}
