using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform PlayerBody;
    float x_rotation = 0f;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;    
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        x_rotation -= mouseY;
        //clamp to make sure the camera will not rotate over the PlayerBody
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(x_rotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
