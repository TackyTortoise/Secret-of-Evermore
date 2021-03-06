﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Script_VisualItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public static GameObject ItemNameDisplay;
    private static Text _itemDisplayText;
    private Script_Item _attachedItem;

    public enum VisualItemType
    {
        Inventory,
        Shop
    }

    public VisualItemType VisualType;

    public void Start()
    {
        if (ItemNameDisplay == null)
            ItemNameDisplay = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemNameDisplay"));

        ItemNameDisplay.transform.SetParent(Script_GameManager.GetInstance().UIManager.Canvas.transform);
        _itemDisplayText = ItemNameDisplay.transform.GetChild(1).GetComponent<Text>();
        ItemNameDisplay.SetActive(false);
    }

    public void SetItem(Script_Item item)
    {
        _attachedItem = item;
        _attachedItem.UIItem = this;
        //Give item color based on type
        switch (item.Type)
        {
            case Script_Item.ItemType.Weapon:
                GetComponent<Image>().color = Color.red;
                break;
            case Script_Item.ItemType.Armor:
                GetComponent<Image>().color = Color.blue;
                break;
            case Script_Item.ItemType.Consumable:
                GetComponent<Image>().color = Color.green;
                break;
            default:
                GetComponent<Image>().color = Color.white;
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Update()
    {
        if (ItemNameDisplay.activeSelf)
            ItemNameDisplay.transform.position = Input.mousePosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Change item display to use this object name
        ItemNameDisplay.SetActive(true);
        _itemDisplayText.text = (_attachedItem != null ? _attachedItem.Name : "Dummy") + (_attachedItem.Amount > 1 ? " x" + _attachedItem.Amount : "");

        //Add price at end of text if shop item
        if (VisualType == VisualItemType.Shop)
            _itemDisplayText.text += " " + Script_GameManager.GetInstance().UIManager.ShopPanel.AttachedKeeper.ShopEntries
                .First(x => x.Item == _attachedItem).Price + "G";

        //Put display in front
        ItemNameDisplay.transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Hide item display name
        ItemNameDisplay.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (VisualType == VisualItemType.Inventory)
            InventoryClick();
        else if (VisualType == VisualItemType.Shop)
            ShopClick();
    }

    private void InventoryClick()
    {
        //Consume item
        if (_attachedItem.Type == Script_Item.ItemType.Consumable)
        {
            (_attachedItem as Script_Consumable).Use();
            _itemDisplayText.text = (_attachedItem != null ? _attachedItem.Name : "Dummy") + (_attachedItem.Amount > 1 ? " x" + _attachedItem.Amount : "");
        }
        //Equip / Unequip item
        else
        {
            if (!_attachedItem.Equiped)
                Script_GameManager.GetInstance().CharacterManager.GetCharacters()[0].EquipItem(_attachedItem);
            else
                Script_GameManager.GetInstance().CharacterManager.GetCharacters()[0].UnEquip(_attachedItem);

            //Update equiped stats for if characterwindow would be open
            Script_GameManager.GetInstance().UIManager.CharacterPanel.Refresh();
        }
    }

    private void ShopClick()
    {
        //Buy item if possible
        if (Script_GameManager.GetInstance().UIManager.ShopPanel.AttachedKeeper.BuyItem(_attachedItem))
            ItemNameDisplay.SetActive(false);
    }
}
