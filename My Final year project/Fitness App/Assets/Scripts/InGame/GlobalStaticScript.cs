using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GlobalStaticScript : MonoBehaviour
{
    static public bool inGame = false;
    #region --Game Values --- 
    static public int playerLevel = 1;
    static public float playerEXP = 0;
    static public int playerScore = 0;
    static public int missHitValue = 10;
    static public int ScoreValue = 10;
    static public int Modifier = 2;
    static public int comboCount = 0;
    static public int currentTrack;
    static public float countDown = 5;
    static public bool startTimer = false;
    static public bool loadNextTrack = false;
    static public bool firstTimeGame = true;
    static public bool tutorialvideo = false;
    static public bool inMenuState = false;
    static public bool firstTimePauseMenu = false;
    #endregion

    #region --Reference Variable--
    static public bool StageComplete;
    static public int playerLevelRef;
    static public int playerScoreRef;
    static public int ScoreValueRef;
    static public float playerExpRef;
    static public int modifierRef;
    static public string enumStateRef;
    #endregion
    //InGame Status
    public enum GameState { inTheGym, inGame }
    static public GameState state;
    //GameplayStatus
    public enum GameplayStatus { Track1, Track2, Track3, Track4, }
    static public GameplayStatus gps;
    public enum SpawnerState { RedCubes, GreenCubes, OrangeCubes, DoNotHit}
    static public SpawnerState ss;
    public enum FirstTrackState { UpperMid, UpperLeft, UpperRight, MiddleLeft,MiddleRight, Lower}
    static public FirstTrackState track1;

   public enum SaberState { LeftRed,RightBlue}
    static public SaberState saber;
}
