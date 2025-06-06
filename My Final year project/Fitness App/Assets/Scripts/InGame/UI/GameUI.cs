using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText, countDownText, finalScore;
    public static GameUI instance;
    public GameObject hand, finishUI, PauseMenu, screen;
    public bool startTimerFF;
    [SerializeField] private GameObject firstTimeCanvas;
    void OnEnable()
    {
        EventSystem.ES.onUpdateScore += OnUpdateScoreText; 
        EventSystem.ES.onPrevoiusInfo += GetPreviousInfo;
        EventSystem.ES.onResetUI += ResetUI;
        EventSystem.ES.onFinishGameUI += FinishingGameUI;
        EventSystem.ES.onMenuState += OnEnterMenu;
    }
    private void OnDisable()
    {
        EventSystem.ES.onUpdateScore -= OnUpdateScoreText;
        EventSystem.ES.onPrevoiusInfo -= GetPreviousInfo;
        EventSystem.ES.onResetUI -= ResetUI;
        EventSystem.ES.onFinishGameUI -= FinishingGameUI;
        EventSystem.ES.onMenuState -= OnEnterMenu;
    }
    private void Awake()
    {
        instance = this;
        EventSystem.ES.UpdateScoreText();
        firstTimeCanvas.SetActive(true);
    }
    private void Update() => Timer();
    void Timer()
    {
        if (GlobalStaticScript.startTimer)
        {
            screen.gameObject.SetActive(true);
            GlobalStaticScript.countDown -= Time.deltaTime;
            countDownText.text = string.Format("Time left" + ":" + Mathf.RoundToInt( GlobalStaticScript.countDown).ToString());
            if (GlobalStaticScript.countDown <= 0)
            {
                GlobalStaticScript.loadNextTrack = true;
                screen.gameObject.SetActive(false);
                GlobalStaticScript.startTimer = false;
                //GlobalStaticScript.countDown = 5;
            }
        }
        if (!GlobalStaticScript.firstTimeGame) { firstTimeCanvas.SetActive(false); }
    }

    public void OnUpdateScoreText() => scoreText.text = string.Format("SCORE\n{0}", GlobalStaticScript.playerScore.ToString());
    public void GetPreviousInfo()
    {
        GlobalStaticScript.playerLevel = GlobalStaticScript.playerLevelRef;
        GlobalStaticScript.playerEXP = GlobalStaticScript.playerExpRef;
        GlobalStaticScript.playerScore = GlobalStaticScript.playerScoreRef;
        GlobalStaticScript.Modifier = GlobalStaticScript.modifierRef;
        GlobalStaticScript.ScoreValue = GlobalStaticScript.ScoreValueRef;
    }
    public void FinishingGameUI()
    {
        finishUI.gameObject.SetActive(true);
        finalScore.text = GlobalStaticScript.ScoreValue.ToString();
    }

    public void ResetUI()
    {
        firstTimeCanvas.SetActive(true);
        GlobalStaticScript.firstTimeGame = true;
    }
    public void BackToGym()
    {
        GlobalStaticScript.state = GlobalStaticScript.GameState.inTheGym;
        EventSystem.ES.ResetGame();
    }
    public void ReturnToGame()
    {
        EventSystem.ES.ResumeMusic();
        Time.timeScale = 1f;
        PauseMenu.gameObject.SetActive(false);
        DisableRay();
        GlobalStaticScript.inMenuState = false;
        
       // EventSystem.ES.ChangeState((GlobalStaticScript.GameplayStatus)System.Enum.Parse(typeof(GlobalStaticScript.GameplayStatus), GlobalStaticScript.enumStateRef));
    }
    void OnEnterMenu()
    {
        //freezTime
        //Show UI
        EventSystem.ES.StopMusic();
        Time.timeScale = 0f;
        PauseMenu.gameObject.SetActive(true);
        EnableRay();
        GlobalStaticScript.inMenuState = true;
    }
   public void EnableRay()
    {
        hand.GetComponent<XRRayInteractor>().enabled = true;
        hand.GetComponent<XRInteractorLineVisual>().enabled = true;
        hand.GetComponent<LineRenderer>().enabled = true;
    }
    public void DisableRay()
    {
        hand.GetComponent<XRRayInteractor>().enabled = false;
        hand.GetComponent<XRInteractorLineVisual>().enabled = false;
        hand.GetComponent<LineRenderer>().enabled = false;
    }
    public void Quit()
    {
        EventSystem.ES.ResetGame();
        Application.Quit();
    }
}
