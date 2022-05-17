using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveClip : MonoBehaviour
{
    [SerializeField] Material onMaterial;
    [SerializeField] Material offMaterial;

    private bool _glowState;

    void Start()
    {
        _glowState = false;
    }



    public void TurnClip()
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
