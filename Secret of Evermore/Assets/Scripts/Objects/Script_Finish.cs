using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Finish : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Script_GameManager.GetInstance().CompleteGame(true);
        }
    }
}
