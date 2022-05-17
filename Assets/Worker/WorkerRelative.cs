using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerRelative : MonoBehaviour
{
    [SerializeField] private GameObject money;
    private GameObject _money;

    [SerializeField] Canvas PlayerUI; 
    private Animator _anim;

    //private AudioSource _audioSource;
    //private bool _audio_Play;

    private bool _missionToke = false;
    public int amountOfBoxes = 0;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.END_MIS, EndMission);
    }
    private void Start()
    {
        _anim = GetComponent<Animator>();
        //_audioSource = GetComponent<AudioSource>();
        //_audioSource.enabled = true;
        //_audio_Play = false;
    }

    private void Update()
    {
        if (!_missionToke) //���� ������ ��� �� �����
        {

            Ray ray = new Ray(transform.position, transform.forward); //��� ��������� � ��� �� ���������
                                                                      //� ������������ � ��� ��
                                                                     //�����������, ��� � ��������
            RaycastHit hit;

            if (Physics.SphereCast(ray, 1.75f, out hit)) //������� ��� � ��������� ������ ���� �����������
            {
                //hit.distance > 10.0f && hit.distance < 5.0f &&
                if (hit.transform.CompareTag("Player") && hit.distance < 10.0f && hit.distance > 5.0f)
                {
                    //if (!_audio_Play)
                    //{
                    //_audioSource.Play();
                    //_audio_Play = true;
                    //}
                    _anim.SetBool("SeePlayer", true);

                }
                else
                {
                    //if (_audio_Play)
                    //{
                    //    //_audio_Play = false;
                    //}
                    _anim.SetBool("SeePlayer", false);
                }
            }
        }
    }

    public void Mission()
    {
        if (amountOfBoxes == 5)
        {
            PlayerUI.enabled = true;
            Messenger.Broadcast(GameEvent.END_MIS);
        }
        else if (_missionToke)
        {
            Debug.Log("���� ��������� ��� ������?");
        }
        else
        {
            Debug.Log("������, ��������?");
            _missionToke = true;
            PlayerUI.enabled = true;
        }
        
    }

    public void EndMission()
    {
        this.transform.parent.gameObject.SetActive(false);
        _money = Instantiate(money) as GameObject;
        _money.name = "Money 20";
        _money.transform.position = this.transform.position;
    }


}
