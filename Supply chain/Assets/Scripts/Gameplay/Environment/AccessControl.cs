using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AccessControl : MonoBehaviour, IInteractWith
{
    [SerializeField] int accessID;
    [SerializeField] UnityEvent action;

    Animator anim;

    bool progressionDone;

    void Start()
    {
        anim = GetComponent<Animator>();
        progressionDone = false;
    }

    void Update()
    {
        if (progressionDone && anim.GetInteger("ACState") != 2) anim.SetInteger("ACState", 2);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (GlobalScript.currentHeldKeyItem?.GivenID == accessID && !progressionDone)
        {
            anim.SetInteger("ACState", 1);
        }  
    }

    void OnTriggerExit(Collider collision)
    {
        if (!progressionDone)  anim.SetInteger("ACState", 0);
    }

    void ProgressionDone()
    {
        progressionDone = true;
        Effect(null);
    }

    public void Effect(GameObject go)
    {
        action.Invoke();
    }
}