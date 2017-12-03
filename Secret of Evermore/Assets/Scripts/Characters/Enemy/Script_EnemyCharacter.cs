using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Script_EnemyCharacter : Script_Character
{
    public Script_EnemyCharacter()
    {
        Name = "Enemy";
        Attack = 20;
        var visCharPrefab = Resources.Load<Script_VisualCharacter>("Prefabs/BasicEnemy");
        _visualCharacter = GameObject.Instantiate(visCharPrefab);
        _visualCharacter.SetCharacter(this);
    }

    protected override void Die()
    {
        GameObject.Destroy(_visualCharacter.gameObject);
        Script_GameManager.GetInstance().Inventory.Currency += 50;
    }
}
