using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Script_ItemLock : MonoBehaviour
{
    //Object that destroys when player enters in range, with certain item in inventory
    //Used as door which requires key

    public String ItemName;

    private void Start()
    {
        transform.GetChild(0).GetComponentInChildren<Text>().text = "Collect " + ItemName + " to open";
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        //Show text
        transform.GetChild(0).gameObject.SetActive(true);

        //Check if item is in inventory
        var item = Script_GameManager.GetInstance().Inventory.GetUnEquipedItems().FirstOrDefault(x => x.Name == ItemName);
        if (item != null)
        {
            //Destroy object and remove required item from inventory
            Destroy(gameObject);
            Script_GameManager.GetInstance().Inventory.RemoveItem(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;

        //Disable text
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
