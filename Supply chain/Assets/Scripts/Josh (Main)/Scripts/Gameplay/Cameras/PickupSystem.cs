using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField] float rayDistance = 4f;
    float rotationSpeed = 3;
    RaycastHit itemHit;

    //Transform hit;
    Transform holding;
    Rigidbody rbHolding;

    bool pickingUp;

    KeyItemIdentification kii;
    string hold;

    void Update()
    {
        scanForItem();
        CanHold();

        if (!rbHolding && GlobalScript.currentHeldKeyItem != null) GlobalScript.currentHeldKeyItem = null;
    }

    void OnEnable()
    {
        GameplayEventSystem.ges.onPickupItem += Pickup;
    }

    void OnDisable()
    {
        GameplayEventSystem.ges.onPickupItem -= Pickup;
    }

    // Scans for a pick up item
    void scanForItem()
    {
        // If raycast hits something
        if (Physics.Raycast(transform.position, transform.forward, out itemHit, rayDistance))
        {
            // Holds 'IPickUp'?
            if (itemHit.transform.GetComponent<CentralPickupStructure>() != null && !holding)
            {
                holding = itemHit.transform;
                holding.transform.gameObject.layer = LayerMask.NameToLayer("Hovered");
            }

            else if (itemHit.transform.GetComponent<CentralPickupStructure>() == null && holding)
            {
                holding.transform.gameObject.layer = LayerMask.NameToLayer("GrabbableObjects");
                holding = null;
            }
        }
        else
        {
            if (!Input.GetMouseButton(0) && holding || Input.GetMouseButton(0) && !holding)
                GameplayEventSystem.ges.PickupItem(false);
        }
    }

    void CanHold()
    {
        // (Left Mouse: Hold) Pickup Item
        if (Input.GetMouseButton(0) && holding)
        {
            if (holding.transform.GetComponent<NormalObjects>() && holding.transform.GetComponent<NormalObjects>().pickUpByPlayer == false) return;
            else GameplayEventSystem.ges.PickupItem(true);
        }
    }

    // Picks up the pick up item
    void Pickup(bool pickedUp)
    {
        if (!holding) return;

        pickingUp = pickedUp;

        // Picks up item
        if (pickingUp)
        {
            if (rbHolding == null)
            {
                rbHolding = holding.GetComponent<Rigidbody>();
                rbHolding.freezeRotation = pickingUp;
                kii = rbHolding.transform.GetComponentInChildren<KeyItemIdentification>(true);
                kii?.gameObject.SetActive(false);

                foreach (Transform pickupColliders in holding.transform)
                {
                    if (pickupColliders.GetComponent<Collider>())
                        Physics.IgnoreCollision(transform.parent.GetComponent<Collider>(), holding.GetChild(1).GetComponent<Collider>(), pickingUp);
                }

                GlobalScript.currentHeldKeyItem = rbHolding.gameObject.GetComponent<KeyItem>()?.infoKID;

            }

            rbHolding.velocity = holding.GetComponent<IPickUp>().LaunchSpeed() * (transform.GetChild(0).position - holding.transform.position);
            if (holding.GetComponent<IPickUp>().CanRotate()) RotateHeld();

            // If the player is far away from a pick up item, drop it
            if (Vector3.Distance(transform.position, holding.transform.position) > 50f && rbHolding)
            {
                rbHolding.velocity = Vector3.zero;
                GameplayEventSystem.ges.PickupItem(false);
            }
        }

        // Drops item
        else if (!pickingUp)
        {
            if (rbHolding)
            {
                rbHolding.freezeRotation = pickingUp;
                rbHolding = null;
                kii?.gameObject.SetActive(true);
            }

            foreach (Transform pickupColliders in holding?.transform)
            {
                if (pickupColliders.GetComponent<Collider>())
                    Physics.IgnoreCollision(transform.parent.GetComponent<Collider>(), holding.GetChild(1).GetComponent<Collider>(), pickingUp);
            }

            holding.transform.gameObject.layer = LayerMask.NameToLayer("GrabbableObjects");
            holding = null;
            GlobalScript.currentHeldKeyItem = null;
        }
    }

    // Rotates the Key Item
    void RotateHeld()
    {
        if (Input.GetKey(GlobalScript.rotateKIX)) holding.Rotate(transform.forward, rotationSpeed, Space.World);
        if (Input.GetKey(GlobalScript.rotateKIY)) holding.Rotate(transform.right, rotationSpeed, Space.World);
        if (Input.GetKey(GlobalScript.rotateKIZ)) holding.Rotate(transform.up, rotationSpeed, Space.World);
    }
}