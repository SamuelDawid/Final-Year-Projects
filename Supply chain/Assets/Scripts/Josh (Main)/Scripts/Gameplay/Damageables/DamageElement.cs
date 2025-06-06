using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageElement : MonoBehaviour
{
    [SerializeField] GameObject interaction;

    void OnCollisionEnter(Collision collision)
    {
        if (interaction.GetComponent<IInteractWith>() != null)
            interaction.GetComponent<IInteractWith>().Effect(collision.gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (interaction.GetComponent<IInteractWith>() != null)
            interaction.GetComponent<IInteractWith>().Effect(collision.gameObject);
    }
}