using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeycardUI : MonoBehaviour
{
    [SerializeField] Image fadeInScreen;
    [SerializeField] Image contrastScreen;
    public Image mainCrosshair;

    [Header("All Areas of Gameplay UI")]
    int currentlyOnScreen;
    List<GameObject> allScreens = new List<GameObject>();

    public GameObject HUD;
    [SerializeField] GameObject wholeInventory;
    [SerializeField] GameObject crafting;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOver;

    [Header("Keycard Animation")]
    Animator anim;
    string animSet = "State";

    [Header("You Win")]
    [SerializeField] GameObject youWinCam;

    [Header("Fonts")]
    [SerializeField] List<TMP_FontAsset> fontStylesHeading = new List<TMP_FontAsset>();
    [SerializeField] List<TMP_FontAsset> fontStylesNormal = new List<TMP_FontAsset>();
    List<TextMeshProUGUI> textListHeading = new List<TextMeshProUGUI>();
    List<TextMeshProUGUI> textListNormal = new List<TextMeshProUGUI>();

    GameplayOptions go;

    void Awake()
    {
        anim = GetComponent<Animator>();
        go = FindObjectOfType<GameplayOptions>(true);
    }

    void Start()
    {
        allScreens.Add(HUD);
        allScreens.Add(wholeInventory);
        allScreens.Add(pauseMenu);
        allScreens.Add(gameOver);
        TurnOffAllSections();
        HUD.SetActive(true);

        GetAllText();
        SetControlList();

        GameplayEventSystem.ges.OpenArea(GlobalScript.GameplayStatus.GameplayHUD);

        Color col = new Color(go.uiContrast.color.r, go.uiContrast.color.g, go.uiContrast.color.b, GlobalScript.uiContrast);
        go.uiContrast.color = col;
    }

    void OnEnable()
    {
        GameplayEventSystem.ges.onOpenArea += State;
        GameplayEventSystem.ges.onPlayerDead += GameOver;
        GameplayEventSystem.ges.onPlayerWin += YouWin;
    }

    void OnDisable()
    {
        GameplayEventSystem.ges.onOpenArea -= State;
        GameplayEventSystem.ges.onPlayerDead -= GameOver;
        GameplayEventSystem.ges.onPlayerWin -= YouWin;
    }

    void Update()
    {
        if (GlobalScript.gps != GlobalScript.GameplayStatus.GameOver || GlobalScript.gps != GlobalScript.GameplayStatus.YouWin)
        {
            ControlInput();
            fadeInScreen.ScreenFade(0, 0.8f, true, null);
        }
    }

    void GetAllText()
    {
        if (GlobalScript.fontStylesHeading.Count == 0) GlobalScript.fontStylesHeading = fontStylesHeading;
        if (GlobalScript.fontStylesNormal.Count == 0) GlobalScript.fontStylesNormal = fontStylesNormal;

        if (GlobalScript.allNormalText.Count == 0)
        {
            var allTexts = Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI));

            foreach (TextMeshProUGUI txt in allTexts)
            {
                if (txt.gameObject.layer == 16) textListHeading.Add(txt);
                else if (txt.gameObject.layer == 17) textListNormal.Add(txt);
            }
        }

        GlobalScript.allHeadingText = textListHeading;
        GlobalScript.allNormalText = textListNormal;

        foreach (TextMeshProUGUI e in GlobalScript.allHeadingText)
            e.font = GlobalScript.fontStylesHeading[0];

        foreach (TextMeshProUGUI e in GlobalScript.allNormalText)
            e.font = GlobalScript.fontStylesNormal[0];
    }

    void SetControlList()
    {
        GlobalScript.allActionKeys.Add(GlobalScript.jumpControl);
        GlobalScript.allActionKeys.Add(GlobalScript.rotateKIX);
        GlobalScript.allActionKeys.Add(GlobalScript.rotateKIY);
        GlobalScript.allActionKeys.Add(GlobalScript.rotateKIZ);

        GlobalScript.allUIKeys.Add(GlobalScript.hudSwitchWeaponLeft);
        GlobalScript.allUIKeys.Add(GlobalScript.hudSwitchWeaponRight);
        GlobalScript.allUIKeys.Add(GlobalScript.keycardVisibility);
        GlobalScript.allUIKeys.Add(GlobalScript.inventory);
        GlobalScript.allUIKeys.Add(GlobalScript.pausing);
    }

    void ControlInput()
    {
        // 'Pause (Esc)'
        if (Input.GetKeyDown(GlobalScript.pausing))
        {
            switch(GlobalScript.gps)
            {
                case GlobalScript.GameplayStatus.Paused: GameplayEventSystem.ges.OpenArea(GlobalScript.GameplayStatus.GameplayHUD);
                    break;

                case GlobalScript.GameplayStatus.GameplayHUD: GameplayEventSystem.ges.OpenArea(GlobalScript.GameplayStatus.Paused);
                    break;
            }
        }

        // 'Inventory (Tab)'
        if (Input.GetKeyDown(GlobalScript.inventory))
        {
            switch(GlobalScript.gps)
            {
                case GlobalScript.GameplayStatus.Inventory: GameplayEventSystem.ges.OpenArea(GlobalScript.GameplayStatus.GameplayHUD);
                    break;

                case GlobalScript.GameplayStatus.GameplayHUD: GameplayEventSystem.ges.OpenArea(GlobalScript.GameplayStatus.Inventory);
                    break;
            }
        }

        // 'Keycard Appearance (E)'
        if (Input.GetKeyDown(GlobalScript.keycardVisibility))
        {
            if (anim.GetInteger(animSet) == 0) anim.SetInteger(animSet, 2);
            else if (anim.GetInteger(animSet) == 2) anim.SetInteger(animSet, 0);
        }
    }

    void TurnOffAllSections()
    {
        foreach (GameObject screen in allScreens)
        {
            if (screen.activeSelf) screen.SetActive(false);
        }
    }

    public void OpenSection(int num)
    {
        if (currentlyOnScreen == num) return;

        currentlyOnScreen = num;
        TurnOffAllSections();

        switch (currentlyOnScreen)
        {
            case 0: HUD.SetActive(true);
                break;

            case 1: wholeInventory.SetActive(true);
                break;

            case 2: pauseMenu.SetActive(true);
                break;

            case 3: gameOver.SetActive(true);
                break;
        }
    }

    // Switches the state of the gameplay, with rules applied
    void State(GlobalScript.GameplayStatus gpState)
    {
        switch (gpState)
        {
            case GlobalScript.GameplayStatus.GameplayHUD:
                stateAction(HUD, gpState, 1, CursorLockMode.Locked, false, 0, false);
                if (CursorToggle.cursorOn)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                }
                break;

            case GlobalScript.GameplayStatus.Inventory:
                stateAction(wholeInventory, gpState, 0, CursorLockMode.None, true, 1, true);
                break;

            case GlobalScript.GameplayStatus.Paused:
                stateAction(pauseMenu, gpState, 0, CursorLockMode.None, true, 1, true);
                break;
        }

        void stateAction(GameObject setActive, GlobalScript.GameplayStatus state, int timeScale, CursorLockMode cursorLM, bool cursorVisible, int animState, bool contrastOn)
        {
            GlobalScript.gps = state;

            if (contrastOn) contrastScreen.ScreenFade(0.5f, 0.6f, false, null);
            else if (!contrastOn) contrastScreen.ScreenFade(0, 0.6f, false, null);
            TurnOffAllSections();
            setActive?.SetActive(true);
            if (Time.timeScale != timeScale) Time.timeScale = timeScale;
            if (Cursor.lockState != cursorLM) Cursor.lockState = cursorLM;
            crafting.GetComponent<GraphicRaycaster>().enabled = !cursorVisible;
            anim.SetInteger(animSet, animState);
            mainCrosshair.gameObject.SetActive(!contrastOn);
        }
    }

    void GameOver()
    {
        TurnOffAllSections();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        anim.SetBool("Dead", true);
        mainCrosshair.gameObject.SetActive(false);
        GlobalScript.gps = GlobalScript.GameplayStatus.GameOver;
    }

    void YouWin()
    {
        TurnOffAllSections();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        anim.SetBool("Win", true);
        mainCrosshair.gameObject.SetActive(false);
        youWinCam.gameObject.SetActive(true);
        GlobalScript.gps = GlobalScript.GameplayStatus.YouWin;
    }

    void TurnOffAnimator()
    {
        anim.enabled = false;
    }

    public void BackToGameplay()
    {State(GlobalScript.GameplayStatus.GameplayHUD);}
}
   