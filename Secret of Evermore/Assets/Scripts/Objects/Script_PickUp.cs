using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PickUp : MonoBehaviour
{
    public enum PickupType
    {
        Key,
    }

    [SerializeField]
    private PickupType _type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        switch (_type)
        {
            case PickupType.Key:
                Script_GameManager.GetInstance().Inventory.AddItem(new Script_Key());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Destroy(gameObject);
    }


}
