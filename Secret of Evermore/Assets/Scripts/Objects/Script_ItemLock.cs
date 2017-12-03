using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Script_ItemLock : MonoBehaviour
{
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

        transform.GetChild(0).gameObject.SetActive(true);

        var item = Script_GameManager.GetInstance().Inventory.GetUnEquipedItems().FirstOrDefault(x => x.Name == ItemName);
        if (item != null)
        {
            Destroy(gameObject);
            Script_GameManager.GetInstance().Inventory.RemoveItem(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;

        transform.GetChild(0).gameObject.SetActive(false);
    }
}
