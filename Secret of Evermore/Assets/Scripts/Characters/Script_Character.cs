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
    public Script_Armor Helm { get; protected set; }
    public Script_Armor Chest { get; protected set; }
    public Script_Armor Legs { get; protected set; }

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
        CurrentHealth -= Math.Max(0, number - Defense);
    }

    public void EquipItem(Script_Item item)
    {
        if (item.Type == Script_Item.ItemType.Weapon)
        {
            UnEquip(Weapon);

            Weapon = item as Script_Weapon;
            Weapon.Equiped = true;
        }
        else if (item.Type == Script_Item.ItemType.Armor)
        {
            var armor = item as Script_Armor;
            switch (armor.Type)
            {
                case Script_Armor.ArmorType.Chest:
                    UnEquip(Chest);
                    Chest = armor;
                    Chest.Equiped = true;
                    break;
                case Script_Armor.ArmorType.Legs:
                    UnEquip(Legs);
                    Legs = armor;
                    Legs.Equiped = true;
                    break;
                case Script_Armor.ArmorType.Helm:
                    UnEquip(Helm);
                    Helm = armor;
                    Helm.Equiped = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void UnEquip(Script_Item item)
    {
        if (item != null)
            item.Equiped = false;
    }
}
