using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotHit : MonoBehaviour
{
    // Start is called before the first frame update
    private LayerMask layer;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            // Add points
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == 3)
        {
            // Take points;
            Destroy(gameObject);
        }
    }
}
