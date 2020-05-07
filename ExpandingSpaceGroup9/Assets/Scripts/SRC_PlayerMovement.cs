using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRC_PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    Rigidbody rigidBody;

    public float speed = 6.0f;
    public float fuel = 100.0f;
    public float gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            _moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
            _moveDirection *= speed;
        }
        _moveDirection.y -= gravity * Time.deltaTime;
        var worldMove = transform.TransformDirection(_moveDirection);
        characterController.Move(worldMove * Time.deltaTime);
    }
}
