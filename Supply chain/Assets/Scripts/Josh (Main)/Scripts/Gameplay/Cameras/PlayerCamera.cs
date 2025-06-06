using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float xSense = 10, ySense = 10, xRot, yRot;

    [SerializeField] Transform orientation;

    void Update()
    {
        if (GlobalScript.gps == GlobalScript.GameplayStatus.GameplayHUD) CameraMovement();
    }

    // Camera Movement
    void CameraMovement()
    {
        // Mouse Input
        float xMovement = Input.GetAxisRaw("Mouse X") * xSense;
        float yMovement = Input.GetAxisRaw("Mouse Y") * ySense;

        xRot -= yMovement;
        yRot += xMovement;

        xRot = Mathf.Clamp(xRot, -90, 90);
        //yRot = Mathf.Clamp(xRot, -90, 90);

        // Rotation
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}