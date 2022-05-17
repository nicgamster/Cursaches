using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TakeIt"))
        {
            Messenger.Broadcast(GameEvent.BOX_CAME);
        }
    }

    private void OnTriggerExit(Collider other)
    {    
        if (other.CompareTag("TakeIt"))
        {
            Messenger.Broadcast(GameEvent.BOX_OUT);
        }
    }

}
