using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    public MatchCables ownerMatchCables;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out MouseMove ColliderMove))
        {
            ownerMatchCables.PairObjectInteraction(true, ColliderMove);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MouseMove ColliderMove))
        {
            ownerMatchCables.PairObjectInteraction(false, ColliderMove);
        }
    }
}
