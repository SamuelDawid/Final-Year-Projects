using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitDetection : MonoBehaviour
{
    public LayerMask layer;
    //public GameObject VFX;
    private int SFXindex;
    private void Update()
    {
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 1, layer))
        {
            print(Vector3.Angle(transform.position, hit.transform.forward));
            if (Vector3.Angle(transform.position, hit.transform.forward) > 90   && Vector3.Angle(transform.position,hit.transform.forward) < 130)
            {
                EventSystem.ES.PlaySFX(SFXindex);
                EventSystem.ES.AddScore(GlobalStaticScript.ScoreValue);
                //GlobalStaticScript.comboCount++;
                Destroy(hit.transform.gameObject);
                EventSystem.ES.UpdateScoreText();
            }
        }
       
        
    }
}
