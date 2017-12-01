using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_Sword : Script_Weapon
{
    public Script_Sword()
    {
        Type = ItemType.Sword;
        _power = 20;
        _range = 1.5f;
    }

    public override void Attack(Script_VisualCharacter sender)
    {
        var colHalfHeight = sender.GetComponent<Collider>().bounds.extents.y;
        var startPos = sender.transform.position;
        startPos.y = colHalfHeight;
        startPos += _range / 2f * sender.transform.forward;

        //Get enemies hit in area of effect
        var hitEnemies = Physics.OverlapBox(startPos, new Vector3(_range, _range, _range), sender.transform.rotation).Where(x => x.tag.Equals("Enemy"));
        
        //Deal damage to hit enemies
        foreach (var enemy in hitEnemies)
        {
            var enemyScript = enemy.gameObject.GetComponent<Script_EnemyBehaviour>();
            if (enemyScript != null)
            {
                var character = enemyScript.GetAttachedCharacter();
                character.TakeDamage(sender.GetAttachedCharacter().GetTotalDamage());
            }
        }
    }
}
