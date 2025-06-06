using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StageSelection : MonoBehaviour
{
    int previousSelectedStage;
    int currentSelectedStage;
    int goToScene;

    [SerializeField] Image selectedStage;
    [SerializeField] TextMeshProUGUI stageTitle;
    [SerializeField] Image[] stages;
    [SerializeField] string[] stageNames;
    [SerializeField] int[] sceneIndexes;

    void Start()
    {
        currentSelectedStage = 0;
    }

    void OnEnable()
    {
        selectedStage.sprite = stages[currentSelectedStage].sprite;
        stageTitle.text = stageNames[currentSelectedStage];
    }

    public void SwitchStage(int direction)
    {
        previousSelectedStage = currentSelectedStage;
        currentSelectedStage += direction;

        if (currentSelectedStage < 0) currentSelectedStage = stages.Length - 1;
        else if (currentSelectedStage >= stages.Length) currentSelectedStage = 0;

        selectedStage.sprite = stages[currentSelectedStage].sprite;
        stageTitle.text = stageNames[currentSelectedStage];
        goToScene = sceneIndexes[currentSelectedStage];
    }

    public void SelectStage()
    {
        if (goToScene == 0) goToScene = 2;

        if (GameManager.inst != null) GameManager.inst.SwitchArea(SceneManager.GetActiveScene().buildIndex, goToScene);
        else SceneManager.LoadScene(goToScene);
    }
}
