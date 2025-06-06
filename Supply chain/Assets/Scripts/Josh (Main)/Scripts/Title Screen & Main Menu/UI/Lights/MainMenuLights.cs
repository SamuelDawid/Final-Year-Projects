using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLights : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject roomLights;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        MainMenuEventSystem.GESMainMenu.onTitleToMain += LightRoomLights;
        MainMenuEventSystem.GESMainMenu.onPlayStage += GoingToGameplay;
        MainMenuEventSystem.GESMainMenu.onBackFromStage += BackFromGameplay;
    }

    void OnDisable()
    {
        MainMenuEventSystem.GESMainMenu.onTitleToMain -= LightRoomLights;
        MainMenuEventSystem.GESMainMenu.onPlayStage -= GoingToGameplay;
        MainMenuEventSystem.GESMainMenu.onBackFromStage -= BackFromGameplay;
    }

    // Title Screen > Main Menu
    void LightRoomLights()
    {
        anim.SetInteger("LightState", 1);
    }

    // Main Menu > Gameplay
    void GoingToGameplay()
    { 
        anim.SetInteger("LightState", 2);
    }

    // Gameplay > Main Menu
    void BackFromGameplay()
    {
        anim.SetInteger("LightState", 0);
    }

    void DisableAnimation()
    {
        anim.enabled = false;

        SetIntensity(GlobalScript.roomLightIntensity);
    }

    public void SetIntensity(int val)
    {
        foreach (Light light in roomLights.GetComponentsInChildren<Light>())
        {
            light.intensity = val;
        }
    }
}