using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Utilities")]
    [SerializeField] CursorLockMode cursorLockMode = CursorLockMode.Locked;
    [SerializeField] bool cursorVisibility = false;

    [SerializeField][Range(10, 1000)] float sensX = 500f;
    [SerializeField][Range(10, 1000)] float sensY = 500;

    public Transform orientation;
    float yRotation;
    float xRotation;

    private void Start()
    {
        Cursor.lockState = cursorLockMode;
        Cursor.visible = cursorVisibility;
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        //Camera Rotation
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        Mathf.Clamp(xRotation, -85f, 85f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
