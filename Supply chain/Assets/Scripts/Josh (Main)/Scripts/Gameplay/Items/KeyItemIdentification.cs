using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemIdentification : CanvasIdentification
{
    Quaternion canvasRot;
    float canvasY;

    void Start()
    {
        SetParameters();
    }

    void Update()
    {
        SetCanvasPosition();
        PlayerDetection();
    }

    override public void SetParameters()
    {
        base.SetParameters();
        canvasRot = transform.rotation;
        canvasY = transform.position.y;
    }

    // Setting canvas's position & rotation
    public void SetCanvasPosition()
    {
        transform.position = new Vector3(transform.parent.position.x, canvasY, transform.parent.position.z);
        if (transform.rotation != canvasRot) transform.rotation = canvasRot;
    }
}