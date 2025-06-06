using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PinboardNotes : MonoBehaviour
{
    [SerializeField] Texture tt2DOption;
    [SerializeField] GameObject moveToLocation;
    [SerializeField] UnityEvent action;

    string notOverObjectColour = "#DECD98";
    string OverObjectColour = "#D2BA70";

    MeshRenderer meshNote;

    void Awake()
    {
        meshNote = transform.GetChild(0).GetComponent<MeshRenderer>();
        if (tt2DOption != null) meshNote.material.mainTexture = tt2DOption;
    }

    void OnMouseOver()
    {
        if(MainMenu.mms == MainMenu.MainMenuStatus.inOptions) 
            meshNote.material.color = GlobalScript.HexColour(OverObjectColour);
    }

    void OnMouseExit()
    {
        meshNote.material.color = GlobalScript.HexColour(notOverObjectColour);
    }

    void OnMouseDown()
    {
        if (MainMenu.mms == MainMenu.MainMenuStatus.inOptions)
        {action.Invoke();}
    }
}