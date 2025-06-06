using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreTitle : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] Image fadeInScreen;

    Action action;

    void Awake()
    {
        fadeInScreen.gameObject.SetActive(false);
        action += SwitchScenes;
    }

    public void WithoutVR()
    {SetSettings(GlobalScript.PlayType.NoVR);}

    public void WithVR()
    {SetSettings(GlobalScript.PlayType.VR);}

    void SetSettings(GlobalScript.PlayType pt)
    {
        cg.interactable = false;
        GlobalScript.playType = pt;
        fadeInScreen.gameObject.SetActive(true);
        fadeInScreen.ScreenFade(1, 0.6f, false, action);
    }

    void SwitchScenes()
    {
        GameManager.inst.SwitchArea(1, 2);
    }

    public void OverButton()
    {
        GlobalAudio.gAud.soundEffects.PlayOneShot(GlobalAudio.gAud.overButton);
    }
}