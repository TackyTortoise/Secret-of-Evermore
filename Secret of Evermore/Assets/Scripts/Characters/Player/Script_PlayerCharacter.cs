using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class Script_PlayerCharacter : Script_Character
{
    public enum PlayerType
    {
        Hero,
        Dog
    }

    public Script_PlayerCharacter(PlayerType type)
    {
        Script_VisualCharacter visCharPrefab;
        switch (type)
        {
            case PlayerType.Hero:
                _characterName = "Player";
                _weapon = new Script_Sword();
                visCharPrefab = Resources.Load<Script_VisualCharacter>("Prefabs/Player");
                break;
            case PlayerType.Dog:
                _characterName = "Dog";
                visCharPrefab = Resources.Load<Script_VisualCharacter>("Prefabs/Dog");
                break;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }

        _visualCharacter = GameObject.Instantiate(visCharPrefab);
        _visualCharacter.gameObject.name = _characterName;
        //var cb = _visualCharacter.gameObject.AddComponent<Script_CharacterBehaviour>();
        (_visualCharacter as Script_CharacterBehaviour).SetCharacter(this);
    }
}
