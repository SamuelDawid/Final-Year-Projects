using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameManagerGym : MonoBehaviour
{
    [SerializeField] private GameObject UICanvas,Player,Video;
    [SerializeField] private VideoPlayer tutorial;
    [SerializeField] private Camera mainCamera, tutorialCamera;
    [SerializeField] private Transform playerStartTransform;
    public float clipLenghs;
    private void Start()
    {
        Player.transform.position = playerStartTransform.position;
        GlobalStaticScript.state = GlobalStaticScript.GameState.inTheGym;
        clipLenghs = (float)tutorial.clip.length;
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
        GlobalStaticScript.state  = GlobalStaticScript.GameState.inGame;
    }


    void InTheGymState(GlobalStaticScript.GameState state)
    {
        switch (state)
        {
            case GlobalStaticScript.GameState.inTheGym:
                {
                    
                    if (tutorial.isPlaying)
                    {
                        mainCamera.gameObject.SetActive(false);
                        tutorialCamera.gameObject.SetActive(true);
                    }else 
                    {
                        mainCamera.gameObject.SetActive(true);
                        tutorialCamera.gameObject.SetActive(false);
                    }

                    break;
                }
        
        }

    }
    private void FixedUpdate()
    {
        if(GlobalStaticScript.tutorialvideo)
        {
            clipLenghs -= Time.deltaTime;
            if (clipLenghs < 0)
            {
                UICanvas.gameObject.SetActive(true);
                mainCamera.gameObject.SetActive(true);
                tutorialCamera.gameObject.SetActive(false);
                tutorial.gameObject.SetActive(false);
                GlobalStaticScript.tutorialvideo = false;
                Video.SetActive(false);
            }
        }
       
    }
}
