using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inst : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject car;
    float rego = 0.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > rego)
        {
            Instantiate(car, transform.position, transform.rotation);
            rego = Time.time + 22.5f;
        }
    }
}
