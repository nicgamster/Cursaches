using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageRelative : MonoBehaviour
{
    public Transform _finish;
    [SerializeField] CharacterController player��;

    public float smoothTime = 0.2f;
    private Vector3 _velocity = Vector3.zero;
     

    void Update()
    {
        Vector3 targetPosition = new Vector3(
            _finish.position.x, _finish.position.y, _finish.position.z); //��������� ���������� Z, ����� �������� X � Y

        transform.position = Vector3.SmoothDamp(transform.position,
                            targetPosition, ref _velocity, smoothTime);
    }

    public void ChangeStation(Transform station)
    {

       _finish = station;

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {          
            other.transform.gameObject.transform.parent = this.transform;
            
        }
        if (other.name == "GapOut")
        {
            player��.enabled = true;
        }
    }
}
