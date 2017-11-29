﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_CharacterManager
{
    private List<Script_Character> _characterList = new List<Script_Character>();
    private List<Script_Character> _enemyList = new List<Script_Character>();
    private Script_Character _selectedCharacter;

    public Script_CharacterManager()
    {
        _characterList.Add(new Script_PlayerCharacter());

        _selectedCharacter = _characterList[0];
        Camera.main.GetComponent<Script_FollowCamera>().SetTarget(_selectedCharacter.GetVisualCharacter().transform);
    }

    public void SwitchCharacter()
    {
        _selectedCharacter = _characterList.FirstOrDefault(x => x != _selectedCharacter);
        Camera.main.GetComponent<Script_FollowCamera>().SetTarget(_selectedCharacter.GetVisualCharacter().transform);
    }
}
