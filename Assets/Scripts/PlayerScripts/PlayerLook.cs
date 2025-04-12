using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        ProcessLook();
    }

    public void ProcessLook(){   //param : Vector2 input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //caculate camera rotation up down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //applt it
        cam.transform.localRotation = Quaternion.Euler(xRotation,0 ,0);
        // rotate player
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

}
