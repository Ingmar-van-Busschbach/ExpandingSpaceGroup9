using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRC_PlayerMovement : MonoBehaviour
{ //Declare variables
    CharacterController characterController; //Controller for movement
    MeshRenderer material; //Mesh renderer to affect visual track movement
    AudioSource audioSource; //Audio source to affect pitch
    public float speed = 2.0f; //Global movement speed modifier
    public float gravity = 20.0f; //Global gravity modifier
    public float pitch = 0.01f; //Global pitch modifier for sound
    private Vector3 _moveDirection = Vector3.zero; //Vector declaration for movement direction

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); //Get controller
        material = GetComponent<MeshRenderer>(); //Get mesh renderer to affect tracks
        audioSource = GetComponent<AudioSource>(); //Get audio source
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded) //If player is on the ground
        { //Input new movement direction based on input * speed
            _moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
            _moveDirection *= speed;
        }

        float moveSpeed = _moveDirection.z;
        material.materials[5].SetTextureOffset("_MainTex", new Vector2(0, moveSpeed * Time.time)); //Texture offset for tracks
        audioSource.pitch = Input.GetAxis("Vertical")*pitch + 0.5f; //Audio pitch modifier

        _moveDirection.y -= gravity * Time.deltaTime; //Apply gravity
        var worldMove = transform.TransformDirection(_moveDirection); //Apply world direction to movement
        characterController.Move(worldMove * Time.deltaTime); //Move player
    }
}
