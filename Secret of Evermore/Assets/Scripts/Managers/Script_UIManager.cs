using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_UIManager
{
    public Script_CharacterPanel CharacterPanel { get; private set; }
    private GameObject _canvas;

    public int ActivePanels = 0;

    public Script_UIManager()
    {
        _canvas = GameObject.Find("Canvas");
        var characterPanelPrefab = Resources.Load<Script_CharacterPanel>("Prefabs/UIPanels/CharacterPanel");
        CharacterPanel = GameObject.Instantiate(characterPanelPrefab);
        CharacterPanel.Initialise();
        CharacterPanel.transform.SetParent(_canvas.transform);
        CharacterPanel.transform.localPosition = new Vector3(0,0,0);
        CharacterPanel.Hide();
    }
}
