using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private WorkerRelative worker;
    [SerializeField] private Text scoreLabel;
    [SerializeField] private Image blackScreen;

    private void Awake()
    {
        GetComponent<Canvas>().enabled = false;
        Messenger.AddListener(GameEvent.BOX_CAME, BoxCame); 
        Messenger.AddListener(GameEvent.BOX_OUT, BoxOut);
        Messenger.AddListener(GameEvent.END_MIS, EndMission);
        

    }

    private void BoxCame()
    {
        worker.amountOfBoxes++;
        scoreLabel.text = worker.amountOfBoxes.ToString();
    }

    private void BoxOut()
    {
        worker.amountOfBoxes--;
        scoreLabel.text = worker.amountOfBoxes.ToString();
    }

    private void EndMission()
    {
        StartCoroutine(StartEndCutscene());
        
    }



    private IEnumerator StartEndCutscene() //Сопрограммы пользуются функциями IEnumerator
    {
        blackScreen.enabled = true;
        yield return new WaitForSeconds(2); //Ключевое слово yield указывает сопрограмме, когда следует остановиться
        blackScreen.enabled = false;
        GetComponent<Canvas>().enabled = false;
    }
}
