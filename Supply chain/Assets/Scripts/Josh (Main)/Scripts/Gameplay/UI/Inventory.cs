using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    HUD hud;

    // Weapons
    public Image[] weaponSlotImages = new Image[8];

    // Crafting Materials
    [SerializeField] Image[] craftingMaterialSlotImages = new Image[8];
    [SerializeField] TextMeshProUGUI[] craftingMaterialQuantityText = new TextMeshProUGUI[8];

    // Player Stats
    [SerializeField] TextMeshProUGUI hpValue;
    [SerializeField] TextMeshProUGUI levelValue;
    [SerializeField] TextMeshProUGUI attackValue;
    [SerializeField] TextMeshProUGUI defenseValue;

    // Item Information
    [SerializeField] TextMeshProUGUI objectTitle;
    [SerializeField] TextMeshProUGUI objectDescription;
    [SerializeField] Image objectSprite;
    [SerializeField] TextMeshProUGUI objectmodifiedAttack;
    [SerializeField] TextMeshProUGUI objectmodifiedDefense;

    // Different Fields
    [SerializeReference] GameObject objectiveField;
    [SerializeReference] GameObject statsStuffField;

    // Objectives
    [SerializeField] TextMeshProUGUI objectiveTitle;
    [SerializeField] TextMeshProUGUI objectiveDescription;
    [SerializeField] TextMeshProUGUI objectiveReward;
    [SerializeField] TextMeshProUGUI objectiveRequireAmount;

    void Awake()
    {
        objectiveTitle.text = objectDescription.text = objectiveRequireAmount.text = "";
    }

    void OnEnable()
    {
        GameplayEventSystem.ges.onAlertNewObjective += SetObjective;
        GameplayEventSystem.ges.onDropWeapon += WeaponDropped;

        if (hud == null) FindObjectOfType<HUD>();

        hpValue.text = GlobalScript.playerHPAmount.ToString() + "%";    
        levelValue.text = GlobalScript.playerLevel.ToString();    
        attackValue.text = GlobalScript.playerAttack.ToString();    
        defenseValue.text = GlobalScript.playerDefense.ToString();

        SetImagesOn();
    }

    void OnDisable()
    {
        GameplayEventSystem.ges.onAlertNewObjective -= SetObjective;
        GameplayEventSystem.ges.onDropWeapon -= WeaponDropped;
        TurnOffImages();
    }

    void SetImagesOn()
    {
        for (int i = 0; i < GlobalScript.weapons.Length; i++)
        {
            if (GlobalScript.weapons[i] == null) break;

            weaponSlotImages[i].enabled = true;
            weaponSlotImages[i].sprite = GlobalScript.weapons[i].GivenSprite;
        }

        for (int i = 0; i < GlobalScript.craftingMaterials.Length; i++)
        {
            if (GlobalScript.craftingMaterials[i] == null) break;

            craftingMaterialSlotImages[i].enabled = true;
            craftingMaterialSlotImages[i].sprite = GlobalScript.craftingMaterials[i].GivenSprite;
        }
    }

    void TurnOffImages()
    {
        foreach(Image image in weaponSlotImages)
        {
            image.enabled = false;
        }

        foreach (Image image in craftingMaterialSlotImages)
        {
            image.enabled = false;
        }
    }

    // Add Weapons to Inventory
    public void ItemModification(int ID, int whatToDo)
    {
        switch (ID)
        {
            // WEAPON
            case int x when ID >= 200 && ID < 300:
                if (whatToDo == 0) AddItem(ID, GlobalScript.weapons, ItemRegistration.ir.GetWeapon, weaponSlotImages);
                else if (whatToDo == 1) RemoveWeapon(ID, GlobalScript.weapons);
                break;

            // CRAFTING MATERIAL
            case int x when ID >= 300 && ID < 400:
                if (whatToDo == 0) AddItem(ID, GlobalScript.craftingMaterials, ItemRegistration.ir.GetCraftingMaterial, craftingMaterialSlotImages);
                break;
        }

        // Adds an item based on the list
        void AddItem<T>(int ID, T[] data, Func<int, T> action, Image[] slots) where T : ScriptableObject
        {
            for (int a = 0; a < data.Length; a++)
            {
                if (data[a] == null)
                {
                    var info = action.Invoke(ID);

                    try 
                    {
                        if (AddCraftingItemQuantity(info as CraftingMaterialData)) return;
                    } catch { }

                    data[a] = info;

                    if (info as CraftingMaterialData)
                    {
                        slots[a].sprite = (info as CraftingMaterialData).GivenSprite;
                        GlobalScript.craftingMaterialAmount[a]++;
                        craftingMaterialQuantityText[a].text = GlobalScript.craftingMaterialAmount[a].ToString();
                    }
                    else if (info as WeaponData)
                    {
                        var e = (data[a] as WeaponData);
                        slots[a].sprite = e.GivenSprite;
                    }
                    slots[a].gameObject.SetActive(true);
                    break;
                }
            }
        }

        void RemoveWeapon(int ID, WeaponData[] data)
        {
            switch (ID)
            {
                // CRAFTING MATERIAL
                case int x when ID >= 0 && ID < 100:
                    break;
    
                // WEAPON
                case int x when ID >= 200 && ID < 300:
                    break;
            }
        }

        // If a crafting item already exists within the array, just add to its quantity
        bool AddCraftingItemQuantity(CraftingMaterialData info)
        {
            foreach (CraftingMaterialData cm in GlobalScript.craftingMaterials)
            {
                if (cm.GivenID == info.GivenID)
                {
                    int QuantityText = Array.IndexOf(GlobalScript.craftingMaterials, cm);
                    GlobalScript.craftingMaterialAmount[QuantityText]++;
                    craftingMaterialQuantityText[QuantityText].text = GlobalScript.craftingMaterialAmount[QuantityText].ToString();
                    return true;
                }
            }
            return false;
        }
    }

    void WeaponDropped(int num)
    {
        RefreshInventory(1);
    }

    // Refreshes the inventory
    public void RefreshInventory(int area)
    {
        if (area == 1) RefreshArea(weaponSlotImages, GlobalScript.weapons, null);
        else if (area == 2) RefreshArea(craftingMaterialSlotImages, GlobalScript.craftingMaterials, craftingMaterialQuantityText);
        else if (area == 3)
        {
            RefreshArea(weaponSlotImages, GlobalScript.weapons, null);
            RefreshArea(craftingMaterialSlotImages, GlobalScript.craftingMaterials, craftingMaterialQuantityText);
        }

        // Refreshes each area
        void RefreshArea<T>(Image[] images, T[] area, TextMeshProUGUI[] quantity) where T : ScriptableObject
        {
            if (quantity != null) foreach (TextMeshProUGUI txt in craftingMaterialQuantityText) txt.text = "---";

            // Clears each image
            foreach (Image img in images)
            {
                img.sprite = null; 
                img.gameObject.SetActive(false); 
            }

            // Resets each images
            for (int i = 0; i < area.Length; i++)
            {
                if (area[i] != null)
                {
                    images[i].gameObject.SetActive(true);

                    if (area[i].GetType() == typeof(WeaponData)) 
                    {
                        images[i].sprite = (area[i] as WeaponData).GivenSprite; 
                    }
                    else if (area[i].GetType() == typeof(CraftingMaterialData)) 
                    {
                        images[i].sprite = (area[i] as CraftingMaterialData).GivenSprite;

                        // (Crafting Material) Resets quantity
                        craftingMaterialQuantityText[i].text = GlobalScript.craftingMaterialAmount[i].ToString();
                    }
                }
            }
        }
    }

    public void SetStatsAndStuffInfo(int id)
    {
        Debug.Log(1);
        GameObject gm = EventSystem.current.currentSelectedGameObject;

        if (gm == null) return;

        if (gm.GetComponent<Image>().sprite != null)
        {
            Sprite spt = gm.GetComponent<Image>().sprite;

            foreach (WeaponData wd in GlobalScript.weapons)
            {
                Debug.Log(2);
                if (wd == null) break;
                if (wd.GivenSprite == spt)
                {
                    GlobalScript.SelectedItemInformation(wd.GivenName, wd.GivenDescription, wd.GivenSprite, wd.GivenAttack, wd.GivenDefense,
                    objectTitle, objectDescription, objectSprite, objectmodifiedAttack, objectmodifiedDefense);
                    GUIES.currentSelected = gm;

                    return;
                }
            }

            foreach (CraftingMaterialData cm in GlobalScript.craftingMaterials)
            {
                if (cm.GivenSprite == spt)
                {
                    GlobalScript.SelectedItemInformation(cm.GivenName, cm.GivenDescription, cm.GivenSprite, 0, 0, objectTitle, objectDescription, objectSprite, null, null);
                    Debug.Log(2);
                    GUIES.currentSelected = gm;
                    return;
                }
            }
        }
    }

    void DropWeapon()
    {
        
    }

    // ----------------------------------------------------

    public void SetObjective(string title, string description, int required, int current, int expReward)
    {
        objectiveTitle.text = title;
        objectiveDescription.text = description;
        objectiveReward.text = "( Reward: " + expReward + " EXP)";
        objectiveRequireAmount.text = "Required Amount " + current + "/" + required;
    }
}