using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_Inventory
{
    private List<Script_Item> _itemList = new List<Script_Item>();

    public void AddItem(Script_Item item)
    {
        //Check if item type already exists in inventory
        var inventoryItem = _itemList.First(x => x.Type == item.Type);
        if (inventoryItem != null)
        {
            //Add amount
            inventoryItem.Amount += item.Amount;
        }
        else
        {
            //Add new item type in inventory
            _itemList.Add(item);
        }
    }

    public void RemoveItem(Script_Item item)
    {
        //Find inventory slot of item type
        var inventoryItem = _itemList.First(x => x.Type == item.Type);
        if (inventoryItem != null)
        {
            //Remove amount of items
            inventoryItem.Amount -= item.Amount;

            //Delete item type from inventory if none are left
            if (inventoryItem.Amount <= 0)
                _itemList.Remove(inventoryItem);
        }
    }
}
