using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRC_CameraController : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 charLook;
    Vector2 smoothV;
    Vector2 smoothV2;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        character = this.transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        var cd = new Vector2(Input.GetAxis("Horizontal")/5, Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        cd = Vector2.Scale(cd, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        smoothV2.x = Mathf.Lerp(smoothV2.x, cd.x, 1f / smoothing);
        smoothV2.y = Mathf.Lerp(smoothV2.y, cd.y, 1f / smoothing);
        mouseLook += smoothV;
        charLook += smoothV2;

        //mouseLook.y has max angle of -90 to 90, so you cant look behind you by turning vertically.
        mouseLook.y = Mathf.Clamp(mouseLook.y, -45f, 45f);

        transform.localRotation = Quaternion.Euler(-mouseLook.y, mouseLook.x, 0f);
        character.transform.localRotation = Quaternion.AngleAxis(charLook.x, character.transform.up);

        if (Input.GetKeyDown("escape"))
        {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
