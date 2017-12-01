using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_Character
{
    protected String _characterName;
    protected int _health = 0;
    public int CurrentHealth { get { return _health; } }
    private const int _maxHealth = 100;
    public int MaxHealth { get { return _maxHealth; } }
    protected int _attack = 10;
    public int Attack { get { return _attack; } }
    protected int _defense = 10;
    protected Script_VisualCharacter _visualCharacter;
    protected Script_Weapon _weapon;
    public Script_Weapon Weapon { get { return _weapon; } }

    public Script_Character()
    {
        _health = _maxHealth;
    }

    public Script_VisualCharacter GetVisualCharacter()
    {
        return _visualCharacter;
    }

    public void TakeDamage(int number)
    {
        Debug.Log(_characterName + " taking " + number + " damage");
        _health -= number;
        //if (_health <= 0)
        //    GameObject.Destroy(_visualCharacter.gameObject);
    }
}
