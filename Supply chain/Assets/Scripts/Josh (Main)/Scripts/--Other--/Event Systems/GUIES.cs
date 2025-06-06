using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// GUIES = Gameplay UI Event System
public class GUIES : MonoBehaviour
{
    [SerializeField] HUD hud;

    static public GameObject currentSelected;
    static EventSystem es;

    [Header("Selected Objects of Each Area (excluding HUD & Inventory)")]
    [SerializeField] GameObject selectedHUD;
    [SerializeField] GameObject selectedInventory;
    [SerializeField] GameObject selectedCrafting;
    [SerializeField] GameObject selectedPauseMenu;
    [SerializeField] GameObject selectedGameOver;
    [SerializeField] GameObject selectedYouWin;

    void Awake()
    {
        es = EventSystem.current;
        NewSelectedGameObject(GlobalScript.gps);
    }

    void OnEnable()
    {
        GameplayEventSystem.ges.onOpenArea += NewSelectedGameObject;
    }

    void OnDisable()
    {
        GameplayEventSystem.ges.onOpenArea -= NewSelectedGameObject;
    }

    void Update()
    {
        if (currentSelected == null && es.currentSelectedGameObject != null)
            currentSelected = es.currentSelectedGameObject;

        StateConditions();

        //if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
          //  es.SetSelectedGameObject(currentSelected);
    }

    static public void SetCurrentSeleted(GameObject toSetSelected)
    {es.SetSelectedGameObject(currentSelected = toSetSelected);}

    // Selects a new selected object based on where the player is within the gameplay
    void NewSelectedGameObject(GlobalScript.GameplayStatus gs)
    {
        switch(gs)
        {
            case GlobalScript.GameplayStatus.GameplayHUD:
                es.SetSelectedGameObject(currentSelected = selectedHUD);
                break;

            case GlobalScript.GameplayStatus.Inventory:
                es.SetSelectedGameObject(currentSelected = selectedInventory);
                break;
        }
    }

    void StateConditions()
    {
        if (GlobalScript.gps == GlobalScript.GameplayStatus.GameplayHUD)
        {
            if (Input.GetKeyDown(GlobalScript.hudSwitchWeaponLeft)) SwitchCurrent(-1);
            else if (Input.GetKeyDown(GlobalScript.hudSwitchWeaponRight)) SwitchCurrent(1);
        }
    }

    void SwitchCurrent(int direction)
    { hud.NewCurrentWeapon(direction); }
}