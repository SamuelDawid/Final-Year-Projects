using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI errorMessage;

    int[] requiredMaterials = new int[8];
    int[] currentMaterialsAmount = new int[8];

    int weaponShown = 201;

    Animator anim;
    bool firstTime = true;

    void Start()
    {
        ShowWeapon();
        anim = GetComponent<Animator>();
    }

    // FUNCTIONALITY --------------------------------------------------------------------------------------------------------------------

    void OnEnable()
    {
        GameplayEventSystem.ges.onIntoCraftingArea += WithinArea;
    }

    void WithinArea()
    {
        ShowWeapon();
        anim.enabled = true;
    }

    void setAnimation()
    {
        anim.SetInteger("CraftingState", 0);
    }

    public void GrabWeapon(int ID)
    {
        var state = HasRequiredMaterials(ID);

        if (state.Item2) GetWeapon(ID);
        else {
            errorMessage.text = state.Item1; 
            anim.SetInteger("CraftingState", 1);
        }  
    }

    public Tuple<string, bool> HasRequiredMaterials(int ID)
    {
        requiredMaterials = ItemRegistration.ir.GetWeapon(ID).GivenRequiredCraftingMaterials;
        currentMaterialsAmount = new int[GlobalScript.craftingMaterialAmount.Length];
        Array.Copy(GlobalScript.craftingMaterialAmount, currentMaterialsAmount, GlobalScript.craftingMaterialAmount.Length);

        // ---> AREA: CHECK IF REQUIRED MATERIALS EXIST

        // Going through each required material
        for (int a = 0; a < requiredMaterials.Length; a++)
        {
            for (int b = 0; b < GlobalScript.craftingMaterials.Length; b++)
            {
                // Check if the material exists within the inventory, otherwise return
                if (GlobalScript.craftingMaterials[b]?.GivenID == requiredMaterials[a])
                {
                    // Check if the material's quantity is greater than 0 from 'currentMaterialsAmount'
                    if (currentMaterialsAmount[b] > 0) { currentMaterialsAmount[b]--; break; }
                    else{
                        return new Tuple<string, bool>("Not enough materials!", false);
                    }
                }
                else {
                    if (b != GlobalScript.craftingMaterials.Length - 1) continue;
                    else
                    {
                        return new Tuple<string, bool>("You don't have the required crafting materials", false);
                    }
                }
            }
        }

        // ---> AREA: CHECK IF ENOUGH WEAPON SPACE IS IN INVENTORY

        if (GlobalScript.weapons.Length >= 8)
            return new Tuple<string, bool>("Too many weapons in inventory", true);


        return new Tuple<string, bool>("Weapon has been created!", true);
    }

    public void GetWeapon(int wdID)
    {
        // remove specific material from inventory if needed

        GlobalScript.playerRef.playerHUD.inventory.ItemModification(wdID, 0);
        GlobalScript.playerRef.playerHUD.SetCurrentWeapon(GlobalScript.weapons[GlobalScript.playerRef.playerHUD.selectedWeaponSlot]);

        // REMOVING MATERIALS FROM INVENTORY

        Array.Copy(currentMaterialsAmount, GlobalScript.craftingMaterialAmount, GlobalScript.craftingMaterialAmount.Length);

        for (int i = 0; i < GlobalScript.craftingMaterialAmount.Length; i++)
        {
            if (GlobalScript.craftingMaterialAmount[i] <= 0)
            {
                GlobalScript.craftingMaterials[i] = null;
            }
        }
        GlobalScript.playerRef.playerHUD.inventory.RefreshInventory(3);
        ShowWeapon();
    }

    // INFORMATION OUTPUT --------------------------------------------------------------------------------------------------------------------

    [SerializeField] TextMeshProUGUI weaponNameText;
    [SerializeField] TextMeshProUGUI weaponNameDesc;
    [SerializeField] Image weaponImage;
    [SerializeField] TextMeshProUGUI attackStatText;
    [SerializeField] TextMeshProUGUI defenseStatText;
    [SerializeField] Image[] requiredMaterialImages;
    [SerializeField] TextMeshProUGUI[] requiredMaterialNamesAmount;

    public void ShowWeapon()
    {
        ClearEverything();

        WeaponData wd = ItemRegistration.ir.GetWeapon(weaponShown);

        GlobalScript.SelectedItemInformation(wd.GivenName, wd.GivenDescription, wd.GivenSprite, wd.GivenAttack, wd.GivenDefense,
        weaponNameText, weaponNameDesc, weaponImage, attackStatText, defenseStatText);

        List<int> idReference = new List<int>(); // store references to materials that have been added
        List<int> requiredTotalAmount = new List<int>(); // store references to the quantity of each material

        // Sets the required materials
        for (int a = 0; a < wd.GivenRequiredCraftingMaterials.Length; a++)
        {
            bool exit = false;

            // If the same material has already beeen registered
            for (int b = 0; b < idReference.Count; b++)
            {
                if (wd.GivenRequiredCraftingMaterials[a] == idReference[b])
                {
                    requiredTotalAmount[b]++;
                    exit = true;
                    break;
                }
            }

            if (exit) continue;

            // Add material to list
            for (int c = 0; c < requiredMaterialImages.Length; c++)
            {
                if (!requiredMaterialImages[c].gameObject.activeSelf)
                {
                    requiredMaterialImages[c].gameObject.SetActive(true);
                    CraftingMaterialData cmd = ItemRegistration.ir.GetCraftingMaterial(wd.GivenRequiredCraftingMaterials[a]);
                    requiredMaterialImages[c].sprite = cmd.GivenSprite;
                    requiredMaterialNamesAmount[c].text = cmd.GivenName;
                    idReference.Add(cmd.GivenID);
                    requiredTotalAmount.Add(1);
                    break;
                }
            }
        }

        for (int c = 0; c < idReference.Count; c++)
        {
            for (int d = 0; d < GlobalScript.craftingMaterials.Length; d++)
            {
                if (GlobalScript.craftingMaterials[d] != null)
                {
                    if (idReference[c] == GlobalScript.craftingMaterials[d].GivenID)
                    {
                        requiredMaterialNamesAmount[c].text += " (" + GlobalScript.craftingMaterialAmount[d].ToString() + " / " + requiredTotalAmount[c] + ")";
                        break;
                    }
                }
                else 
                {
                    if (d != GlobalScript.craftingMaterials.Length - 1) continue;
                    else
                    {
                        requiredMaterialNamesAmount[c].text += " (0 / " + requiredTotalAmount[c] + ")";
                        break;
                    }
                }
            }
        }
    }

    void ClearEverything()
    {
        for (int i = 0; i < requiredMaterialImages.Length; i++)
        {
            requiredMaterialImages[i].gameObject.SetActive(false);
            requiredMaterialNamesAmount[i].text = "";
        }
    }

    public void ShowAnotherWeapon(int direction)
    {
        weaponShown += direction;

        if (weaponShown <= 200) weaponShown = ItemRegistration.ir.GetID(typeof(WeaponData), ItemRegistration.ir.ReturnListLength(typeof(WeaponData)) - 1);
        else if (weaponShown > ItemRegistration.ir.GetID(typeof(WeaponData), ItemRegistration.ir.ReturnListLength(typeof(WeaponData)) - 1))
        {
             weaponShown = 201;
        }

        ShowWeapon();
    }

    public void GenerateWeapon()
    {
        GrabWeapon(weaponShown);
    }
}
