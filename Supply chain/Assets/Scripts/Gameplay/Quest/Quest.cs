using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest 
{
    public bool isActive;
    public string title;
    public string description;
    public int requiredAmmount;
    public int currentAmmount;
    public int expReward;
    public void AddAmmount()
    {
        currentAmmount++;
    }
    public void MinusAmmount()
    {
        currentAmmount--;
    }
    public bool isReached()
    {
        return (currentAmmount >= requiredAmmount);
    }
    public void AddExp()
    {
        GlobalScript.playerRef.playerHUD.ModifyLevel(expReward);
        
    }
}
