using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_Character
{
    public String Name { get; protected set; }
    public int CurrentHealth { get; protected set; }
    public int MaxHealth { get; protected set; }
    public int Attack { get; protected set; }
    public int Defense { get; protected set; }
    protected Script_VisualCharacter _visualCharacter;

    public Script_Weapon Weapon { get; protected set; }
    public Script_Armor Helm { get; protected set; }
    public Script_Armor Chest { get; protected set; }
    public Script_Armor Legs { get; protected set; }

    public CharacterType CharType { get; protected set; }

    public enum CharacterType
    {
        Enemy,
        Hero,
        Dog
    }

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
        //Get defense boost
        var equipedBoost = 0;
        Script_GameManager.GetInstance().Inventory.GetEquipedItems().ForEach(x => equipedBoost += x.DefenseBoost);
        //Take adjusted amage
        var damage = Math.Max(0, number - (Defense + equipedBoost));
        CurrentHealth -= damage;
        //Die
        if (CurrentHealth <= 0)
            Die();
        //Show combat text
        Script_GameManager.GetInstance().CombatTextManager.AddText(damage.ToString(), _visualCharacter.transform.position + 2*Vector3.up);
    }

    protected virtual void Die(){}

    public void Heal(int number)
    {
        CurrentHealth += Math.Min(number, MaxHealth - CurrentHealth);
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
            switch (armor.GearType)
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
        Script_GameManager.GetInstance().UIManager.InventoryPanel.SwitchItemInventoryList(item.UIItem);
    }

    public void UnEquip(Script_Item item)
    {
        if (item != null && item.Equiped)
        {
            item.Equiped = false;
            if (item == Weapon)
                Weapon = null;
            if (item == Helm)
                Helm = null;
            if (item == Chest)
                Chest = null;
            if (item == Legs)
                Legs = null;
            Script_GameManager.GetInstance().UIManager.InventoryPanel.SwitchItemInventoryList(item.UIItem);
        }
    }
}
