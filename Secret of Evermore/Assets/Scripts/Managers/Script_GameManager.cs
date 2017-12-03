using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GameManager : MonoBehaviour
{
    private static Script_GameManager _instance;
    
    public Script_CharacterManager CharacterManager{get;private set;}
    public Script_UIManager UIManager { get; private set; }
    public Script_Inventory Inventory { get; private set; }
    public Script_CombatTextManager CombatTextManager { get; private set; }

    // Use this for initialization
    void Awake()
    {
        if (_instance != null)
            Debug.LogError("Creating multiple GameManagers!");
        _instance = this;

        Inventory = new Script_Inventory();
        CharacterManager = new Script_CharacterManager();
        UIManager = new Script_UIManager();
        CombatTextManager = new Script_CombatTextManager();

        Inventory.AddItem(new Script_HealthPotion());
        Inventory.AddItem(new Script_HealthPotion());
        Inventory.AddItem(new Script_HealthPotion());
        Inventory.AddItem(new Script_Spear());
        Inventory.AddItem(new Script_Armor("Bronze chest", Script_Armor.ArmorType.Chest, 2));
        Inventory.AddItem(new Script_Sword());
        Inventory.AddItem(new Script_Axe());
    }

    public static Script_GameManager GetInstance()
    {
        if (_instance == null)
            Debug.LogError("No GameManager present in scene");

        return _instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CharacterManager.SwitchPlayerCharacter();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UIManager.CharacterPanel.SwitchState();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.InventoryPanel.SwitchState();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.InventoryPanel.Hide();
            UIManager.CharacterPanel.Hide();
        }
    }
}
