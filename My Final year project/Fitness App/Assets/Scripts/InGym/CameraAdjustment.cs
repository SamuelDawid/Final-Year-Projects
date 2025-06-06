using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjustment : MonoBehaviour
{
    private Vector3 camerPosition;
    private void Start()
    {
        camerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camerPosition;
    }
}
