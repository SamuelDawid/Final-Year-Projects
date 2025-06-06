using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisshitDetector : MonoBehaviour
{
    private static string NAME = "Blocks";
    void Start()
    {
         EventSystem.ES.onMissCube += OnMissCube;
    }
     public void OnMissCube(int missBlock)
    {
        GlobalStaticScript.playerScore -= missBlock;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(NAME))
        {
            EventSystem.ES.MissCube(GlobalStaticScript.missHitValue);
            Destroy(other.gameObject);
            GlobalStaticScript.tutorialvideo = true;
            EventSystem.ES.UpdateScoreText();
            if (GlobalStaticScript.comboCount > 0) GlobalStaticScript.comboCount = 0;
        }
    }
   
}
