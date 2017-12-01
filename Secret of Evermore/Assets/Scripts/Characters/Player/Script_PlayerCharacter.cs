using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class Script_PlayerCharacter : Script_Character
{
    public Script_PlayerCharacter()
    {
        _characterName = "Player";
        _weapon = new Script_Sword();

        var visCharPrefab = Resources.Load<Script_VisualCharacter>("Prefabs/Player");
        _visualCharacter = GameObject.Instantiate(visCharPrefab);
        //var cb = _visualCharacter.gameObject.AddComponent<Script_CharacterBehaviour>();
        (_visualCharacter as Script_CharacterBehaviour).SetCharacter(this);
    }
}
