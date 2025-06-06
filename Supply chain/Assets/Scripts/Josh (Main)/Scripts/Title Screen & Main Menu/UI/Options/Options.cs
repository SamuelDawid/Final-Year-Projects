using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    // Audio
    [Header("Audio Control")]
    [SerializeField] TextMeshProUGUI bgmValue;
    [SerializeField] TextMeshProUGUI seValue;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [Space]
    [Space]

    // Graphics
    [SerializeField] TextMeshProUGUI fsModeText;

    public void SetParameters()
    {
        // Window Mode
        WindowMode(GlobalScript.fsMode);
    }

    public void SetAudio()
    {
        // Audio
        bgmSlider.value = GlobalScript.musicVolume;
        seSlider.value = GlobalScript.soundEffectsVolume;
        GlobalScript.Volume(bgmSlider, bgmValue, ref GlobalScript.musicVolume, GlobalAudio.gAud.BGM);
        GlobalScript.Volume(seSlider, seValue, ref GlobalScript.soundEffectsVolume, GlobalAudio.gAud.soundEffects);
    }


    // --------------------------------------------------------------------------------------------------------------------------------------------------------

    // Font Styles
    public void ChangeFontHeading(TMP_Dropdown val)
    {
        foreach (TextMeshProUGUI e in GlobalScript.allHeadingText)
            e.font = GlobalScript.fontStylesHeading[val.value];
    }

    public void ChangeFontNormal(TMP_Dropdown val)
    {
        foreach (TextMeshProUGUI e in GlobalScript.allNormalText)
            e.font = GlobalScript.fontStylesNormal[val.value];
    }

    // Update Audio Volume
    public void AudioVolume()
    {
        bgmSlider.onValueChanged.AddListener(delegate { GlobalScript.Volume(bgmSlider, bgmValue, ref GlobalScript.musicVolume, GlobalAudio.gAud.BGM); });
        seSlider.onValueChanged.AddListener(delegate { GlobalScript.Volume(seSlider, seValue, ref GlobalScript.soundEffectsVolume, GlobalAudio.gAud.soundEffects); });
    }

    // Window Display
    public void WindowMode(int Change)
    {
        GlobalScript.fsMode += Change;

        GlobalScript.fsMode = GlobalScript.fsMode < 0 ? 3 : GlobalScript.fsMode > 3 ? 0 : GlobalScript.fsMode;

        Screen.fullScreenMode = (FullScreenMode)GlobalScript.fsMode;

        switch (GlobalScript.fsMode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                fsModeText.text = "Full Screen";
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                fsModeText.text = "Full Screen Window";
                break;

            case 2:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                fsModeText.text = "Maximized Window";
                break;

            case 3:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                fsModeText.text = "Window";
                break;
        }
    }
}