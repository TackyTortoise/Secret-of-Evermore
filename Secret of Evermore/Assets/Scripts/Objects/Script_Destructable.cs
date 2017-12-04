using UnityEngine;

public class Script_Destructable : MonoBehaviour
{
    [SerializeField]
    private Script_Weapon.WeaponType TypeToDestroy = Script_Weapon.WeaponType.None;
    [SerializeField]
    private int _currencyReward = 0;

    public void Destruct(Script_Weapon sendWeapon)
    {
        if (sendWeapon == null)
            return;

        //Destroy object if hit by correct type of weapon
        if (sendWeapon.WeapType == TypeToDestroy || TypeToDestroy == Script_Weapon.WeaponType.None)
        {
            Destroy(gameObject);
            Script_GameManager.GetInstance().Inventory.Currency += _currencyReward;
        }
    }
}
