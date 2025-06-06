using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    Color col;

    // OPTIONS

    [SerializeField] GameObject optionsAreas;
    int currentOptionsArea = -1;

    // CROSSHAIRS

    [Header("Crosshairs")]
    [SerializeField] Image crosshairContainer;
    [SerializeField] Transform crosshairs;

    [Header("Crosshair Shape")]
    [SerializeField] TextMeshProUGUI currentCrosshairShapeText;
    [SerializeField] Slider crosshairSize;
    [SerializeField] Slider crosshairOpacity;

    // (Play Event is in 'KeycardUI - State' Function)

    void OnEnable()
    {
        setCrosshair();
    }

    // Restart the stage
    public void Restart()
    {
        GlobalScript.playerHPAmount = 100;
        GlobalScript.playerLevel = GlobalScript.playerLevelRef;
        GlobalScript.playerEXP = GlobalScript.playerEXPRef;
        GlobalScript.playerAttack = GlobalScript.playerAttackRef;
        GlobalScript.playerDefense = GlobalScript.playerDefenseRef;

        Array.Copy(GlobalScript.weaponsRef, GlobalScript.weapons, GlobalScript.weaponsRef.Length);
        Array.Copy(GlobalScript.craftingMateriaslRef, GlobalScript.craftingMaterials, GlobalScript.craftingMateriaslRef.Length);
        Array.Copy(GlobalScript.craftingMaterialAmountRef, GlobalScript.craftingMaterialAmount, GlobalScript.craftingMaterialAmountRef.Length);

        SwitchStage(SceneManager.GetActiveScene().buildIndex);
    }

    // Options
    public void Options()
    {
        if (currentOptionsArea == -1)
        {
            currentOptionsArea = 0;
            optionsAreas.transform.GetChild(currentOptionsArea).gameObject.SetActive(true);
        }
    }

    // Selecting a stage
    public void SwitchStage(int stage)
    {
        if (GameManager.inst != null) GameManager.inst.SwitchArea(SceneManager.GetActiveScene().buildIndex, stage);
        else SceneManager.LoadScene(stage);
    }

    // Exiting the game
    public void ExitGame()
    {
        GlobalScript.withinStage = false;
        Application.Quit(); 
    }

    // OPTIONS ------------------------------------///

    #region General

    public void switchToOptionsArea(int direction)
    {
        optionsAreas.transform.GetChild(currentOptionsArea).gameObject.SetActive(false);

        int previous = currentOptionsArea;
        currentOptionsArea += direction;

        if (currentOptionsArea < 0) currentOptionsArea = optionsAreas.transform.childCount - 1;
        else if (currentOptionsArea == optionsAreas.transform.childCount) currentOptionsArea = 0;

        optionsAreas.transform.GetChild(currentOptionsArea).gameObject.SetActive(true);
    }

    #endregion

    #region Customisation

        #region Crosshair Customisation

    void setCrosshair()
    {
        currentCrosshairShapeText.text = crosshairs.GetChild(GlobalScript.crosshair).name;
        crosshairContainer.sprite = crosshairs.GetChild(GlobalScript.crosshair).GetComponent<Image>().sprite;
        crosshairSize.value = GlobalScript.crosshairSize;
        crosshairContainer.color = col.HexColour(GlobalScript.crosshairColour);
        crosshairOpacity.value = GlobalScript.crosshairOpacity;

        CrosshairSize(crosshairSize);
        CrosshairColour(GlobalScript.crosshairColour);
        CrosshairOpacity(crosshairOpacity);
    }

        
    public void SwitchCrosshairs(int direction)
    {
        GlobalScript.crosshair += direction;

        if (GlobalScript.crosshair == -1) GlobalScript.crosshair = crosshairs.childCount - 1;
        else if (GlobalScript.crosshair == crosshairs.childCount) GlobalScript.crosshair = 0;

        crosshairContainer.sprite = crosshairs.GetChild(GlobalScript.crosshair).GetComponent<Image>().sprite;
        currentCrosshairShapeText.text = crosshairs.GetChild(GlobalScript.crosshair).name.ToString();
    }

    public void CrosshairSize(Slider slider)
    {
        float sizeVal = slider.value;
        crosshairContainer.rectTransform.localScale = new Vector3(sizeVal, sizeVal, sizeVal);
        GlobalScript.crosshairSize = slider.value;
    }

    public void CrosshairColour(string hex)
    {
        crosshairContainer.color = col.HexColour("#" + hex);
        GlobalScript.crosshairColour = hex;
    }

    public void CrosshairOpacity(Slider slider)
    {
        var alpha = crosshairContainer.color;
        alpha.a = slider.value;
        crosshairContainer.color = alpha;
        GlobalScript.crosshairOpacity = alpha.a;
    }

    #endregion

    #endregion
}