using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest[] quest;
    private int  currentQUest;
    public static QuestManager instance;
    [SerializeField] Transform playerStartPosition;
    [SerializeField]
    private GameObject[] quest0Objects,quest1Objects,quest3Obj;
    public bool[] questCompleted;
    public Canvas wireCanvas;
    void Start()
    {
        instance = this;
        StartGame();
    }
    public void UpdateObjective(int questNumber)
    {
        if (questNumber >= quest.Length) return;

        GameplayEventSystem.ges.AlertNewObjective(quest[questNumber].title, quest[questNumber].description, quest[questNumber].requiredAmmount, quest[questNumber].currentAmmount, quest[questNumber].expReward);
    }
    void OnEnable()
    {
        GameplayEventSystem.ges.onNextQuest += NextQuest;
    }
    void OnDisable()
    {
     GameplayEventSystem.ges.onNextQuest -= NextQuest;
    }
    void NextQuest(int nextOne)
    {
    quest[nextOne].AddExp();
    quest[nextOne +1].isActive =true;
    quest[nextOne].isActive = false;
    }

    void StartGame(){
        UpdateObjective(0);
        Quest0Obj(false);
        //GlobalScript.playerRef.transform.position = playerStartPosition.position;
        questCompleted = new bool[quest.Length];
        wireCanvas.gameObject.SetActive(false);
        
    }

    void Update()
    {
        if(quest[0].isActive && quest[0].isReached())
        {
            if(!questCompleted[0])
            DestoryQuest0Obj();
            Quest0Obj(true);
             GameplayEventSystem.ges.NextQuest(0);
             UpdateObjective(1);
            questCompleted[0] = true;
        }
       if(quest[1].isActive)
        {
            if(quest[1].isReached() && !questCompleted[1])
            {
                GameplayEventSystem.ges.NextQuest(1);
                UpdateObjective(2);
                // ENABLE LIGHT TO MACHINES
                questCompleted[1] = true;
            }
        }
        if(quest[2].isActive)
        {
            if(quest[2].isReached() && !questCompleted[2])
            {
                 Destroyquest3Obj();
                GameplayEventSystem.ges.NextQuest(2);
                UpdateObjective(3);
                // ENABLE LIGHT TO CHIMNEY
                questCompleted[2] = true;
            }
        }
        if(quest[3].isActive)
        {
            if(quest[3].isReached() && !questCompleted[3])
            {
                wireCanvas.gameObject.SetActive(true);
                GameplayEventSystem.ges.NextQuest(3);
                UpdateObjective(4);
                questCompleted[3] = true;
            }
        }
        if(quest[4].isActive)
        {
            if(quest[4].isReached() && !questCompleted[4])
            {
                // WIN CONDITIONS
                questCompleted[4] = true;
            }
        }
    }
    void Quest0Obj(bool x)
    {
        for(int i =0; i < quest1Objects.Length; i++)
            {
                quest1Objects[i].SetActive(x);
            }
    }
    void DestoryQuest0Obj()
    {
         for(int i =0; i < quest0Objects.Length; i++)
            {
               Destroy(quest0Objects[i].gameObject);
            }
    }
    void Destroyquest3Obj()
    {
        for(int i =0; i < quest3Obj.Length; i++)
            {
               Destroy(quest3Obj[i].gameObject);
            }
    }
}