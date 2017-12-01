using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_Character
{
    protected String _characterName;
    protected int _health = 100;
    protected int _attack = 10;
    protected int _defense = 10;
    protected Script_VisualCharacter _visualCharacter;
    protected Script_Weapon _weapon;

    public Script_VisualCharacter GetVisualCharacter()
    {
        return _visualCharacter;
    }

    public Script_Weapon GetWeapon()
    {
        return _weapon;
    }

    public void TakeDamage(int number)
    {
        Debug.Log(_characterName + " taking " + number + " damage");
        _health -= number;
        if (_health <= 0)
            GameObject.Destroy(_visualCharacter.gameObject);
    }

    public int GetTotalDamage()
    {
        var total = _attack;
        if (_weapon != null)
            total += _weapon.GetPower();
        return total;
    }
}
