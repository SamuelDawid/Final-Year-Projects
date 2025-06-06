using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] string title;
    [SerializeField] [TextArea] string description;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && MainMenu.mms != MainMenu.MainMenuStatus.Base) Base();
    }

    public void MouseOverObject()
    {
        if (MainMenu.mms == MainMenu.MainMenuStatus.Base)
        {
            MainMenuEventSystem.GESMainMenu.OverButtonObject(title, description);
        }
    }

    public void MouseNotOverObject()
    {
        if (MainMenu.mms == MainMenu.MainMenuStatus.Base)
        {
            MainMenuEventSystem.GESMainMenu.OverButtonObject("", "");
        }
    }

    /* ACTIONS ----------------------------------- */

    public void Base()
    {
        MainMenuEventSystem.GESMainMenu.Base();
    }

    // Newspaper
    public void OpenStory(Camera cam)
    {
        cam.enabled = true;
        MainMenuEventSystem.GESMainMenu.Newspaper(cam);
    }

    // Album
    public void OpenCredits(Camera cam)
    {
        cam.enabled = true;
        MainMenuEventSystem.GESMainMenu.Credits(cam);
    }

    // Album
    public void OpenOptions(Camera cam)
    {
        cam.enabled = true;
        MainMenuEventSystem.GESMainMenu.Pinboard(cam);
    }

    // Door
    public void OpenStageSelection()
    {
        if (MainMenu.mms == MainMenu.MainMenuStatus.Base)
            MainMenuEventSystem.GESMainMenu.Door();
    }

    // Bed
    public void QuitGame()
    {
        MainMenuEventSystem.GESMainMenu.Bed();
    }
}