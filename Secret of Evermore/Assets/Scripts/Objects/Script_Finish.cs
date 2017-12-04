using UnityEngine;

public class Script_Finish : MonoBehaviour
{
    //Object that finishes game when object is in trigger

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Script_GameManager.GetInstance().CompleteGame(true);
        }
    }
}
