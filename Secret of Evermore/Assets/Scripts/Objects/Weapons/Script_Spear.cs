using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_Spear : Script_Weapon
{
    public Script_Spear()
    {
        Name = "Spear";
        AttackBoost = 17;
        _range = 4f;
    }
    public override List<Script_Character> GetHitEnemies(Script_VisualCharacter sender)
    {
        var colHalfHeight = sender.GetComponent<Collider>().bounds.extents.y;
        var startPos = sender.transform.position;
        startPos.y = colHalfHeight;
        startPos += _range / 2f * sender.transform.forward;

        //Get enemies hit in area of effect
        var hitEnemies = Physics.OverlapBox(startPos, new Vector3(1f, 1f, _range), sender.transform.rotation).Where(x => x.tag.Equals("Enemy")).ToList();

        //Convert to characters
        List<Script_Character> result = new List<Script_Character>();
        foreach (var enemy in hitEnemies)
        {
            var eb = enemy.GetComponent<Script_EnemyBehaviour>();
            if (eb != null)
                result.Add(eb.GetAttachedCharacter());
        }
        return result;
    }
}
