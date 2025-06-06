using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MMCamera : MonoBehaviour
{
    [Header("Fade In Screen")]
    [SerializeField] Image fadeScreen;

    [SerializeField] Transform orientation;

    Animator anim;

    float xRot, yRot;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        fadeScreen.ScreenFade(0, 0.7f, false, null);

        switch (GlobalScript.state)
        {
            case GlobalScript.GameState.inTitleScreen:
                MainMenuEventSystem.GESMainMenu.EnteringGame();
                break;

            case GlobalScript.GameState.inGame:
                MainMenuEventSystem.GESMainMenu.BackFromStage();
                break;
        }
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;

        MainMenuEventSystem.GESMainMenu.onTitleToMain += MainMenuTransition;
        MainMenuEventSystem.GESMainMenu.onBase += DisableAnimator;
        MainMenuEventSystem.GESMainMenu.onBed += QuitGame;
        MainMenuEventSystem.GESMainMenu.onBackFromStage += GameplayToMainMenu;
    }

    void OnDisable()
    {
        MainMenuEventSystem.GESMainMenu.onTitleToMain -= MainMenuTransition;
        MainMenuEventSystem.GESMainMenu.onBase -= DisableAnimator;
        MainMenuEventSystem.GESMainMenu.onBed -= QuitGame;
        MainMenuEventSystem.GESMainMenu.onBackFromStage -= GameplayToMainMenu;
    }

    void Update()
    {
        if (MainMenu.mms == MainMenu.MainMenuStatus.Base) CameraMovement();
    }

    #region Animation Events

    void setToMainMenu()
    {
        MainMenuEventSystem.GESMainMenu.Base();
    }

    void DisableAnimator()
    { anim.enabled = false; }

    #endregion

    #region Subscribed Events

    void MainMenuTransition()
    { anim.SetBool("(Title Screen) Proceed", true); }

    // Camera Movement
    void CameraMovement()
    {
        // Keyboard Input
        xRot -= Input.GetAxisRaw("Vertical");
        yRot += Input.GetAxisRaw("Horizontal");

        xRot = Mathf.Clamp(xRot, -90, 90);

        // Rotation
        transform.rotation = Quaternion.Euler(10, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }

    public void BackFromStageSelection()
    {
        MainMenuEventSystem.GESMainMenu.Base();
    }

    void GameplayToMainMenu()
    {
        anim.SetBool("BackFromGameplay", true);
    }

    public void GoToStage(int stage)
    {
        Debug.Log(MainMenu.mms);

        if (MainMenu.mms == MainMenu.MainMenuStatus.inStageSelection)
        {
            fadeScreen.ScreenFade(1, 5, true, PlayStage);
        }

        void PlayStage()
        {
            if (GameManager.inst != null) GameManager.inst.SwitchArea(2, stage);
            else
            {
                SceneManager.LoadScene(stage);
            }
        }
    }

    #endregion

    void QuitGame()
    {
        fadeScreen.ScreenFade(1, 1, true, Quit);

        void Quit()
        {
            Application.Quit();
        }
    }
}