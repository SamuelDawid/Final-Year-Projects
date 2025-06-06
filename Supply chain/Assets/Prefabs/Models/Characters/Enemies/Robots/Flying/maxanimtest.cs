using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maxanimtest : MonoBehaviour
{
    private Animator animState;
    [SerializeField]
    GameObject lwrist;
    [SerializeField]
    GameObject rwrist;
    RaycastHit obj;

    void Start()
    {
        
    }
    public void playdrill()
    {
        Debug.Log("drrrrr");
    }
    public void attack()
    {
        Collider[] drill = Physics.OverlapSphere(transform.GetChild(3).transform.position, 0.5f);
        foreach (Collider col in drill)
        {
            if (col.gameObject == GlobalScript.playerRef.gameObject)
            {
                IDamage att = col.transform.GetComponent<IDamage>();
                att.hpModification(-20);
            }
        }
    }
    public void hittest()
    {
        Debug.Log("hit");
    }
    public void claw()
    {
        Debug.DrawRay(lwrist.transform.position, transform.forward * 2.5f, Color.yellow, 1.0f);
        if (Physics.Raycast(lwrist.transform.position, transform.forward * 2.5f, out obj))
        {
            if (obj.transform.gameObject == GlobalScript.playerRef.gameObject)
            {
                Debug.Log("claw hit");
                GlobalScript.playerRef.transform.GetComponent<IDamage>().hpModification(Random.Range(-6, -8));
            }
        }
    }
    public void sword()
    {
        Debug.DrawRay(rwrist.transform.position, transform.forward * 2.5f, Color.yellow, 1.0f);
        if (Physics.Raycast(rwrist.transform.position, transform.forward * 2.5f, out obj))
        {
            if (obj.transform.gameObject == GlobalScript.playerRef.gameObject)
            {
                Debug.Log("sword hit");
                GlobalScript.playerRef.transform.GetComponent<IDamage>().hpModification(Random.Range(-12, -14));
            }
        }
    }
    public void spin()
    {
        if (Vector2.Distance(new Vector2(transform.position.x,transform.position.z),new Vector2(GlobalScript.playerRef.transform.position.x, GlobalScript.playerRef.transform.position.z)) < 14)
        {
            GlobalScript.playerRef.transform.GetComponent<IDamage>().hpModification(Random.Range(-2, -4));
        }
    }
}
