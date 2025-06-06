using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CursorToggle : MonoBehaviour
{
    [SerializeField] GameObject UI;
    static public bool cursorOn;

    [SerializeField] UnityEvent action;

    Image crosshair;

    void Start()
    {
        if (!crosshair) crosshair = FindObjectOfType<KeycardUI>().mainCrosshair;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GlobalScript.playerRef.gameObject)
        {
            Cursor.lockState = CursorLockMode.Confined;
            if(UI != null) UI.SetActive(true);
            GlobalScript.playerRef.currentHeldWeapon.SetActive(false);
            cursorOn = true;
            crosshair.gameObject.SetActive(false);
            if (action != null) action.Invoke();
            Debug.Log(1);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GlobalScript.playerRef.gameObject)
        {
            Cursor.lockState = CursorLockMode.Locked;
            GlobalScript.playerRef.currentHeldWeapon.SetActive(true);
            if(UI != null) UI.SetActive(false);
            cursorOn = false;
            crosshair.gameObject.SetActive(true);
        }
    }

    public void TurnOnCraftingMachine()
    {
        GameplayEventSystem.ges.InfoCraftingArea();
    }
}
