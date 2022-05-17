using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveButton : MonoBehaviour
{
    [SerializeField] Material onMaterial;
    [SerializeField] Material offMaterial;


    public Transform Parent;
    public char lineColor;

    private string closerStation;

    public string CloserStationRead
    {
        get { return closerStation; }
    }

    private int _idButton;

    public int ID
    {
        get { return _idButton; }
    }

    private bool _glowState; //Отвечает за состояние вкл/выкл

    void Start()
    {
        _glowState = false;
        lineColor = Parent.name[0];
        closerStation = Parent.name.Substring(2, Parent.name.Length - 2);
        if (transform.parent.gameObject.name.Length > 10)
        {
            _idButton = System.Int32.Parse(transform.parent.gameObject.name.Substring(8, 2));
        }
        else
        {
            _idButton = (int)char.GetNumericValue(transform.parent.gameObject.name[8]);
        }
        
    }



    public void PushButton()
    {
        if (_glowState)
        {
            _glowState = false;
            GetComponent<Renderer>().material = offMaterial;
            

        }
        else
        {
            _glowState = true;
            GetComponent<Renderer>().material = onMaterial;
        }
    }
}
