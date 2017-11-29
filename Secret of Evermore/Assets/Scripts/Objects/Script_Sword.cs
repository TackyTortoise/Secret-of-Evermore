using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_Sword : Script_Weapon
{
    public Script_Sword()
    {
        _type = ItemType.Sword;
        _power = 20;
        _range = 1.5f;
    }

    public override void Attack(GameObject sender)
    {
        var colHalfHeight = sender.GetComponent<Collider>().bounds.extents.y;
        var startPos = sender.transform.position;
        startPos.y = colHalfHeight;
        startPos += _range / 2f * sender.transform.forward;
        var hitEnemies = Physics.OverlapBox(startPos, new Vector3(_range, _range, _range), sender.transform.rotation).Where(x => x.tag.Equals("Enemy"));
        if (hitEnemies.ToArray().Length > 0)
            Debug.Log("Hit enemy");
    }
}
