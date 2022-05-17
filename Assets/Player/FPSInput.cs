using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 2.0f;
    public float jumpForce;


    private CharacterController _charController;

    public float gravity = -9.8f;
    private float _vertSpeed;
    public float minFall = -1.5f;
    public float terminalVelocity = -10.0f;
    bool hitGround = false;


    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;

    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement = transform.TransformDirection(movement);


        //Debug.Log(_charController.isGrounded);

        hitGround = _charController.isGrounded;
        if (hitGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                //Debug.Log(_charController.isGrounded);
                _vertSpeed = jumpForce;
            }
            else
            {
                _vertSpeed = minFall;
            }
        }
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
        }

        movement.y = _vertSpeed;
        movement *= Time.deltaTime;

        _charController.Move(movement);


    }
}
