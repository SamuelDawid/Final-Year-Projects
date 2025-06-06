using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyorcar : MonoBehaviour
{
    // Start is called before the first frame update
    float startrot = 0;
    float secs;
    void Start()
    {
        secs = Time.time;
        startrot = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        int turning = 0;
        transform.position = transform.position + transform.forward * 1 * Time.deltaTime;
        if (Time.time > 3.75f+secs && Time.time < 6.0f + secs)
            turning = 1;
        if (Time.time > 17.0f + secs && Time.time < 19.0f + secs)
            turning = 2;
        if (turning == 1)
        {
            transform.Rotate(0, -45 * Time.deltaTime, 0);
            if (transform.rotation.eulerAngles.y < startrot - 90) transform.Rotate(0, startrot - 90 - transform.rotation.eulerAngles.y, 0);
        }
        if (turning == 2)
        {
            transform.Rotate(0, 45 * Time.deltaTime, 0);
            if (transform.rotation.eulerAngles.y > startrot) transform.Rotate(0, startrot - transform.rotation.eulerAngles.y, 0);
        }
        if (Time.time > 22.5f + secs && turning != 3)
        {
            //transform.gameObject.SetActive(false);
            Destroy(transform.gameObject);
        }
    }
}
