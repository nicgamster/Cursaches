using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int _money;
    public int Money
    {
        get { return _money;}
    }
    // Start is called before the first frame update
    void Start()
    {
        _money = 0;
    }

    // Update is called once per frame
    public void TakeMoney(int money)
    {
        _money += money; //Уменьшение здоровья игрока
        
    }
}
