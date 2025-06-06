using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMove : MonoBehaviour
{
    public GameObject track1;
    public GameObject[] cubes;
    public Transform[] spawnPoints;

    private void OnEnable()
    {
        EventSystem.ES.onFirstTrackState += FirstTrackSpawner;
    }
    private void OnDisable()
    {
        EventSystem.ES.onFirstTrackState -= FirstTrackSpawner;
    }

    void FirstTrackSpawner(GlobalStaticScript.FirstTrackState spawnState)
    {
        switch(spawnState)
        {
            case GlobalStaticScript.FirstTrackState.UpperMid:
                {
                    GameObject cubeR = Instantiate(cubes[0], spawnPoints[0]);
                    GameObject cubeG = Instantiate(cubes[1], spawnPoints[1]);
                    cubeR.transform.localPosition = Vector3.zero;
                    cubeG.transform.localPosition = Vector3.zero;
                    break;
                }
            case GlobalStaticScript.FirstTrackState.UpperLeft:
                {
                    GameObject cubeR = Instantiate(cubes[0], spawnPoints[2]);
                    GameObject cubeG = Instantiate(cubes[1], spawnPoints[3]);
                    cubeR.transform.localPosition = Vector3.zero;
                    cubeG.transform.localPosition = Vector3.zero;
                    break;
                }
            case GlobalStaticScript.FirstTrackState.UpperRight:
                {
                    GameObject cubeR = Instantiate(cubes[0], spawnPoints[4]);
                    GameObject cubeG = Instantiate(cubes[1], spawnPoints[5]);
                    cubeR.transform.localPosition = Vector3.zero;
                    cubeG.transform.localPosition = Vector3.zero;
                    break;
                }
            case GlobalStaticScript.FirstTrackState.MiddleLeft:
                {
                    GameObject cubeR = Instantiate(cubes[0], spawnPoints[6]);
                    GameObject cubeG = Instantiate(cubes[1], spawnPoints[7]);
                    cubeR.transform.localPosition = Vector3.zero;
                    cubeG.transform.localPosition = Vector3.zero;
                    break;
                }
            case GlobalStaticScript.FirstTrackState.MiddleRight:
                {
                    GameObject cubeR = Instantiate(cubes[0], spawnPoints[8]);
                    GameObject cubeG = Instantiate(cubes[1], spawnPoints[9]);
                    cubeR.transform.localPosition = Vector3.zero;
                    cubeG.transform.localPosition = Vector3.zero;
                    break;
                }
            case GlobalStaticScript.FirstTrackState.Lower:
                {
                    GameObject cubeR = Instantiate(cubes[0], spawnPoints[10]);
                    GameObject cubeG = Instantiate(cubes[1], spawnPoints[11]);
                    cubeR.transform.localPosition = Vector3.zero;
                    cubeG.transform.localPosition = Vector3.zero;
                    break;
                }
        }
    }
}
