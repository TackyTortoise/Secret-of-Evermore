using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_InventoryPanel : Script_EvermorePanel
{
    private Transform _equipedGrid;
    private Transform _inventoryGrid;
    private Script_VisualItemUI _inventoryImagePrefab;
    
    public override void Initialise()
    {
        _equipedGrid = transform.GetChild(0);
        _inventoryGrid = transform.GetChild(1);
        _inventoryImagePrefab = Resources.Load<Script_VisualItemUI>("Prefabs/UI/ItemInventoryImage");
    }

    public override void Refresh()
    {
        foreach (Transform child in _equipedGrid)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in _inventoryGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in Script_GameManager.GetInstance().Inventory.GetEquipedItems())
        {
            var obj = Instantiate(_inventoryImagePrefab);
            obj.SetItem(item);
            obj.transform.SetParent(_equipedGrid);
        }

        foreach (var item in Script_GameManager.GetInstance().Inventory.GetUnEquipedItems())
        {
            var obj = Instantiate(_inventoryImagePrefab);
            obj.SetItem(item);
            obj.transform.SetParent(_inventoryGrid);
        }
    }
}
