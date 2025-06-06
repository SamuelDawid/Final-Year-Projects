using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEditor.VFX;

public class GameplayOptions : Options
{
    // Controls
    int keyRef;
    bool wantToSwitch = false;
    KeyCode pressed;

    [Header("Audio Control")]
    [SerializeField] TextMeshProUGUI controlError;    
    [SerializeField] TextMeshProUGUI controlErrorTwo;    
    [SerializeField] TextMeshProUGUI[] keyDisplayText = new TextMeshProUGUI[10];
    [SerializeField] TextMeshProUGUI[] keyInputText = new TextMeshProUGUI[10];

    [Header("UI Contrast")]
    public Image uiContrast;
    [SerializeField] Slider sliderContrast;
    [SerializeField] TextMeshProUGUI contrastText;
    [SerializeField] Material keyItemHighlighter;

    Color col;

    void Start()
    {
        SetParameters();
        SetAudio();
        sliderContrast.value = GlobalScript.uiContrast;
    }

    void Update()
    {
        AudioVolume();
        if (wantToSwitch) SwitchDetection();
    }

    // Switches Controls
    public void SwitchControls(int Control)
    {
        if (!wantToSwitch)
        {
            keyInputText[Control].text = "...";
            keyRef = Control;
            wantToSwitch = true;
            return;
        }
    }

    void SwitchDetection()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(key))
                {
                    pressed = key;
                    break;
                }
            }

            // Checks if key/button has already been assigned

            for(int a = 0; a < GlobalScript.allActionKeys.Count; a++)
            {
                if (GlobalScript.allActionKeys[a] == pressed)
                {
                    controlErrorTwo.text = controlError.text = "Input has already been assigned to '" + keyDisplayText[a].text;
                    controlErrorTwo.color = controlError.color = GlobalScript.HexColour("#FFACAC");
                    return;
                }
            }

            for (int b = 0; b < GlobalScript.allUIKeys.Count; b++)
            {
                if (GlobalScript.allUIKeys[b] == pressed)
                {
                    controlErrorTwo.text = controlError.text = "Input has already been assigned to '" + keyDisplayText[b].text;
                    controlErrorTwo.color = controlError.color = GlobalScript.HexColour("#FFACAC");
                    return;
                }
            }

            // Assigns the Key

            if (keyRef >= 0 && keyRef < 4)
            {
                GlobalScript.allActionKeys[keyRef] = pressed;
                keyInputText[keyRef].text = KeyButtonOutput(pressed.ToString());
            }
            else if (keyRef >= 4)
            {
                GlobalScript.allUIKeys[keyRef - 4] = pressed;
                keyInputText[keyRef].text = KeyButtonOutput(pressed.ToString());
            }

            AssignKey(keyRef);

            controlErrorTwo.text = controlError.text = "PRESS THE BUTTON NEXT TO EACH ACTION TO CHANGE IT'S CONTROL TO WHATEVER INPUT YOU WANT. YOU CAN'T HAVE 2 ACTIONS PER KEY";
            controlErrorTwo.color = controlError.color = GlobalScript.HexColour("#FFFFFF");
            wantToSwitch = false;
            keyRef = -1;
        }
    }

    void AssignKey(int key)
    {
        switch (key)
        {
            case 0: GlobalScript.jumpControl = GlobalScript.allActionKeys[0]; break;
            case 1: GlobalScript.rotateKIX = GlobalScript.allActionKeys[1]; break;
            case 2: GlobalScript.rotateKIY = GlobalScript.allActionKeys[2]; break;
            case 3: GlobalScript.rotateKIZ = GlobalScript.allActionKeys[3]; break;
            case 4: GlobalScript.pausing = GlobalScript.allUIKeys[0]; break;
            case 5: GlobalScript.hudSwitchWeaponLeft = GlobalScript.allUIKeys[1]; break;
            case 6: GlobalScript.hudSwitchWeaponRight = GlobalScript.allUIKeys[2]; break;
            case 7: GlobalScript.inventory = GlobalScript.allUIKeys[3]; break;
            case 8: GlobalScript.keycardVisibility = GlobalScript.allUIKeys[4]; break;
        }
    }

    string KeyButtonOutput(string key)
    {
        if (key.Contains("Alpha")) 
            key = key.Substring(5);

        return key.ToString();
    }

    public void SetUIContrast(Slider slider)
    {
        col = uiContrast.color;
        col.a = slider.value;
        //contrastText.text = "(" + (slider.value ).ToString("00") + "%) UI Contrast"; 
        GlobalScript.uiContrast = slider.value;
        uiContrast.color = col;
    }

    public void SetHighligherColour(string Hex)
    {
        GlobalScript.keyItemHighlighter = Hex;
    }
}
