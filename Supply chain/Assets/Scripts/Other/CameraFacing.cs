using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCG.Other
{
    public class CameraFacing : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
