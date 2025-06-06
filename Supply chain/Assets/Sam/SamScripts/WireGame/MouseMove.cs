using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MouseMove : MonoBehaviour
{
    //public Transform Anchor;
    //private Vector3 initialScale;
    private Camera wireCamera;
    private float CameraZDistance;

    private bool connected;
    
    private Vector3 initialPosition;
    private const string portTag = "Port";
    private const float dragResponseTreshold = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        wireCamera = GameObject.Find("WireCamera").GetComponent<Camera>();
        CameraZDistance = wireCamera.WorldToScreenPoint(transform.position).z;
    }

    // Update is called once per frame
   
    void OnMouseDrag()
    {
        Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance);
        Vector3 WorldPosition = wireCamera.ScreenToWorldPoint(ScreenPosition);
        //transform.position = newWorldPosition;

        if(!connected)
        {
            transform.position = WorldPosition;
        }else if(Vector3.Distance(transform.position, WorldPosition) > dragResponseTreshold)
        {
            connected = false;
        }
        
    }
    private void OnMouseUp()
    {
        if(!connected)
        {
            ResetPosition();
        }
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetInitialPosition(Vector3 NewPosition)
    {
        initialPosition = NewPosition;
        transform.position = initialPosition;
    }
    private void ResetPosition()
    {
        transform.position = initialPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(portTag))
        {
            connected = true;
            transform.position = other.transform.position;
        }
    }
}
