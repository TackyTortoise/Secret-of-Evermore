using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class Script_PlayerCharacter : Script_Character
{
    public Script_PlayerCharacter(CharacterType type)
    {
        Script_VisualCharacter visCharPrefab;
        switch (type)
        {
            case CharacterType.Hero:
                Name = "Player";
                visCharPrefab = Resources.Load<Script_VisualCharacter>("Prefabs/Player");
                break;
            case CharacterType.Dog:
                Name = "Dog";
                Attack = 15;
                MaxHealth = 150;
                CurrentHealth = MaxHealth;
                visCharPrefab = Resources.Load<Script_VisualCharacter>("Prefabs/Dog");
                break;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }

        CharType = type;

        _visualCharacter = GameObject.Instantiate(visCharPrefab);
        _visualCharacter.gameObject.name = Name;
        //var cb = _visualCharacter.gameObject.AddComponent<Script_CharacterBehaviour>();
        (_visualCharacter as Script_CharacterBehaviour).SetCharacter(this);
    }
}
