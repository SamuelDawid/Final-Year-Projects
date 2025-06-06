using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuOptions : Options
{
    [SerializeField] List<GameObject> areas = new List<GameObject>();

    [Header("Indicators")]
    [SerializeField] GameObject IndicatorHolder;
    [SerializeField] Sprite[] IndicatorShapes;

    void Start()
    {
        SetParameters();
    }

    void Update()
    {
        
    }

    public void SetAreaOn(GameObject area)
    {
        Debug.Log(3);

        foreach (GameObject go in areas) area.gameObject.SetActive(false);

        area.gameObject.SetActive(true);
    }

    public void SetBGM(float val)
    {
        GlobalScript.musicVolume = GlobalAudio.gAud.BGM.volume = val / 100;
    }

    public void SetSE(float val)
    {
        GlobalScript.soundEffectsVolume = GlobalAudio.gAud.soundEffects.volume = val / 100;
    }
}
