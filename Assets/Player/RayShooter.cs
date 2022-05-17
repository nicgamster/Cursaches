using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    public float useRange = 5.0f;
    [SerializeField] private PlayerCharacter player;
    private Camera _camera;

    [SerializeField] BoardController board;


    //[SerializeField] Play board;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked; //�������� ��������� ����
        Cursor.visible = false;
    }

    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*"); //������� GUI.Label() ���������� �� �����e ������.

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))  // ������� �� ������� �������
        {
            
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0); //������� ����� �� ������� ����������� ��� (����� ������)
            Ray ray = _camera.ScreenPointToRay(point);  //�������� � ���� ����� ���� ������� ScreenPointToRay()

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, useRange)) //���������� ��� ��������� ����������� ����������, �� ������� ������� ������.
            {
                GameObject hitObject = hit.transform.gameObject;
                WorkerRelative workerRelative = hitObject.GetComponent<WorkerRelative>();  //�������� ������, � ������� ����� ���
                if (workerRelative != null)
                {
                    workerRelative.Mission();
                }

                if (hitObject.CompareTag("Money"))
                {
                    
                    int money = Int32.Parse(hit.transform.gameObject.name.Substring(6)); //���-�� ����� �� ������� �����
                    player.TakeMoney(money);
                    Messenger<int>.Broadcast(GameEvent.MONEYCHANGED, player.Money);
                    Destroy(hit.transform.gameObject);
                    
                }

                ReactiveButton stationButton = hitObject.GetComponent<ReactiveButton>(); //������ ������ �� �����
                if (stationButton != null)
                {
                    board.StationChoosed(stationButton);

                }

                ControllCarriage buttonCarriage = hitObject.GetComponent<ControllCarriage>(); //������ ������ �� �������
                if (buttonCarriage != null)
                {
                    buttonCarriage.UseCarriage();

                }




            }
        }

    }


}
