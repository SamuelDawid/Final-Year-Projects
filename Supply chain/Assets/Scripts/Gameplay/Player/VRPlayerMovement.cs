using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerMovement : PlayerMovement
{
    void Awake()
    {
        AwakeConditions();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Start()
    { StartConditions(); }

    void Update()
    { GeneralActions(); }

    void FixedUpdate()
    { MovementStates(); }

    public override void Movement()
    {
        // General control for moving. Modify if you want, but make sure that it goes before 'base.Movement()'
        movement = orientation.forward * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");

        // Rotation on the y-axis based on camera movement. (Model should've been unclamped, but still is for some reason)
        transform.rotation = new Quaternion(transform.rotation.x, Quaternion.SlerpUnclamped(transform.rotation, playerCamera.transform.rotation, 1).y, transform.rotation.z, 1);

        // Base movement from 'PlayerMovement'
        base.Movement();
    }
}