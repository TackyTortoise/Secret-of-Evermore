using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_ItemLock : MonoBehaviour
{
    public String ItemName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        if (Script_GameManager.GetInstance().Inventory.GetUnEquipedItems().First(x => x.Name == ItemName) != null)
        {
            Destroy(gameObject);
        }
    }

}
