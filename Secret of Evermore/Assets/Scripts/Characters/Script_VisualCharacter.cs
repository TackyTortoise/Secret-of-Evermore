using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_VisualCharacter : MonoBehaviour
{
    protected Script_Character _attachedCharacter;
    
    public void SetCharacter(Script_Character character)
    {
        _attachedCharacter = character;
    }

    public Script_Character GetAttachedCharacter()
    {
        return _attachedCharacter;
    }
}
