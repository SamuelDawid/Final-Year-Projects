using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager inst;
    [SerializeField] GameObject loadingScreen;
    List<AsyncOperation> scenes = new List<AsyncOperation>();

    void Awake()
    {
        inst = this;
        scenes.Add(SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive));

        StartCoroutine(LoadingScene());
    }

    public void SwitchArea(int unload, int load)
    {
        loadingScreen.gameObject.SetActive(true);
        scenes.Add(SceneManager.UnloadSceneAsync(unload));
        scenes.Add(SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive));
        StartCoroutine(LoadingScene());
    }

    public IEnumerator LoadingScene()
    {
        loadingScreen.gameObject.SetActive(true);

        foreach (AsyncOperation scene in scenes)
        {
            while (!scene.isDone)
                yield return null;
        }

        loadingScreen.SetActive(false);
    }
}