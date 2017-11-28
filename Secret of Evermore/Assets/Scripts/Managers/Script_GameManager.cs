using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GameManager : MonoBehaviour
{
    private static Script_GameManager _instance;

    private static Script_CharacterManager _characterManager;

    // Use this for initialization
    void Start()
    {
        _instance = this;
        _characterManager = new Script_CharacterManager();
    }

    public static Script_GameManager GetInstance()
    {
        if (_instance == null)
            Debug.LogError("No GameManager present in scene");

        return _instance;
    }

    public void CreateCharacter(Script_Character c)
    {
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.AddComponent<Script_CharacterBehaviour>();
        var vc = player.AddComponent<Script_VisualCharacter>();
        vc.SetCharacter(c);
    }
}
