using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
abstract public class CentralPickupStructure : MonoBehaviour, IPickUp, IRobotPickup
{
    [HideInInspector] Rigidbody rb;
    Collider col;
    public float launchSpeed;
    public Action givenAction;
    public bool isRotatable;
    public float rotateSpeed;

    // --------------------------------------------------------

    public void SetParameters()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>() ? GetComponent<Collider>() : null;
    }

    // Returns object's launch speed
    public float LaunchSpeed()
    {return launchSpeed;}

    // Can the object be rotated whilst held?
    public bool CanRotate()
    { return isRotatable; }

    // Returns object
    public GameObject ThisObject()
    {return gameObject;}

    // --------------- SPECIFICALLY WITH NORMAL OBJECTS ----------->

    // Is the object kinematic
    public void GrabDrop(bool isKinematic)
    {rb.isKinematic = isKinematic;}

    // Sets object's parent
    public void SetParent(Transform parent)
    {
        if (transform.parent != parent)
        {
            transform.parent = parent.transform;
            GrabDrop(true);
            if (col) col.isTrigger = true;
        }

        if (transform.position != parent.transform.position)
            transform.position = Vector3.Lerp(transform.position, parent.transform.position, 2 * Time.deltaTime);
    }

    public void DetachFromParent()
    {
        if (gameObject.transform.parent) gameObject.transform.parent = null;
        if (col) col.isTrigger = false;
        GrabDrop(false);
    }

    public virtual void ExecuteAction(Transform pickup)
    {
        Debug.Log(pickup);
        //givenAction();
    }

    // -----------------------------> LIST OF 'GIVEN ACTIONS' HERE:


}