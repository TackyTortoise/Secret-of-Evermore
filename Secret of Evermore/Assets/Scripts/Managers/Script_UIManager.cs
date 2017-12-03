using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_UIManager
{
    public Script_CharacterPanel CharacterPanel { get; private set; }
    public Script_InventoryPanel InventoryPanel { get; private set; }
    public Script_ShopPanel ShopPanel { get; private set; }
    public GameObject Canvas { get; private set; }

    public int ActivePanels = 0;

    public Script_UIManager()
    {
        Canvas = GameObject.Find("Canvas");
        CharacterPanel = GameObject.Instantiate(Resources.Load<Script_CharacterPanel>("Prefabs/UI/CharacterPanel"));
        CharacterPanel.Initialise();
        CharacterPanel.transform.SetParent(Canvas.transform);
        CharacterPanel.transform.localPosition = Vector3.zero;
        CharacterPanel.Hide();

        InventoryPanel = GameObject.Instantiate(Resources.Load<Script_InventoryPanel>("Prefabs/UI/InventoryPanel"));
        InventoryPanel.Initialise();
        InventoryPanel.transform.SetParent(Canvas.transform);
        InventoryPanel.transform.localPosition = Vector3.zero;
        InventoryPanel.Hide();

        ShopPanel = GameObject.Instantiate(Resources.Load<Script_ShopPanel>("Prefabs/UI/ShopPanel"));
        ShopPanel.Initialise();
        ShopPanel.transform.SetParent(Canvas.transform);
        ShopPanel.transform.localPosition = Vector3.zero;
        ShopPanel.Hide();
    }
}
