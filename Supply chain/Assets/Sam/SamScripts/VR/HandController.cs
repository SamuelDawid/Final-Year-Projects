using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Collections;

public class HandController : MonoBehaviour
{public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;
    //public Animator handAnimator;
    [Header("Physics Movement")]
    [SerializeField] private GameObject followObject;
    private Transform followTarget;
    private Rigidbody rb;
    private float followSpeed = 30f;
    private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    void Start()
    {
        TryInitialize();
        //Physics Movement
        followTarget = followObject.transform;
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.mass = 20f;

        // hands pos
        rb.position = followTarget.position;
        rb.rotation = followTarget.rotation;
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
    void PhysicsMove()
    {
        //Position
        var positionWithOffset = followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        rb.velocity = (followTarget.position - transform.position).normalized *(followSpeed *distance);
        //Rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var quat = rotationWithOffset * Quaternion.Inverse(rb.rotation);
        quat.ToAngleAxis(out float angle, out Vector3 axis);
        rb.angularVelocity = angle * (axis * Mathf.Deg2Rad * rotateSpeed);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        PhysicsMove();
        targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue);
        if(triggerValue)
        {
            Debug.Log("PressingButton");
        }
    }
}
   

