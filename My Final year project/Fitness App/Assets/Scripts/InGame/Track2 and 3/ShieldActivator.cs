using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActivator : MonoBehaviour
{
    public Transform[] other;
    public GameObject[] colliders;

    private void FixedUpdate()
    {
        if(GlobalStaticScript.gps == GlobalStaticScript.GameplayStatus.Track3)
        {
            float dist = Vector3.Distance(other[0].position, other[1].position);

            if (dist <= 0.19f)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].gameObject.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].gameObject.SetActive(false);
                }
            }
        }
        
    }

}
