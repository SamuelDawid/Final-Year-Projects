using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMaterial : MonoBehaviour
{
    public CraftingMaterialData info;
    [HideInInspector] public bool isPickupable;
    Rigidbody rb;

    void OnEnable()
    {
        if (gameObject.GetComponent<Rigidbody>()) rb = GetComponent<Rigidbody>();
        gameObject.name = info.GivenName;
        StartCoroutine(WaitUntilPickup());

        transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
        Vector3 forcePos = transform.right * Random.Range(-10, 10) + transform.up * 10;
        rb.AddForce(forcePos, ForceMode.VelocityChange);
    }

    void OnDisable()
    {
        isPickupable = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7) rb.isKinematic = true;
    }

    void Update()
    {
        transform.Rotate(-Vector3.up, 45 * Time.deltaTime);
    }

    IEnumerator WaitUntilPickup()
    {
        while (!isPickupable)
        {
            yield return new WaitForSeconds(2);
            isPickupable = true;
        }
    }
}
