using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterNewLevel : MonoBehaviour
{
    private int ID_level;
    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.TIMETOMOVE, NewLevelIdIonput);
    }

    private void NewLevelIdIonput(int ID)
    {
        ID_level = ID;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<CharacterController>().enabled == false)
        {
            SceneManager.LoadScene(ID_level);

        }
    }
}
