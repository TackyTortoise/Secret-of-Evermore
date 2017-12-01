using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Script_EnemyCharacter : Script_Character
{
    public Script_EnemyCharacter()
    {
        Name = "Enemy";
        var visCharPrefab = Resources.Load<Script_VisualCharacter>("Prefabs/BasicEnemy");
        _visualCharacter = GameObject.Instantiate(visCharPrefab);
        _visualCharacter.SetCharacter(this);
    }
}
