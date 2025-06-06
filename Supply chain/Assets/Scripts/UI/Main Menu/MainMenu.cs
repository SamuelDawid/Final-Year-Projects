using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // Area: --Main Menu--
    public enum MainMenuStatus
    {
        Base,
        inOptions,
        inCredits,
        inStageSelection,
        inNewspaper,
        inExit,
        ComingBackFromGameplay
    }
    static public MainMenuStatus mms;

    [SerializeField] TextMeshProUGUI screenTitle, screenDesc;

    Camera subCameraRef;

    Animator anim;
    [SerializeField] GameObject Normal;
    [SerializeField] GameObject StageSelector;

    void Awake()
    {
        Time.timeScale = 1;
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        MainMenuEventSystem.GESMainMenu.onBase += InMainMenu;
        MainMenuEventSystem.GESMainMenu.onNewspaper += Newspaper;
        MainMenuEventSystem.GESMainMenu.onPinboard += Options;
        MainMenuEventSystem.GESMainMenu.onAlbums += Credits;
        MainMenuEventSystem.GESMainMenu.onDoor += StageSelection;
        MainMenuEventSystem.GESMainMenu.onBed += Exit;
        MainMenuEventSystem.GESMainMenu.onOverButtonObject += SetTextOnHover;
    }

    void OnDisable()
    {
        MainMenuEventSystem.GESMainMenu.onBase -= InMainMenu;
        MainMenuEventSystem.GESMainMenu.onNewspaper -= Newspaper;
        MainMenuEventSystem.GESMainMenu.onPinboard -= Options;
        MainMenuEventSystem.GESMainMenu.onAlbums -= Credits;
        MainMenuEventSystem.GESMainMenu.onDoor -= StageSelection;
        MainMenuEventSystem.GESMainMenu.onBed -= Exit;
        MainMenuEventSystem.GESMainMenu.onOverButtonObject -= SetTextOnHover;
    }

    void SetTextOnHover(string title, string desc)
    {
        screenTitle.text = title;
        screenDesc.text = desc;
    }

    public void InMainMenu()
    {
        if (GlobalScript.state != GlobalScript.GameState.inMainMenu) GlobalScript.state = GlobalScript.GameState.inMainMenu;
        mms = MainMenuStatus.Base;
        BasePosition();
    }

    void BasePosition()
    {
        if (subCameraRef)
        {
            subCameraRef.enabled = false;
            subCameraRef = null;
        }
        mms = MainMenuStatus.Base;
        anim.SetInteger("State", 0);

        if (StageSelector.activeSelf)
        {
            StageSelector.SetActive(false);
            Normal.SetActive(true);
        }
    }

    void Newspaper(Camera cam)
    {
        subCameraRef = cam;
        mms = MainMenuStatus.inNewspaper;
    }

    void Options(Camera cam)
    {
        subCameraRef = cam;
        mms = MainMenuStatus.inOptions;
    }

    void Credits(Camera cam)
    {
        subCameraRef = cam;
        mms = MainMenuStatus.inCredits;
    }

    void StageSelection()
    {
        mms = MainMenuStatus.inStageSelection;
        anim.SetInteger("State", 1);
        Normal.SetActive(false);
        StageSelector.SetActive(true);
    }

    void Exit()
    {
        mms = MainMenuStatus.inExit;
    }
}