﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class Script_HUD : MonoBehaviour
{
    struct PlayerUI
    {
        public Text HealthText;
        public Text ChargeText;
        public Script_VisualCharacter TrackCharacter;
    }

    private PlayerUI _playerUI1;
    private PlayerUI _playerUI2;

    // Use this for initialization
    void Start()
    {
        var uip1 = transform.GetChild(0);
        _playerUI1 = new PlayerUI
        {
            HealthText = uip1.transform.GetChild(0).GetComponent<Text>(),
            ChargeText = uip1.transform.GetChild(2).GetComponent<Text>(),
            TrackCharacter = Script_GameManager.GetInstance().CharacterManager.GetCharacters()[0].GetVisualCharacter()
        };
        var uip2 = transform.GetChild(1);
        _playerUI2 = new PlayerUI
        {
            HealthText = uip2.transform.GetChild(0).GetComponent<Text>(),
            ChargeText = uip2.transform.GetChild(2).GetComponent<Text>(),
            TrackCharacter = Script_GameManager.GetInstance().CharacterManager.GetCharacters()[1].GetVisualCharacter()
        };
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerUI(_playerUI1);
        UpdatePlayerUI(_playerUI2);
    }

    void UpdatePlayerUI(PlayerUI ui)
    {
        var character = ui.TrackCharacter.GetAttachedCharacter();
        ui.HealthText.text = character.CurrentHealth + " / " + character.MaxHealth;
        ui.ChargeText.text = ((ui.TrackCharacter as Script_CharacterBehaviour).GetCurrentChargePercentage() * 100).ToString("N0") + "%";
    }
}
