using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllCarriage : MonoBehaviour
{
    [SerializeField] CarriageRelative carriage;
    [SerializeField] CharacterController player��; //�������� �������� �������� �� ������� ������
    public Transform start;
    public Transform gapStation;
    public Transform endOfTheRoad;

    public bool StartButton; //���� true �� ��� ������ ������ ������

    static bool stateCarriage = false;
    

    /// <summary>
    /// �������� �� ������������ � ��������
    /// </summary>
    public void UseCarriage()
    {
        if (StartButton)
        {
            player��.enabled = false;
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
