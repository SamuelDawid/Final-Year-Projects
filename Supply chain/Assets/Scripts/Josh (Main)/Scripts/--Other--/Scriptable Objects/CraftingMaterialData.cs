using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Material", menuName = "Items/Crafting Material")]
public class CraftingMaterialData : ScriptableObject
{
    [SerializeField] int givenID;
    [SerializeField] string givenName;
    [SerializeField] string givenDescription;
    [SerializeField] GameObject givenCraftingMaterial;
    [SerializeField] Sprite givenSprite;

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

    public GameObject GivenCraftingMaterial
    {
        get { return givenCraftingMaterial; }
    }

    public Sprite GivenSprite
    {
        get { return givenSprite; }
    }
}