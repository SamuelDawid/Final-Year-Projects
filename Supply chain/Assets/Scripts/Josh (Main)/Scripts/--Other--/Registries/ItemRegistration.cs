using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// SINGLETON
public class ItemRegistration : MonoBehaviour
{
    static public ItemRegistration ir { get; private set; }

    [SerializeField] List<CraftingMaterialData> CraftingMaterials = new List<CraftingMaterialData>();
    [Space]
    [SerializeField] List<KeyItemData> KeyItems = new List<KeyItemData>();
    [Space]
    [SerializeField] List<WeaponData> Weapons = new List<WeaponData>();

    void Awake()
    {
        if (ir == null)
        {
            ir = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    public int ReturnListLength(Type type)
    {
        if (type == typeof(CraftingMaterialData)) return CraftingMaterials.Count;
        if (type == typeof(KeyItemData)) return KeyItems.Count;
        if (type == typeof(WeaponData)) return Weapons.Count; 

        return 0;
    }

    public int GetID(Type type, int index)
    {
        if (type == typeof(CraftingMaterialData)) return CraftingMaterials[index].GivenID;
        if (type == typeof(KeyItemData)) return KeyItems[index].GivenID; ;
        if (type == typeof(WeaponData)) return Weapons[index].GivenID;

        return 0;
    }

    public int GetIDBySprite(Type type, Sprite image)
    {
        if (type == typeof(CraftingMaterialData))
        {
            foreach(CraftingMaterialData cm in CraftingMaterials)
            {
                if (cm.GivenSprite == image) return cm.GivenID;
            }
        }

        if (type == typeof(WeaponData))
        {
            foreach (WeaponData wd in Weapons)
            {
                if (wd.GivenSprite == image) return wd.GivenID;
            }
        }

        return -1;
    }

    public CraftingMaterialData GetCraftingMaterial(int id)
    {
        foreach (CraftingMaterialData cm in CraftingMaterials)
            if (id == cm.GivenID) return cm;

        return null;
    }

    public KeyItemData GetKeyItem(int id)
    {
        foreach (KeyItemData ki in KeyItems)
            if (id == ki.GivenID) return ki;

        return null;
    }

    public WeaponData GetWeapon(int id)
    {
        foreach (WeaponData weapon in Weapons)
            if (id == weapon.GivenID) return weapon;

        return null;
    }
}