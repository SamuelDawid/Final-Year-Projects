using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyIdentification : CanvasIdentification
{
    float refCurrentHPAmount;
    float refMaxHPAmount;

    [Header("HP Fields")]
    [SerializeField] TextMeshProUGUI enemyHPText;
    [SerializeField] Image enemyHP;

    void Start()
    {
        SetParameters();
    }

    void Update()
    {
        PlayerDetection();

        if (enemyHP.fillAmount != (refCurrentHPAmount / refMaxHPAmount))
            enemyHP.fillAmount = Mathf.Lerp(enemyHP.fillAmount, (refCurrentHPAmount / refMaxHPAmount), 3f * Time.deltaTime);
    }    

    public void UpdateHP(float currentHPAmount, float maxHPAmount)
    {
        refCurrentHPAmount = currentHPAmount;
        refMaxHPAmount = maxHPAmount;
        enemyHPText.text = refCurrentHPAmount + "/" + refMaxHPAmount;
    }
}