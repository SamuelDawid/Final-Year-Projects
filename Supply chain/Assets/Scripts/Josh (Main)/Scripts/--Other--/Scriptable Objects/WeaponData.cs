using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] int givenID;
    [SerializeField] string givenName;
    [SerializeField] string givenDescription;
    [SerializeField] GameObject givenWeapon;
    [SerializeField] Sprite givenSprite;
    [Space][Space]
    [SerializeField] float givenAttack;
    [SerializeField] float givenDefense;
    [Space][Space]
    [Header("Store Crafting Material IDs Here (300 - 399)")]
    [SerializeField] int[] givenRequiredCraftingMaterials;
    [HideInInspector] public Weapon weaponRef;

    public int GivenID
    {
        get { return givenID; }
    }

    public string GivenName
    {
        get { return givenName; }
    }

    public string GivenDescription
    {
        get { return givenDescription; }
    }

    public GameObject GivenWeapon
    {
        get { return givenWeapon; }
    }

    public Sprite GivenSprite
    {
        get { return givenSprite; }
    }

    public float GivenAttack
    {
        get { return givenAttack; }
    }

    public float GivenDefense
    {
        get { return givenDefense; }
    }

    public int[] GivenRequiredCraftingMaterials
    {
        get { return givenRequiredCraftingMaterials; }
    }

    public Weapon SpawnWeapon()
    {
        Weapon weapon = Instantiate(givenWeapon.transform.GetComponent<Weapon>());
        weapon.SetParameters(GivenID, GivenAttack, GivenDefense);
        return weaponRef = weapon;
    }
}