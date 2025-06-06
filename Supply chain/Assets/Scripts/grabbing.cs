using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabbing : MonoBehaviour
{
    public Transform cameraHolder;
    RaycastHit obj;
    Vector3 dep;
    Vector3 move;
    bool ismoving;
    GameObject lastobj;
    float rotationspd = 45.0f;
    // Start is called before the first frame update
    void Start()
    {
        lastobj = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        bool leftMouseButtondown = Input.GetMouseButtonDown(0);
        bool leftMouseButtonup = Input.GetMouseButtonUp(0);
        if (Physics.Raycast(cameraHolder.position, transform.forward, out obj, 10))
        {
            if (leftMouseButtondown)
            {
                if (obj.transform.GetComponent<ipickup>() != null)
                {
                    ismoving = true;
                    lastobj = obj.transform.gameObject;
                }
            }
            if (leftMouseButtonup)
            {
                if (obj.transform.GetComponent<ipickup>() != null)
                {
                    ismoving = false;
                    ipickup item = obj.transform.GetComponent<ipickup>();
                    item.drop();
                }
            }
        }
        else
        {
            ismoving = false;
            if (lastobj.GetComponent<ipickup>() != null)
                lastobj.GetComponent<ipickup>().drop();
        }
        if (ismoving)
        {
            ipickup item = obj.transform.GetComponent<ipickup>();
            item.grab();
            move = cameraHolder.transform.position + cameraHolder.transform.forward * Vector3.Distance(item.getpos(), cameraHolder.transform.position);
            dep = cameraHolder.transform.position + cameraHolder.transform.forward * 5;
            item.setpos(new Vector3(move.x, move.y, dep.z));
            item.addrot(Input.GetAxis("Mouse ScrollWheel") * rotationspd);
        }
    }
}
