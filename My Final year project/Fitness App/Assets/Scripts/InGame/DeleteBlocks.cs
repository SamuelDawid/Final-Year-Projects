using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBlocks : MonoBehaviour
{
    private GameObject player;
    private float distance;
    private float maxDistane = 1.5f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
       if(distance <= maxDistane)
        {
            print(distance);
            Destroy(gameObject);
        }
        
    }
}
