using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayEventSystem : MonoBehaviour
{
    static public GameplayEventSystem ges;

    public event Action onEnemyDestroyed;
    public event Action onEnteringStage;
    public event Action onExitingStage;
    public event Action onGotCraftingMaterial;
    public event Action onInAreaWithCursor;
    public event Action onIntoCraftingArea;
    public event Action onGotWeapon;
    public event Action onPlayBGM;
    public event Action onPlaySoundEffect;
    public event Action onPlayerDead;
    public event Action onPlayerWin;

    //public event Action<int> onInventoryWeaponSelected;
    public event Action<string, string, int, int, int> onAlertNewObjective;
    public event Action onAlertInfo;
    public event Action<WeaponData> onChangeWeapon;
    public event Action<int> onDropWeapon;
    public event Action<GlobalScript.GameplayStatus> onOpenArea;
    public event Action<bool> onPickupItem;
    public event Action<Transform> onPlayerAttacked;
    public event Action<string> onUpdateNewInfo;
    public event Action<string> onUpdateObjective;
    public event Action<WeaponData> onSwitchHUDWeapons;
    public event Action<int> onNextQuest;
    void Awake()
    {
        GlobalScript.gps = GlobalScript.GameplayStatus.GameplayHUD;
        Time.timeScale = 1;
        ges = this;
    }

    /* ACTIONS -------------------------------------------------------- */

    // ---> Entering the Stage
    public void EnteringStage()
    {if (onEnteringStage != null) onEnteringStage();}

    // ---> Enemy is Destroyed
    public void EnemyDestroyed()
    { if (onEnemyDestroyed != null) onEnemyDestroyed();}

    // ---> Exiting the Stage
    public void ExitingStage()
    {
        if (onExitingStage != null) onExitingStage();
        Application.Quit();
    }
    public void NextQuest(int x)
    {
        if(onNextQuest != null) onNextQuest(x);
    }

    public void PlayerAttacked(Transform x)
    { if (onPlayerAttacked != null) onPlayerAttacked(x); }

    // ---> Player Dies
    public void PlayerDead()
    { if (onPlayerDead != null) onPlayerDead();}

    // ---> Player Wins
    public void PlayerWins()
    { if (onPlayerWin != null) onPlayerWin(); }

    // ---> Getting A Crafting Material
    public void GotCraftingMaterial()
    {if (onGotCraftingMaterial != null) onGotCraftingMaterial();}

    // ---> Into the Crafting Area
    public void InAreaWithCursor()
    { if (onInAreaWithCursor != null) onInAreaWithCursor(); }

    // ---> Into the Crafting Area
    public void InfoCraftingArea()
    { if (onIntoCraftingArea != null) onIntoCraftingArea(); }

    // ---> Getting a Weapon
    public void GotWeapon()
    {if (onGotWeapon != null) onGotWeapon();}

    /* ACTIONS (W/ ARGUMENTS)------------------------------------------------- */

    public void AlertNewObjective(string Tttle, string desc, int required, int current, int exp)
    {
        if (onAlertNewObjective != null) onAlertNewObjective(Tttle, desc, required, current, exp);
    }

    public void AlertInfo()
    {

    }

    public void ChangeWeapon(WeaponData x)
    { if (onChangeWeapon != null) onChangeWeapon(x); }

    // ---> Dropping a Weapon
    public void DropWeapon(int num)
    { if (onGotWeapon != null) onDropWeapon(num); }

    // ---> Opens an area
    public void OpenArea(GlobalScript.GameplayStatus area)
    {if (onOpenArea != null) onOpenArea(area);}

    // ---> Picking up a Key Item
    public void PickupItem(bool activate)
    { if (onPickupItem != null) onPickupItem(activate); }

    // ---> Switch HUD Weapons
    public void SwitchHUDWeapons(WeaponData wd)
    { if (onSwitchHUDWeapons != null) onSwitchHUDWeapons(wd); }

    public void UpdateNewInfo(string info)
    { if (onUpdateNewInfo != null) onUpdateNewInfo(info); }

    public void UpdateObjective(string objective)
    { if (onUpdateObjective != null) onUpdateObjective(objective); }

    /* --- BUTTON EVENTS --------------------------------------------*/

    public void ActivateArea(GameObject area)
    {
        if (!area.activeSelf) area.SetActive(true);
        else area.SetActive(false);
    }
}