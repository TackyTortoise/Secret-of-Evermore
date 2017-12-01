using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Script_CharacterPanel : Script_EvermorePanel
{
    private Script_Character _character;

    private Text _baseText;
    private Text _itemBoostText;

    public override void Initialise()
    {
        _character = Script_GameManager.GetInstance().CharacterManager.GetCharacters()[0];
        _baseText = transform.GetChild(0).GetComponent<Text>();
        _itemBoostText = transform.GetChild(1).GetComponent<Text>();
    }

    public override void Refresh()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Name: ");
        stringBuilder.Append(_character.Name);
        stringBuilder.Append("\n");

        stringBuilder.Append("Weapon: ");
        stringBuilder.Append(_character.Weapon.Name);
        stringBuilder.Append("\n");

        stringBuilder.Append("Health: ");
        stringBuilder.Append(_character.MaxHealth);
        stringBuilder.Append("\n");

        stringBuilder.Append("Attack: ");
        stringBuilder.Append(_character.Attack);
        stringBuilder.Append("\n");

        stringBuilder.Append("Defense: ");
        stringBuilder.Append(_character.Defense);
        stringBuilder.Append("\n");

        _baseText.text = stringBuilder.ToString();


        //Item Boost Text
        stringBuilder.Length = 0;
        stringBuilder.Append("Attack: ");
        stringBuilder.Append(Script_GameManager.GetInstance().Inventory.GetTotalAttackBoost());
        stringBuilder.Append("\n");

        stringBuilder.Append("Defense: ");
        stringBuilder.Append(Script_GameManager.GetInstance().Inventory.GetTotalDefenseBoost());
        stringBuilder.Append("\n");

        _itemBoostText.text = stringBuilder.ToString();
    }
}
