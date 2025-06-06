using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used as a way for objects (specifically their methods) to subscribe to so that events can be fired from one point, and so that data can be shared
// without having to make everything public
public class MainMenuEventSystem : MonoBehaviour
{
    static public MainMenuEventSystem GESMainMenu;

    public event Action onEnteringGame;
    public event Action onTitleToMain;
    public event Action onBase;

    public event Action onBed;
    public event Action onDoor;
    public event Action onPlayStage;
    public event Action onBackFromStage;

    public event Action<Camera> onNewspaper;
    public event Action<Camera> onPinboard;
    public event Action<Camera> onAlbums;
    public event Action<string, string>onOverButtonObject;
    public event Action<int> onWithinAnArea;

    void Awake()
    {
        Time.timeScale = 1;
        GESMainMenu = this;
    }

    public void EnteringGame()
    { if (onEnteringGame != null) onEnteringGame(); }

    public void ToMainMenu()
    { if (onTitleToMain != null) onTitleToMain(); }

    public void Base()
    { if (onBase != null) onBase(); }

    public void Newspaper(Camera cam)
    { if (onNewspaper != null) onNewspaper(cam); }

    public void Pinboard(Camera cam)
    { if (onPinboard != null) onPinboard(cam); }

    public void Bed()
    { if (onBed != null) onBed(); }

    public void Credits(Camera cam)
    { if (onAlbums != null) onAlbums(cam); }

    public void Door()
    { if (onDoor != null) onDoor(); }

    public void OverButtonObject(string title, string desc)
    { if (onOverButtonObject != null) onOverButtonObject(title, desc); }

    public void PlayStage()
    { if (onPlayStage != null) onPlayStage(); }

    public void BackFromStage()
    {if (onBackFromStage != null) onBackFromStage();}

    public void WithinAnArea(int animState)
    {if (onWithinAnArea != null) onWithinAnArea(animState);}
}