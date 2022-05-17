using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private Text moneyLabel;
    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.MONEYCHANGED, MoneyChanged);
    }

    private void MoneyChanged(int money)
    {
        moneyLabel.text = money.ToString();
    }
}
