using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_Character
{
    public String Name { get; protected set; }
    public int CurrentHealth { get; protected set; }
    public int MaxHealth { get; private set; }
    public int Attack { get; protected set; }
    public int Defense { get; protected set; }
    protected Script_VisualCharacter _visualCharacter;
    public Script_Weapon Weapon { get; protected set; }

    public Script_Character()
    {
        Defense = 0;
        Attack = 10;
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
    }

    public Script_VisualCharacter GetVisualCharacter()
    {
        return _visualCharacter;
    }

    public void TakeDamage(int number)
    {
        Debug.Log(Name + " taking " + number + " damage");
        CurrentHealth -= Math.Max(0, Defense - number);
    }
}
