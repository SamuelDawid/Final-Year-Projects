using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCameraControl : MonoBehaviour
{
    Camera cam;

    Vector3 originalPosition;

    [SerializeField] bool travelForward;
    [SerializeField] bool travelHorizontally;
    [SerializeField] bool travelVertically;

    [SerializeField] float forwardBounds;
    [SerializeField] float horizontalBounds;
    [SerializeField] float verticalBounds;


    void Awake()
    {
        cam = GetComponent<Camera>();
        originalPosition = cam.transform.position;
    }

    void Update()
    {
        CameraControl();
    }

    void CameraControl()
    {
        if (cam.enabled)
        {
            
            // Keyboard Input
            float xMovement = 0;
            float yMovement = 0;
            float zMovement = 0;

            if (travelHorizontally) xMovement = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
            if (travelVertically) yMovement = Input.GetAxisRaw("Vertical") * Time.deltaTime;
            if (travelForward) zMovement = Input.GetAxisRaw("Vertical") * Time.deltaTime;

            if (travelHorizontally && travelForward)
            {
                xMovement = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
                zMovement = Input.GetAxisRaw("Vertical") * Time.deltaTime;
                transform.localPosition += new Vector3(xMovement, 0, zMovement);
            }

            else if (travelVertically && travelForward)
            {
                yMovement = Input.GetAxisRaw("Vertical") * Time.deltaTime;
                zMovement = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
                transform.localPosition += new Vector3(0, yMovement, zMovement);
            }

            else if (travelHorizontally && travelVertically)
            {
                xMovement = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
                yMovement = Input.GetAxisRaw("Vertical") * Time.deltaTime;
                transform.localPosition += new Vector3(xMovement, yMovement, 0);
            }

            else return;

            if (transform.localPosition.x > (originalPosition.x + horizontalBounds)) transform.localPosition += new Vector3(-0.1f, 0, 0);
            if (transform.localPosition.x < (originalPosition.x - horizontalBounds)) transform.localPosition += new Vector3(0.1f, 0, 0);

            if (transform.localPosition.y > (originalPosition.y + verticalBounds)) transform.localPosition += new Vector3(0, 0, -0.1f);
            if (transform.localPosition.y < (originalPosition.y - verticalBounds)) transform.localPosition += new Vector3(0, 0, 0.1f);

            if (transform.localPosition.z > (originalPosition.z + forwardBounds)) transform.localPosition += new Vector3(0, 0, -0.1f);
            if (transform.localPosition.z < (originalPosition.z - forwardBounds)) transform.localPosition += new Vector3(0, 0, 0.1f);
        }
    }
}
