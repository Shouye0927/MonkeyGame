using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitive = 200f;

    float xRotation = 0f;
    float yRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //GetAxis是觀察上一楨到現在這楨的移動量
        float xMouse = Input.GetAxis("Mouse X") * mouseSensitive * Time.deltaTime;
        float yMouse = Input.GetAxis("Mouse Y") * mouseSensitive * Time.deltaTime;

        xRotation -= yMouse; //vertical viewport 

        xRotation = Mathf.Clamp(xRotation, -90f,90f);

        yRotation += xMouse;

        transform.localRotation = Quaternion.Euler(xRotation,yRotation ,0f);

        //Debug.Log("Mouse X: " + xMouse + ", Mouse Y: " + yMouse);
        //Debug.Log("X Rotation: " + xRotation + ", Y Rotation: " + yRotation);

    }
}
