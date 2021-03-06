﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_Sword : Script_Weapon
{
    public Script_Sword()
    {
        Name = "Sword";
        WeapType = WeaponType.Sword;
        AttackBoost = 20;
        _range = 2f;
    }

    public override List<GameObject> GetHitObjects(Script_VisualCharacter sender)
    {
        var colHalfHeight = sender.GetComponent<Collider>().bounds.extents.y;
        var startPos = sender.transform.position;
        startPos.y = colHalfHeight;
        startPos += _range / 2f * sender.transform.forward;

        //Get enemies hit in area of effect
        var hitObjects = Physics.OverlapBox(startPos, new Vector3(1.5f, 1.5f, _range), sender.transform.rotation).ToList();
        
        //Convert to Gameobjects
        List<GameObject> result = new List<GameObject>(hitObjects.Count);
        hitObjects.ForEach(x => result.Add(x.gameObject));
        return result;
    }
}
