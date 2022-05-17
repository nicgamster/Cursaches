using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllCarriage : MonoBehaviour
{
    [SerializeField] CarriageRelative carriage;
    [SerializeField] CharacterController playerСС; //Отбираем контроль персонжа по нажатию кнопки
    public Transform start;
    public Transform gapStation;
    public Transform endOfTheRoad;

    public bool StartButton; //Если true то это кнопка внутри кабины

    static bool stateCarriage = false;
    

    /// <summary>
    /// Отвечает за манипуляцией с кареткой
    /// </summary>
    public void UseCarriage()
    {
        if (StartButton)
        {
            playerСС.enabled = false;
            carriage.ChangeStation(endOfTheRoad);
        }
        else
        {
            if (stateCarriage)
            {
                carriage.ChangeStation(start);
            }
            else
            {
                carriage.ChangeStation(gapStation);
            }
        }
        
        stateCarriage = !stateCarriage;
    }
}
