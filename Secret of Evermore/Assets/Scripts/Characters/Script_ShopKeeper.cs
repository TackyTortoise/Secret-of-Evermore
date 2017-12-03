using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ShopKeeper : MonoBehaviour
{
    public struct ShopEntry
    {
        public ShopEntry(Script_Item item, int price)
        {
            Item = item;
            Price = price;
        }
        public Script_Item Item;
        public int Price;
    }

    public List<ShopEntry> ShopEntries { get; private set; }

    private bool _playerInRange = false;

    // Use this for initialization
    void Start()
    {
        ShopEntries = new List<ShopEntry>();
        ShopEntries.Add(new ShopEntry(new Script_Axe(), 100));
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Script_GameManager.GetInstance().UIManager.ShopPanel.SetShopKeeper(this);
            Script_GameManager.GetInstance().UIManager.ShopPanel.SwitchState();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            _playerInRange = true;

        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            _playerInRange = false;

        transform.GetChild(0).gameObject.SetActive(false);
    }

    public bool BuyItem(Script_Item item)
    {
        var entry = ShopEntries.Find(x => x.Item == item);
        var inventory = Script_GameManager.GetInstance().Inventory;
        if (entry.Item != null && inventory.Currency >= entry.Price)
        {
            //Remove item from shop
            ShopEntries.Remove(entry);
            //Adjust currency
            inventory.Currency -= entry.Price;
            //Move item to inventory
            inventory.AddItem(entry.Item);
            //Refresh shop panel
            Script_GameManager.GetInstance().UIManager.ShopPanel.Refresh();
            //Refresh inventory if open
            if (Script_GameManager.GetInstance().UIManager.InventoryPanel.IsActive())
                Script_GameManager.GetInstance().UIManager.InventoryPanel.Refresh();

            return true;
        }
        return false;
    }
}
