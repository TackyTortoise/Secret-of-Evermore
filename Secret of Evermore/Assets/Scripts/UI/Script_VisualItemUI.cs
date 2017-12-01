using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Script_VisualItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private static GameObject _itemNameDisplay;
    private static Text _itemDisplayText;
    private Script_Item _attachedItem;

    public void Start()
    {
        if (_itemNameDisplay == null)
            _itemNameDisplay = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemNameDisplay"));

        _itemNameDisplay.transform.SetParent(Script_GameManager.GetInstance().UIManager.Canvas.transform);
        _itemDisplayText = _itemNameDisplay.transform.GetChild(1).GetComponent<Text>();
        _itemNameDisplay.SetActive(false);
    }

    public void SetItem(Script_Item item)
    {
        _attachedItem = item;
        switch (item.Type)
        {
            case Script_Item.ItemType.Weapon:
                GetComponent<Image>().color = Color.red;
                break;
            case Script_Item.ItemType.Armor:
                GetComponent<Image>().color = Color.blue;
                break;
            default:
                GetComponent<Image>().color = Color.white;
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Update()
    {
        if (_itemNameDisplay.activeSelf)
            _itemNameDisplay.transform.position = Input.mousePosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _itemNameDisplay.SetActive(true);
        _itemDisplayText.text = _attachedItem != null ? _attachedItem.Name : "Dummy";
        _itemNameDisplay.transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _itemNameDisplay.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _attachedItem.Equiped = !_attachedItem.Equiped;
        //Update visual inventory
        Script_GameManager.GetInstance().UIManager.InventoryPanel.Refresh();
        //Update equiped stats for if characterwindow would be open
        Script_GameManager.GetInstance().UIManager.CharacterPanel.Refresh();
    }
}
