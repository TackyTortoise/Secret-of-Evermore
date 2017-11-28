using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Character
{
    private String _characterName;
    private int _health = 100;
    private int _attack = 10;
    private int _defense = 10;

    public Script_Character(String name)
    {
        _characterName = name;
        Script_GameManager.GetInstance().CreateCharacter(this);
    }
}
