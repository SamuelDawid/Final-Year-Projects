using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingElements : MonoBehaviour
{
    [SerializeField] float restoredHealthAmount;


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == GlobalScript.playerRef.gameObject)
        {
            GlobalScript.playerRef.hpModification(restoredHealthAmount);
            gameObject.SetActive(false);
        } 
    }
}
