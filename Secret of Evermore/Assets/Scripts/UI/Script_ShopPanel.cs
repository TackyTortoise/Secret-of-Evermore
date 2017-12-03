using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_ShopPanel : Script_EvermorePanel
{
    public Script_ShopKeeper AttachedKeeper { get; private set; }
    private Transform _shopGrid;
    private Script_VisualItemUI _itemImagePrefab;
    private Text _currencyText;

    public override void Initialise()
    {
        _shopGrid = transform.GetChild(0);
        _currencyText = transform.GetChild(1).GetComponent<Text>();
        _itemImagePrefab = Resources.Load<Script_VisualItemUI>("Prefabs/UI/ItemInventoryImage");
    }

    public override void Refresh()
    {
        foreach (Transform child in _shopGrid)
        {
            Destroy(child.gameObject);
        }

        if (AttachedKeeper == null)
            return;
        foreach (var entry in AttachedKeeper.ShopEntries)
        {
            var obj = Instantiate(_itemImagePrefab);
            obj.SetItem(entry.Item);
            obj.VisualType = Script_VisualItemUI.VisualItemType.Shop;
            obj.transform.SetParent(_shopGrid);
        }

        _currencyText.text = Script_GameManager.GetInstance().Inventory.Currency + " G";
    }

    public void SetShopKeeper(Script_ShopKeeper keeper)
    {
        AttachedKeeper = keeper;
    }
}
