using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : CentralPickupStructure
{
    [Space]
    [Header("Key Item Data")]
    public KeyItemData infoKID;
    [SerializeField] Sprite keyItemTypeSprite;

    void Awake()
    {
        SetParameters();
        GetComponentInChildren<KeyItemIdentification>().CanvasParameters(infoKID.GivenName, keyItemTypeSprite); // Maybe change to designated sprite of each key item?
    }
}