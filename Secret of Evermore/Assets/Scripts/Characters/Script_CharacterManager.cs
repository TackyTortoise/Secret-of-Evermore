using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CharacterManager
{
    private List<Script_Character> _characterList = new List<Script_Character>();
    private List<Script_Character> _enemyList = new List<Script_Character>();
    private Script_Character _selectedCharacter;

    public Script_CharacterManager()
    {
        //_selectedCharacter = _characterList[0];
        _characterList.Add(new Script_Character("Filip"));
    }
}
