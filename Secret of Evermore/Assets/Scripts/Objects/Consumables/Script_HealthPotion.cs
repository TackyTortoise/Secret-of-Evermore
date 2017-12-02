using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_HealthPotion : Script_Consumable
{
    private const int _healAmount = 25;

    public Script_HealthPotion()
    {
        Name = "Health Potion";
    }

    public override void Use()
    {
        bool canBeUsed = false;
        foreach (var character in Script_GameManager.GetInstance().CharacterManager.GetCharacters())
        {
            if (character.CurrentHealth < character.MaxHealth)
            {
                character.Heal(_healAmount);
                canBeUsed = true;
            }
        }

        if (canBeUsed)
            Script_GameManager.GetInstance().Inventory.RemoveItem(this);
    }
}
