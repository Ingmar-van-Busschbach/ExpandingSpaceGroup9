using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRC_CameraController : MonoBehaviour
{
    Vector2 mouseLook; //Vector for mouse rotation
    Vector2 charLook; //Vector for character rotation
    Vector2 smoothMouse; //Smoothing of rotation of mouse
    Vector2 smoothChar; //Smoothing of rotation of character
    public float sensitivity = 5.0f; //Global sensitivity
    public float smoothing = 2.0f; //Global smoothing
    GameObject character; //Gameobject to rotate
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        character = this.transform.parent.gameObject; //Get parent gameobject to rotate
        Cursor.lockState = CursorLockMode.Locked; //Lock mouse cursor
    }

    // Update is called once per frame
    void Update()
    {
        var mouseVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")); //Get input mouse
        var charVector = new Vector2(Input.GetAxis("Horizontal")/10, Input.GetAxisRaw("Mouse Y")); //Get input character
        mouseVector = Vector2.Scale(mouseVector, new Vector2(sensitivity * smoothing, sensitivity * smoothing)); //Scale input mouse
        charVector = Vector2.Scale(charVector, new Vector2(sensitivity * smoothing, sensitivity * smoothing)); //Scale input character
        smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseVector.x, 1f / smoothing); //Smooth mouse X
        smoothMouse.y = Mathf.Lerp(smoothMouse.y, mouseVector.y, 1f / smoothing); //Smooth mouse Y
        smoothChar.x = Mathf.Lerp(smoothChar.x, charVector.x, 1f / smoothing); //Smooth character X
        smoothChar.y = Mathf.Lerp(smoothChar.y, charVector.y, 1f / smoothing); //Smooth character Y
        mouseLook += smoothMouse; //Set rotation mouse
        charLook += smoothChar; //Set rotation character

        //mouseLook.y has max angle of -45 to 45, so you cant look behind you by turning vertically or clip your camera through the terrain.
        mouseLook.y = Mathf.Clamp(mouseLook.y, -45f, 45f);

        //Set rotation of local object, of which the camera and robot body are attached to. This way, the camera and head of the robot can be rotated.
        transform.localRotation = Quaternion.Euler(-mouseLook.y, mouseLook.x, 0f);
        //Set rotation of parent object, which rotates the robot body. It only turns the body horizontally.
        character.transform.localRotation = Quaternion.AngleAxis(charLook.x, character.transform.up);


        //This piece of code is dedicated to setting the angle of the player to match the terrain's angle.
        RaycastHit hit;
        direction = character.transform.TransformDirection(Vector3.up) * -1;
        Ray ray = new Ray(character.transform.position, direction);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(character.transform.position, direction, Color.green, 5, false);
            print(hit.normal);
            print(hit.transform.eulerAngles);
            character.transform.rotation = Quaternion.FromToRotation(character.transform.up, hit.normal) * character.transform.rotation;
        }

        //Unlock the cursor is you hit the escape button
        if (Input.GetKeyDown("escape"))
        {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
