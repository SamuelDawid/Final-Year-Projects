using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

abstract public class GlobalScript : MonoBehaviour
{
    // DEFINING DIFFERENT AREAS

        // Game In VR?
    public enum PlayType
    {
        NoVR,
        VR
    }
    static public PlayType playType;

        // Area: --Whole Game--
    public enum GameState
    {
        inTitleScreen,
        inMainMenu,
        inGame
    }
    static public GameState state;

        // Area: --Gameplay--
    public enum GameplayStatus
    {
        GameplayHUD,
        Inventory,
        Paused,
        Crafting,
        YouWin,
        GameOver
    }
    static public GameplayStatus gps;

    // GAMEPLAY

        // Entered Level

    static public bool withinStage = false;

        // Player

    static public PlayerMovement playerRef;
    static public float playerMaxHPAmount = 100;
    static public float playerHPAmount = 100;
    static public int playerLevel = 1;
    static public float playerEXP = 0;
    static public float playerAttack = 1;
    static public float playerDefense = 1;

    // Inventory

    static public KeyItemData currentHeldKeyItem;
    static public WeaponData[] weapons = new WeaponData[8];
    static public CraftingMaterialData[] craftingMaterials = new CraftingMaterialData[8];
    static public int[] craftingMaterialAmount = new int[8] {0, 0, 0, 0, 0, 0, 0, 0 };

    // OPTIONS

    // Room Light Intensity

    static public int roomLightIntensity = 200;

        // Sound

    static public float musicVolume = 50f;
    static public float soundEffectsVolume = 50f;

        // Fullscreen Mode

   static public int fsMode = 0;

        // Font Styles

   static public List<TextMeshProUGUI> allHeadingText = new List<TextMeshProUGUI>();
   static public List<TextMeshProUGUI> allNormalText = new List<TextMeshProUGUI>();

   static public List<TMP_FontAsset> fontStylesHeading = new List<TMP_FontAsset>();
   static public List<TMP_FontAsset> fontStylesNormal = new List<TMP_FontAsset>();

        // Controls

    static public List<KeyCode> allActionKeys = new List<KeyCode>();
    static public List<KeyCode> allUIKeys = new List<KeyCode>();

            // Actions

    static public KeyCode jumpControl = KeyCode.Space;  // Jumping
    static public KeyCode rotateKIX = KeyCode.Z;        // Rotating Key Item (X Axis)
    static public KeyCode rotateKIY = KeyCode.X;        // Rotating Key Item (Y Axis)
    static public KeyCode rotateKIZ = KeyCode.C;        // Rotating Key Item (Z Axis)

             // UI

    static public KeyCode pausing = KeyCode.Escape;                 // Pause/Unpause
    static public KeyCode hudSwitchWeaponLeft = KeyCode.Alpha1;     // Switch Current Weapon (Left)
    static public KeyCode hudSwitchWeaponRight = KeyCode.Alpha2;    // Switch Current Weapon (Right)
    static public KeyCode inventory = KeyCode.Tab;                  // Open/Close Inventory
    static public KeyCode keycardVisibility = KeyCode.E;            // Keycard Appearance

            // Contrast

    static public float uiContrast;
    static public string keyItemHighlighter;

    // Crosshair

    static public int crosshair = 0;
    static public float crosshairSize = 1;
    static public string crosshairColour = "000000";
    static public float crosshairOpacity = 1;

    // Stage Selection

    static public int stageSelected = 3;

    // Reference Variables (Upon Stage Entry)

    static public bool StageComplete;
    static public int playerLevelRef;
    static public float playerEXPRef;
    static public float playerAttackRef;
    static public float playerDefenseRef;
    static public WeaponData[] weaponsRef = new WeaponData[8];
    static public CraftingMaterialData[] craftingMateriaslRef = new CraftingMaterialData[8];
    static public int[] craftingMaterialAmountRef = new int[8];

    static public bool firstTimeInGame;
    static public bool firstTimePickup;
    static public bool firstTimeAttacking;
    static public bool firstTimeCraftingMachine;
    static public bool firstTimePausing;

    /* ---------------------------------------------METHODS------------------------------------------------ */

    // Resets inventory
    static public void ResetInventories()
    {
        for (int i = 0; i < weapons.Length; i++) weapons[i] = null;
        for (int j = 0; j < craftingMaterials.Length; j++) craftingMaterials[j] = null;
    }

    // Changes Colour through Hex
    static public Color HexColour(string hex)
    {
        Color hexCol;
        ColorUtility.TryParseHtmlString(hex, out hexCol);
        return hexCol;
    }

    // Plays a BGM
    static public void PlayBGM(AudioClip sound)
    {
        GlobalAudio.gAud.BGM.clip = sound;
        GlobalAudio.gAud.BGM.Play();
    }

    // Plays a Sound Effect
    static public void PlaySoundEffect(AudioClip sound)
    {
        GlobalAudio.gAud.soundEffects.PlayOneShot(sound);
    }

    // Sets each specific information of a weapon, crafting material, etc, onto a UI
    static public void SelectedItemInformation(string dataName, string dataDesc, Sprite dataSprite, float dataAttackChange, float dataDefenseChange,
    TextMeshProUGUI name, TextMeshProUGUI desc, Image image, TextMeshProUGUI attackChange, TextMeshProUGUI defenseChange)
    {
        // Sets: Name
        if (name) name.text = dataName;

        // Sets: Description 
        if (desc) desc.text = dataDesc;

        // Sets: Sprite/Image
        if (image) image.sprite = dataSprite;

        // (Weapon) Sets: Attack Value
        if (attackChange) attackChange.text = SetModifiedValues(ref attackChange, dataAttackChange);

        // (Weapon) Sets: Defense Value
        if (defenseChange) defenseChange.text = SetModifiedValues(ref defenseChange, dataDefenseChange);

        // (Weapon: Attack/Defense) Positive or Negative Change Indication
        string SetModifiedValues(ref TextMeshProUGUI modText, float modifyVal)
        {
            modText.text = "";

            if (modifyVal < 0)
            {
                modText.color.HexColour("#B96761");
            }
            else if (modifyVal > 0)
            {
                modText.color.HexColour("#59BA5A");
                modText.text = "+";
            }

            return modText.text + modifyVal.ToString();
        }
    }

    static public void Volume(Slider slider, TextMeshProUGUI percentage, ref float volume, AudioSource audio)
    {
        percentage.text = slider.value.ToString("0") + "%";
        audio.volume = slider.value / 100;
        volume = slider.value;
    }

    static public void PlayAnimation(Animation anim, AnimationClip clip)
    {
        anim.Play(clip.name);
    }
}