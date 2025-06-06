using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDMGLifeTime : MonoBehaviour
{
    public float range;
    public int dmg;
    private float countDown;
    Collider[] collide;
    // Start is called before the first frame update
   void Update()
   {    
       countDown += Time.deltaTime;
       if(countDown > 3)
       {
        collide = Physics.OverlapSphere(transform.position,range);
        foreach(var p in collide)
        {
            if(p.CompareTag("Player"))
                GlobalScript.playerRef.hpModification(-dmg);
        }
      countDown = 0;
       }

   }
}
