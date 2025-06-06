using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public static EventSystem ES;

    void Awake()
    {
        ES = this;
    }

    #region Action --> event <--
    public event Action onPlayerSpawnPosition;
    public event Action onUpdateScore;
    public event Action onResetGame;
    public event Action onResetUI;
    public event Action onPrevoiusInfo;
    public event Action onFinishGameUI;
    public event Action onMenuState;
    public event Action onStopMusic;
    public event Action onResumeMusic;
    #endregion

    #region Action<int> --> event<int> <--
    public event Action<int> onMissCube;
    public event Action<int> onHitWrongBlock;
    public event Action<int> onAddScore;
    public event Action<GameObject, bool, GlobalStaticScript.GameplayStatus, GameObject, GameObject, GameObject, GameObject> onLevelCompleate;
    public event Action<GlobalStaticScript.SaberState> onSaber;
    public event Action<GlobalStaticScript.GameplayStatus> onChangeState;
    public event Action<GlobalStaticScript.SpawnerState> onSpawnerState;
    public event Action<GlobalStaticScript.GameplayStatus> onChangeStateUI;
    public event Action<GlobalStaticScript.FirstTrackState> onFirstTrackState;
    public event Action<int> onPlayRecording;
    public event Action<int> onPlaySFX;


#endregion

#region Action events functons
    public void UpdateScoreText() {if(onUpdateScore != null) onUpdateScore();}
    public void PlayerSpawnPosition() { if (onPlayerSpawnPosition != null) onPlayerSpawnPosition(); }
    public void FinishGameUI() { if (onFinishGameUI != null) onFinishGameUI(); }
    public void StopMusic() { if (onStopMusic != null) onStopMusic(); }
    public void ResumeMusic() { if (onResumeMusic != null) onResumeMusic(); }
    public void MenuState() { if (onMenuState != null) onMenuState(); }
    public void ResetUI() { if (onResetUI != null) onResetUI(); }
    public void GetPreviousInf() { if (onPrevoiusInfo != null) onPrevoiusInfo(); }
    public void MissCube(int x) {if(onMissCube != null) onMissCube(x);}
    public void HitWrongBlock(int y){if(onHitWrongBlock != null) onHitWrongBlock(y);}
    public void AddScore(int z){if(onAddScore != null) onAddScore(z);}
    public void LevelCompleate(GameObject disableTrack, bool timerStart, GlobalStaticScript.GameplayStatus nextLevel, GameObject previousWeponLeftHand, GameObject previousWeponRightHand, GameObject currentWeponLeftHand, GameObject currentsWeponRightHand) {if(onLevelCompleate!=null) onLevelCompleate(disableTrack,timerStart,nextLevel, previousWeponLeftHand, previousWeponRightHand, currentWeponLeftHand, currentsWeponRightHand);}
    public void Saber(GlobalStaticScript.SaberState saberState) { if (onSaber != null) onSaber(saberState); }
    public void ResetGame() {if(onResetGame != null) onResetGame();}
    public void PlaySFX(int songToPlay) { if (onPlaySFX != null) onPlaySFX(songToPlay); }
    public void PlayRecording(int songToPlay) { if (onPlayRecording != null) onPlayRecording(songToPlay); }
    public void ChangeState(GlobalStaticScript.GameplayStatus status)
    {if(onChangeState != null) onChangeState(status);}
    public void FirstTrackState(GlobalStaticScript.FirstTrackState state) { if (onFirstTrackState != null) onFirstTrackState(state); }
    public void ChangeStateUI(GlobalStaticScript.GameplayStatus statusUI) { if (onChangeStateUI != null) onChangeStateUI(statusUI); }
    public void ChangeSpawnerSate(GlobalStaticScript.SpawnerState spawnerState)
    { if (onSpawnerState != null) onSpawnerState(spawnerState); }
   
    
#endregion
}
