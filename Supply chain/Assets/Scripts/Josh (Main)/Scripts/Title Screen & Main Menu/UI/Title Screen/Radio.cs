using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    Animator anim;

    [SerializeField] Renderer RadioScreen;
    [SerializeField] Light lgtRadioScreen;

    float flashTime = 0;
    float flashMaxTime = 1.5f;
    int flashType;
    bool stopFlash;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        MainMenuEventSystem.GESMainMenu.onBackFromStage += BackFromStage;
        MainMenuEventSystem.GESMainMenu.onTitleToMain += MainMenuTransition;
    }

    void OnDisable()
    {
        MainMenuEventSystem.GESMainMenu.onBackFromStage -= BackFromStage;
        MainMenuEventSystem.GESMainMenu.onTitleToMain -= MainMenuTransition;
    }

    void Update()
    {
        if (!stopFlash && Input.anyKey && GlobalScript.state == GlobalScript.GameState.inTitleScreen)
        {
            MainMenuEventSystem.GESMainMenu.ToMainMenu();
            lgtRadioScreen.intensity = 0.8f;
            stopFlash = true;
        }

        else if (!stopFlash) {
            flashTime += Time.deltaTime;

            if (flashTime > flashMaxTime)
            {
                if (RadioScreen.material.GetInt("_ScreenSwitch") == 0) flashType = 1;
                else flashType = 0;

                RadioScreen.material.SetInt("_ScreenSwitch", flashType);

                if (flashType == 0) lgtRadioScreen.intensity = 0.2f;
                else if (flashType == 1) lgtRadioScreen.intensity = 0.8f;
                    
                flashTime = 0;
            }
        }
    }

    void MainMenuTransition()
    {
        anim.SetBool("GoToMainMenu", true);
    }

    public void fe()
    {
        StartCoroutine(PlaySound());
        StopCoroutine(PlaySound());
    }

    public void SwitchScreen()
    {
        RadioScreen.material.SetInt("_KeyPressed", 1);
    }

    void BackFromStage()
    {
        Time.timeScale = 1f;
        RadioScreen.material.SetInt("_KeyPressed", 1);
        MainMenuEventSystem.GESMainMenu.ToMainMenu();
        lgtRadioScreen.intensity = 0.8f;
        stopFlash = true;
        MainMenuEventSystem.GESMainMenu.Base();
    }

    IEnumerator PlaySound()
    {
        GlobalAudio.gAud.soundEffects.PlayOneShot(GlobalAudio.gAud.radioPressed);
        yield return new WaitUntil(() => GlobalAudio.gAud.soundEffects.isPlaying);
        GlobalAudio.gAud.BGM.PlayOneShot(GlobalAudio.gAud.bgmMainMenu);
    }
}