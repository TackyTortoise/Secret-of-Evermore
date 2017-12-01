using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GameManager : MonoBehaviour
{
    private static Script_GameManager _instance;

    private static Script_CharacterManager _characterManager;
    public Script_CharacterManager CharacterManager { get { return _characterManager; } }

    private static Script_Inventory _inventory;

    // Use this for initialization
    void Start()
    {
        if (_instance != null)
            Debug.LogError("Creating multiple GameManagers!");
        _instance = this;
        _characterManager = new Script_CharacterManager();
    }

    public static Script_GameManager GetInstance()
    {
        if (_instance == null)
            Debug.LogError("No GameManager present in scene");

        return _instance;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _characterManager.SwitchCharacter();
        }
    }
}
