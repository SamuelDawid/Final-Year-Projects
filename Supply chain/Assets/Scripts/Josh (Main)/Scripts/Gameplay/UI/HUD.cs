using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
    bool playerInvincibility;
    float maxEXP = 100;
    float levelMultiplier = 1;

    // Animation Clips
    [Header("Objectives")]
    [SerializeField] AnimationClip infoUpdated;
    [SerializeField] AnimationClip levelUp;
    [SerializeField] AnimationClip objectiveUpdated;
    [SerializeField] AnimationClip playerAttacked;
    Animation anim;
    [Space]
    [Space]

    // Health & Level
    [Header("Left-Hand Side")]
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] Image imgLevel;
    [Space]
    [Space]

    // Time & Whole Inventory
    [Header("Middle")]
    [SerializeField] TextMeshProUGUI timer;
    float minutes = 30;
    float seconds = 00;
    [SerializeField] TextMeshProUGUI txtHealth;
    [SerializeField] Image imgHealthTop;
    [SerializeField] Image imgHealthMain;

    // Weapons
    [Header("Inventory Slots")]
    int previousSelectedWeaponSlot = 0;
    [HideInInspector] public int selectedWeaponSlot = 0;
    [SerializeField] Image[] weaponSlotsHUD = new Image[3];
    public Inventory inventory;

    // HUD Inventory
    [Header("Right-Hand Side")]
    [SerializeField] TextMeshProUGUI currentItem;
    [SerializeField] GameObject sectionWeapons;
    [SerializeField] GameObject sectionNoWeapons;
    [Space]
    [Space]

    // HP Colours (High, Mid, Low)
    [Header("HP Colour Bars")]
    [SerializeField] Color highHPTop;
    [SerializeField] Color highHPMain;
    [SerializeField] Color midHPTop;
    [SerializeField] Color midHPMain;
    [SerializeField] Color lowHPTop;
    [SerializeField] Color lowHPMain;

    // Levelling Up
    [SerializeField] TextMeshProUGUI levelUpText;

    // Alert Info (& Tutorial)
    [SerializeField] TextMeshProUGUI newInfoText;

    List<Action> newInfos = new List<Action>();

    void Awake()
    {
        // References the player's stats from 'GlobalScript', and applies it to the respected areas
        GetPreviousInfo();

        GlobalScript.state = GlobalScript.GameState.inGame;
        anim = transform.parent.parent.GetComponent<Animation>();

        // Setting HP
        imgHealthTop.fillAmount = (GlobalScript.playerHPAmount / GlobalScript.playerMaxHPAmount) * 1.1f;
        imgHealthMain.fillAmount = GlobalScript.playerHPAmount / GlobalScript.playerMaxHPAmount;
        HPBarColours();

        // Setting the Level
        txtLevel.text = GlobalScript.playerLevel.ToString();
    }

    void Start()
    {
        // Player HUD
        inventory.ItemModification(200, 0);

        inventory.SetStatsAndStuffInfo(selectedWeaponSlot);
        SetCurrentWeapon(GlobalScript.weapons[selectedWeaponSlot]);

        newInfos.Add( ()=> NewInfoAlert("'WASD' or Arrow Keys to move; Space to Jump"));
        newInfos.Add( ()=> NewInfoAlert("'Tab' to open inventory: View current objective, as well as weapons, crafting materials, and your stats"));
        newInfos.Add( ()=> NewInfoAlert("Go into options in the pause menu to view and change your controls"));

        StartCoroutine(AnimationWaiting(anim));
    }

    void OnEnable()
    {
        GameplayEventSystem.ges.onChangeWeapon += SetCurrentWeapon;
        GameplayEventSystem.ges.onPickupItem += HUDRightStatus;
        GameplayEventSystem.ges.onAlertNewObjective += NewObjectiveAlert;
        GameplayEventSystem.ges.onUpdateNewInfo += NewInfoAlert;
        GameplayEventSystem.ges.onPlayerAttacked += PlayerAttacked;
    }

    void OnDisable()
    {
        GameplayEventSystem.ges.onChangeWeapon -= SetCurrentWeapon;
        GameplayEventSystem.ges.onPickupItem -= HUDRightStatus;
        GameplayEventSystem.ges.onAlertNewObjective -= NewObjectiveAlert;
        GameplayEventSystem.ges.onUpdateNewInfo -= NewInfoAlert;
        GameplayEventSystem.ges.onPlayerAttacked -= PlayerAttacked;

        inventory.SetStatsAndStuffInfo(selectedWeaponSlot);
    }

    void GetPreviousInfo()
    {
        if (!GlobalScript.withinStage)
        {
            GlobalScript.playerLevelRef = GlobalScript.playerLevel;
            GlobalScript.playerEXPRef = GlobalScript.playerEXP;
            GlobalScript.playerAttackRef = GlobalScript.playerAttack;
            GlobalScript.playerDefenseRef = GlobalScript.playerDefense;

            Array.Copy(GlobalScript.weapons, GlobalScript.weaponsRef, GlobalScript.weapons.Length);
            Array.Copy(GlobalScript.craftingMaterials, GlobalScript.craftingMateriaslRef, GlobalScript.craftingMaterials.Length);
            Array.Copy(GlobalScript.craftingMaterialAmount, GlobalScript.craftingMaterialAmountRef, GlobalScript.craftingMaterialAmount.Length);
        }
    }

    void Update()
    {
        Timer();
        FillImage();
        //RemoveWeapon();
    }

    void HUDRightStatus(bool areaActivation)
    {
        if (sectionNoWeapons.activeSelf != areaActivation) sectionNoWeapons.SetActive(areaActivation);
        if (sectionNoWeapons.activeSelf != !areaActivation) sectionWeapons.SetActive(!areaActivation);
    }

    // Modification To HP
    public void ModifyHP(float Modifier)
    {
        if (playerInvincibility) return;
        GlobalScript.playerHPAmount += Modifier;

        HPBarColours();

        if (GlobalScript.playerHPAmount >= 100) GlobalScript.playerHPAmount = 100;

        else if (GlobalScript.playerHPAmount <= 0)
        {
            GlobalScript.playerHPAmount = 0;
            GameplayEventSystem.ges.PlayerDead();
        }
    }

    // Levelling Systems
    public void ModifyLevel(float expAmount)
    {
        GlobalScript.playerEXP += expAmount * levelMultiplier;

        if (GlobalScript.playerEXP >= maxEXP)
        {
            float remaining = GlobalScript.playerEXP - maxEXP;
            GlobalScript.playerEXP = remaining;
            GlobalScript.playerLevel++;
            txtLevel.text = GlobalScript.playerLevel < 10 ? "0" + GlobalScript.playerLevel.ToString() : GlobalScript.playerLevel.ToString();
            levelMultiplier *= 0.95f;

            if (GlobalScript.playerAttack > GlobalScript.playerDefense)
            {
                GlobalScript.playerDefense++;
                levelUpText.text = "Level Up! Defense has been Increased!";
            }
            else
            {
                GlobalScript.playerAttack++;
                levelUpText.text = "Level Up! Attack has been Increased!";
            }

            GlobalScript.PlayAnimation(anim, levelUp);
        }
    }

    // Image Filling (HP, Level)
    void FillImage()
    {
        if (imgHealthMain.fillAmount != (GlobalScript.playerHPAmount / GlobalScript.playerMaxHPAmount))
        {
            imgHealthTop.fillAmount = Mathf.Lerp(imgHealthTop.fillAmount, ((GlobalScript.playerHPAmount / GlobalScript.playerMaxHPAmount) * 1.1f), 4f * Time.deltaTime);
            imgHealthMain.fillAmount = Mathf.Lerp(imgHealthMain.fillAmount, (GlobalScript.playerHPAmount / GlobalScript.playerMaxHPAmount), 4f * Time.deltaTime);
        }

        if (imgLevel.fillAmount != (GlobalScript.playerEXP / 100))
            imgLevel.fillAmount = Mathf.Lerp(imgLevel.fillAmount, (GlobalScript.playerEXP / 100), 4f * Time.deltaTime);
    }

    // Colour of HP Bars at a specific fill amount
    void HPBarColours()
    {
        float x = ((GlobalScript.playerHPAmount / GlobalScript.playerMaxHPAmount) * 100);

        if (x > 100) x = 100;

        txtHealth.text = x.ToString("00") + "% LEFT";

        if (GlobalScript.playerHPAmount > 59 && GlobalScript.playerHPAmount < 101)
        {
            imgHealthTop.color = highHPTop;
            imgHealthMain.color = highHPMain;
        }

        else if (GlobalScript.playerHPAmount > 29 && GlobalScript.playerHPAmount < 60)
        {
            imgHealthTop.color = midHPTop;
            imgHealthMain.color = midHPMain;
        }

        else if (GlobalScript.playerHPAmount > 0 && GlobalScript.playerHPAmount < 30)
        {
            imgHealthTop.color = lowHPTop;
            imgHealthMain.color = lowHPMain;
        }

        else if (GlobalScript.playerHPAmount <= 0)
            transform.parent.parent.GetComponent<KeycardUI>().OpenSection(3);
    }

    // Switches weapons
    public void NewCurrentWeapon(int direction)
    {
        previousSelectedWeaponSlot = selectedWeaponSlot;
        selectedWeaponSlot += direction;

        if (selectedWeaponSlot < 0) selectedWeaponSlot = GlobalScript.weapons.Length - 1;
        else if (selectedWeaponSlot >= GlobalScript.weapons.Length) selectedWeaponSlot = 0;

        if (GlobalScript.weapons[selectedWeaponSlot] == null)
        {
            selectedWeaponSlot = previousSelectedWeaponSlot;
            return;
        }

        //inventory.currentWeaponID = selectedWeaponSlot;

        SetCurrentWeapon(GlobalScript.weapons[selectedWeaponSlot]);
    }

    // Refreshes the Gameplay HUD 
    public void SetCurrentWeapon(WeaponData current)
    {
        weaponSlotsHUD[0].sprite = selectedWeaponSlot == 0 ? GlobalScript.weapons[7]?.GivenSprite : GlobalScript.weapons[selectedWeaponSlot - 1]?.GivenSprite;
        weaponSlotsHUD[1].sprite = current?.GivenSprite;
        weaponSlotsHUD[2].sprite = selectedWeaponSlot == GlobalScript.weapons.Length - 1 ? GlobalScript.weapons[0]?.GivenSprite : GlobalScript.weapons[selectedWeaponSlot + 1]?.GivenSprite;

        currentItem.text = GlobalScript.weapons[selectedWeaponSlot].GivenName.ToUpper();
        GameplayEventSystem.ges.SwitchHUDWeapons(GlobalScript.weapons[selectedWeaponSlot]);
    }

    // Countdown Timer
    void Timer()
    {
        seconds -= Time.deltaTime;

        TimeSystem(seconds <= -1, -1, 59);
        TimeSystem(seconds >= 60, 1, 00);

        if (minutes <= 0 && seconds <= 0) seconds = minutes = 0;

        timer.GetComponent<TextMeshProUGUI>().text = minutes.ToString("00") + ":" + (Mathf.CeilToInt(seconds)).ToString("00");

        void TimeSystem(bool Decider, int newMin, int newSec)
        {
            if (Decider)
            {
                minutes += newMin;
                seconds = newSec;
            }
        }

        if (minutes <= 0 && seconds <= 0)
        {
            GameplayEventSystem.ges.PlayerDead();
        }
    }

    // Switches to inventory weapon
    // void RemoveWeapon()
    // {
    //     if (Input.GetMouseButtonDown(1))
    //     {
    //         Sprite spt = weaponSlotsHUD[1].sprite;
    //         int num = 0;

    //         for (int a = 0; a < inventory.weaponSlotImages.Length; a++)
    //         {
    //             if (inventory.weaponSlotImages[a].sprite == spt)
    //             {
    //                 num = a;
    //                 break;
    //             }
    //         }

    //         if (num == 0 && GlobalScript.weapons[num + 1] == null) return;

    //         // Instantiate in front of player
    //         GameObject weapon = Instantiate(GlobalScript.weapons[num].GivenWeapon, GlobalScript.playerRef.transform.forward * 4 + GlobalScript.playerRef.transform.forward * 5, Quaternion.identity);
    //         weapon.AddComponent<BoxCollider>();
    //         weapon.AddComponent<Rigidbody>();
    //         weapon.AddComponent<Weapon>();

    //         // Remove it from the weapons list
    //         GlobalScript.weapons[num] = null;
    //         weaponSlotsHUD[num].sprite = null;
    //         weaponSlotsHUD[num].enabled = false;

    //         // Remove it from the weapons list
    //         ShiftDown(1);

    //         // Update the invnetory
    //         NewCurrentWeapon(0);
    //         GameplayEventSystem.ges.DropWeapon(num);
    //     }
    // }

    void ShiftDown(int area)
    {
        bool shift = false;
        if (area == 1)
        {
            for (int b = 0; b < GlobalScript.weapons.Length; b++)
            {
                if (GlobalScript.weapons[b] == null || shift == true)
                {
                    if (GlobalScript.weapons[b + 1] != null)
                    {
                        GlobalScript.weapons[b] = GlobalScript.weapons[b + 1] ? GlobalScript.weapons[b + 1] : GlobalScript.weapons[b];
                        if (!shift) shift = true;
                    }
                    else return;
                }
            }
        }

        if (area == 2)
        {
            // Same, but with crafting materials
        }
    }

    void PlayInfo(string text)
    {
        newInfos.Add(() => NewInfoAlert(text));
        StartCoroutine(AnimationWaiting(anim));
    }

    void NewInfoAlert(string newInfo)
    {
        GlobalScript.PlayAnimation(anim, infoUpdated);
        newInfoText.text = newInfo;
    }

    void NewObjectiveAlert(string title, string desc, int req, int cur, int exp)
    {
        GlobalScript.PlayAnimation(anim, objectiveUpdated);
        inventory.SetObjective(title, desc, req, cur, exp);
    }

    // Goes through each animationo within the list
    private IEnumerator AnimationWaiting(Animation animation)
    {
        foreach (var info in newInfos)
        {
            info.Invoke();

            while (animation.isPlaying)
            {
                yield return null;
            }   
        }

        newInfos.Clear();
    }

    void PlayerAttacked(Transform tm)
    {
        if (anim.isPlaying) return;

        GlobalScript.PlayAnimation(anim, playerAttacked);
    }

    public void NewInfo(string sentence)
    {
        newInfos.Add(() => NewInfoAlert("Defeat the factory boss!"));
    }
}