using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float startTime = 3.0f;
    private float currentBeats, timer = 0;
    public float songTimer;
    public GameObject player;
    public bool songFinish, songStarted;
    public Transform playerStartPositon;
    private AudioSource audioSource;
    public SongData[] songData;
    [SerializeField] private GameObject[] tracks;
    [SerializeField] private GameObject[] holderLeftHand;
    [SerializeField] private GameObject[] holderRightHand;
    [SerializeField] private Transform spawnTracktransform;

    public GameObject currentTrack;

    public List<GameObject> tracksList = new List<GameObject>();
    public ButtonHandler primaryButtonHandler = null;
    public ButtonHandler secondaryButtonHandler = null;


    public void PrimaryButtonDown(XRController controller)
    {
        //menu Level
        if(GlobalStaticScript.firstTimePauseMenu)
        { if (!GlobalStaticScript.inMenuState) { EventSystem.ES.MenuState(); audioSource.Pause(); } }
       
    }
    public void SecondaryButtonDown(XRController controller)
    {
        if (GlobalStaticScript.firstTimeGame) StartCoroutine(GameManager.instance.StartConditions());
    }

    void Awake()
    {
        instance = this;
        GlobalStaticScript.state = GlobalStaticScript.GameState.inGame;

        for (int i = 0; i < tracks.Length; i++)
        {
            tracksList.Add(tracks[i]);
        }
    }
    private void Start()
    {
        player.transform.position = playerStartPositon.transform.position;
        DontDestroyOnLoad(this);
    }

    void OnEnable()
    {
        EventSystem.ES.onResetGame += OnResetGame;
        EventSystem.ES.onChangeState += State;
        EventSystem.ES.onResumeMusic += OnResumeMusic;
        EventSystem.ES.onStopMusic += OnStopMusic;
        EventSystem.ES.onLevelCompleate += LevelCompleate;
        primaryButtonHandler.OnbuttonUp += PrimaryButtonDown;
        secondaryButtonHandler.OnbuttonUp += SecondaryButtonDown;

    }
    void OnDisable()
    {
        EventSystem.ES.onResetGame -= OnResetGame;
        EventSystem.ES.onChangeState -= State;
        EventSystem.ES.onResumeMusic -= OnResumeMusic;
        EventSystem.ES.onStopMusic -= OnStopMusic;
        EventSystem.ES.onLevelCompleate -= LevelCompleate;
        primaryButtonHandler.OnButtonDown -= PrimaryButtonDown;
        secondaryButtonHandler.OnButtonDown -= SecondaryButtonDown;
    }

    public static T RandomEnumValue<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        if (GlobalStaticScript.gps == GlobalStaticScript.GameplayStatus.Track2)
        {
            T V = (T)A.GetValue(UnityEngine.Random.Range(0, (A.Length - 2)));
            return V;
        }
        if (GlobalStaticScript.gps == GlobalStaticScript.GameplayStatus.Track1)
        {
            T X = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
            return X;
        }
        else
        {
            T Z = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
            return Z;
        }
    }
    #region -- Switch Global States--
    void State(GlobalStaticScript.GameplayStatus gpStatus)
    {
        switch (gpStatus)
        {

            case GlobalStaticScript.GameplayStatus.Track1:
                {
                    stateAction(tracksList[0], spawnTracktransform, gpStatus, audioSource, songData[0], currentBeats);
                    if (audioSource.isPlaying) { songTimer = songData[0].song.length; }
                    break;
                }
            case GlobalStaticScript.GameplayStatus.Track2:
                {
                    EventSystem.ES.PlayRecording(2);
                    stateAction(tracksList[1], spawnTracktransform, gpStatus, audioSource, songData[1], currentBeats);
                    if (audioSource.isPlaying) { songTimer = songData[1].song.length; }
                    break;
                }
            case GlobalStaticScript.GameplayStatus.Track3:
                {
                    EventSystem.ES.PlayRecording(4);
                    stateAction(tracksList[2], spawnTracktransform, gpStatus, audioSource, songData[2], currentBeats);
                    if (audioSource.isPlaying) { songTimer = songData[2].song.length; }
                    break;
                }
            case GlobalStaticScript.GameplayStatus.Track4:
                {
                    EventSystem.ES.PlayRecording(3);
                    stateAction(tracksList[3], spawnTracktransform, gpStatus, audioSource, songData[3], currentBeats);
                    if (audioSource.isPlaying) { songTimer = songData[3].song.length; }
                    break;
                }
        }
        void stateAction(GameObject activeObject, Transform spawnPowint, GlobalStaticScript.GameplayStatus state, AudioSource source, SongData currentSongData, float beats)
        {

            GlobalStaticScript.gps = state;
            GlobalStaticScript.enumStateRef = state.ToString();
            GlobalStaticScript.countDown = 5;
            currentTrack = Instantiate(activeObject, spawnPowint);
            if (!currentTrack.activeInHierarchy) currentTrack.SetActive(true);
            beats = (60 / (float)currentSongData.bpm) * currentSongData.speed;
            currentBeats = beats;
            if (!source.isPlaying)
            {
                source.clip = currentSongData.song;
                source.Play();
            }
            songStarted = true;
            GlobalStaticScript.loadNextTrack = false;
        }
    }
    void FixedUpdate()
    {
        //SongTimer();
        CheckConditions();
    }
    bool isReached()
    {
        return (songTimer <= 0);
    }
    public void LevelCompleate(GameObject disableTrack, bool timerStart, GlobalStaticScript.GameplayStatus nextLevel, GameObject previousWeponLeftHand, GameObject previousWeponRightHand, GameObject currentWeponLeftHand, GameObject currentsWeponRightHand)
    {
        // compleate level
        audioSource.clip = null;
        Destroy(currentTrack);
        GlobalStaticScript.startTimer = timerStart;
        previousWeponLeftHand.SetActive(false);
        previousWeponRightHand.SetActive(false);
        currentWeponLeftHand.SetActive(true);
        currentsWeponRightHand.SetActive(true);
        if (GlobalStaticScript.loadNextTrack) { EventSystem.ES.ChangeState(nextLevel); GlobalStaticScript.countDown = 5; }
    }
    #endregion
    #region --- UPDATE ---
    void CheckConditions()
    {

        if (songStarted)
        {
            songTimer -= Time.deltaTime;
            if (GlobalStaticScript.gps == GlobalStaticScript.GameplayStatus.Track1)
            {
                timer += Time.deltaTime;
                if (timer > currentBeats) { EventSystem.ES.FirstTrackState(RandomEnumValue<GlobalStaticScript.FirstTrackState>()); timer -= currentBeats; }
                if (isReached())
                {
                    EventSystem.ES.LevelCompleate(tracksList[0], true, GlobalStaticScript.GameplayStatus.Track2, holderLeftHand[0], holderRightHand[0], holderLeftHand[1], holderRightHand[1]);
                }
            }
            if (GlobalStaticScript.gps == GlobalStaticScript.GameplayStatus.Track2)
            {
                timer += Time.deltaTime;
                if (timer > currentBeats) { EventSystem.ES.ChangeSpawnerSate(RandomEnumValue<GlobalStaticScript.SpawnerState>()); timer -= currentBeats; }
                bool oneTime = false;
                if (!oneTime)
                {
                    if (songTimer <= 30 && songStarted)
                    {
                        EventSystem.ES.PlayRecording(1);
                        oneTime = true;
                    }
                }
                if (isReached())
                {
                    EventSystem.ES.LevelCompleate(tracksList[2], true, GlobalStaticScript.GameplayStatus.Track3, holderLeftHand[0], holderRightHand[0], holderLeftHand[1], holderRightHand[1]);
                }
            }
            if (GlobalStaticScript.gps == GlobalStaticScript.GameplayStatus.Track3)
            {
                timer += Time.deltaTime;
                if (timer > currentBeats) { EventSystem.ES.ChangeSpawnerSate(RandomEnumValue<GlobalStaticScript.SpawnerState>()); timer -= currentBeats; }
                if (isReached())
                {
                    EventSystem.ES.LevelCompleate(tracksList[3], true, GlobalStaticScript.GameplayStatus.Track4, holderLeftHand[1], holderRightHand[1], holderLeftHand[2], holderRightHand[2]);
                }
            }
            if (GlobalStaticScript.gps == GlobalStaticScript.GameplayStatus.Track4)
            {
                timer += Time.deltaTime;
                if (timer > currentBeats) { EventSystem.ES.Saber(RandomEnumValue<GlobalStaticScript.SaberState>()); timer -= currentBeats; }
                if (isReached())
                {
                    FinishGame();
                }
            }

        }
    }
    #endregion
    public IEnumerator StartConditions()
    {
        audioSource = GetComponent<AudioSource>();

        GlobalStaticScript.startTimer = true;
        GlobalStaticScript.firstTimeGame = false;
        yield return new WaitForSeconds(5);
        EventSystem.ES.ChangeState(GlobalStaticScript.GameplayStatus.Track1);

    }
    public void OnResetGame()
    {
        tracksList.Clear();
        EventSystem.ES.GetPreviousInf();
        EventSystem.ES.ResetUI();
        holderLeftHand[0].SetActive(true);
        holderRightHand[0].SetActive(true);
        holderLeftHand[1].SetActive(false);
        holderRightHand[1].SetActive(false);
        holderLeftHand[2].SetActive(false);
        holderRightHand[2].SetActive(false);
        EventSystem.ES.UpdateScoreText();
        StartCoroutine(StartConditions());
        Time.timeScale = 1f;
    }
    public void OnResumeMusic()
    {
        if (audioSource != null) audioSource.Play();
    }
    public void OnStopMusic()
    {
        if (audioSource != null) audioSource.Pause();
    }
    public void FinishGame() => EventSystem.ES.FinishGameUI();

}
