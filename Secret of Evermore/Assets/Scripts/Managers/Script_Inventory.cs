using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class Script_Inventory
{
    private List<Script_Item> _itemList;// = new List<Script_Item>();

    public Script_Inventory()
    {
        _itemList = new List<Script_Item>();
    }

    public void AddItem(Script_Item item)
    {
        //Check if item type already exists in inventory
        if (_itemList.Count > 0)
        {
            var inventoryItem = _itemList.FirstOrDefault(x => x.Type == item.Type && x.Name == item.Name);
            if (inventoryItem != null && inventoryItem.Type == Script_Item.ItemType.Consumable)
            {
                //Add amount
                inventoryItem.Amount += item.Amount;
                return;
            }
        }

        //Add new item type in inventory
        _itemList.Add(item);
    }

    public void RemoveItem(Script_Item item, int amount = 1)
    {
        //Find inventory slot of item type
        var inventoryItem = _itemList.First(x => x.Type == item.Type);
        if (inventoryItem != null)
        {
            //Remove amount of items
            inventoryItem.Amount -= amount;

            //Delete item type from inventory if none are left
            if (inventoryItem.Amount <= 0)
            {
                _itemList.Remove(inventoryItem);
                Script_GameManager.GetInstance().UIManager.InventoryPanel.Refresh();
            }
        }
    }

    public int GetTotalAttackBoost()
    {
        return GetEquipedItems().Sum(x => x.AttackBoost);
    }

    public int GetTotalDefenseBoost()
    {
        return GetEquipedItems().Sum(x => x.DefenseBoost);
    }

    public List<Script_Item> GetEquipedItems()
    {
        return _itemList.Where(x => x.Equiped).ToList();
    }

    public List<Script_Item> GetUnEquipedItems()
    {
        return _itemList.Where(x => !x.Equiped).ToList();
    }
}
