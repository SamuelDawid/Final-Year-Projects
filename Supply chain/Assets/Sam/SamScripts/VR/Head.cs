using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private Transform rootObj, followObj;
    [SerializeField] private Vector3 positionOffset, rotationOffset, headbodyOffset;

    // Update is called once per frame
    void LateUpdate()
    {
        rootObj.position = transform.position + headbodyOffset;
       // rootObj.forward = Vector3.ProjectOnPlane(followObj.up, Vector3.up).normalized;

        transform.position = followObj.TransformPoint(positionOffset);
        transform.rotation = followObj.rotation * Quaternion.Euler(rotationOffset);
    }
}
