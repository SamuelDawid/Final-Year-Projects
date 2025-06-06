using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour, ipickup
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void grab()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true;
        }
    }
    public void drop()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = false;
        }
    }
    public void addrot(float rot)
    {
        transform.Rotate(0, rot, 0);
    }
    public Vector3 getpos()
    {
        return transform.position;
    }
    public void setpos(Vector3 pos)
    {
        transform.position = pos;
    }
}
