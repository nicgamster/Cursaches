using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float pickUpRange = 1.5f;
    public float pushForce = 5.0f;
    public Transform holdParent;

    private GameObject holdObject;
    private Camera _camera;

    private GameObject TrueParent; //Так как все коробки у меня хранятся в пустом объекте, а как только я беру объект его родитель меняется, нужно сохранить информацию о первичном родителе

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (holdObject == null)
            {
                Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, .0f); //Создаем точку из которой испускается луч (центр экрана)
                Ray ray = _camera.ScreenPointToRay(point); //Создание в этой точке луча методом ScreenPointToRay()

                RaycastHit hit;


                if (Physics.Raycast(ray, out hit, pickUpRange))
                {
                    if (hit.transform.CompareTag("TakeIt"))
                    {
                        TrueParent = hit.transform.gameObject.transform.parent.gameObject;
                        PickUpObject(hit.transform.gameObject);
                    }
                    
                }
            }
            else
            {
                DropObject();
            }
        }

        if (holdObject != null)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        if (Vector3.Distance(holdParent.transform.position, holdObject.transform.position) >  .1f)
        {
            Vector3 moveDirection = holdParent.transform.position - holdObject.transform.position;
            holdObject.GetComponent<Rigidbody>().AddForce(moveDirection * pushForce);
        }
    }
    private void PickUpObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 10;

            objRig.transform.parent = holdParent.transform.parent;
            holdObject = pickObj;
        }
    }
    private void DropObject()
    {
        Rigidbody holdRig = holdObject.GetComponent<Rigidbody>();
        holdRig.useGravity = true;
        holdRig.drag = 1;

        holdObject.transform.parent = TrueParent.transform;
        TrueParent = null;
        holdObject = null;
        
    }
}
