using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] spawnPoint;
    void OnEnable()
    {
        EventSystem.ES.onSpawnerState += SpawnerState;
    }
    void OnDisable()
    {
        EventSystem.ES.onSpawnerState -= SpawnerState;
    }
    public T Choose<T>(T a, T b, params T[] p)
    {
        int random = Random.Range(0, p.Length +1);
        if (random == 0) return a;
        if (random == 1) return b;
        return p[random -1];
    }
    void SpawnerState(GlobalStaticScript.SpawnerState spawnerState)
    {
        switch(spawnerState)
        {
            case GlobalStaticScript.SpawnerState.RedCubes:
                {
                   
                    GameObject cube = Instantiate(cubes[0], spawnPoint[0]);
                    cube.transform.localPosition = Vector3.zero;
                    cube.transform.Rotate(transform.forward,(int)Choose(-90,-180,0));
                    break;
                }
            case GlobalStaticScript.SpawnerState.GreenCubes:
                {
                  
                    GameObject cube = Instantiate(cubes[1], spawnPoint[1]);
                    cube.transform.localPosition = Vector3.zero;
                    cube.transform.Rotate(transform.forward, (int)Choose(90, 180,0));
                  
                    break;
                }
            case GlobalStaticScript.SpawnerState.OrangeCubes:
                {
                    GameObject cube = Instantiate(cubes[2], spawnPoint[2]);
                    cube.transform.localPosition = Vector3.zero;
                    cube.transform.Rotate(transform.forward, 0f);
                    break;
                }
            case GlobalStaticScript.SpawnerState.DoNotHit:
                {
                    GameObject cube = Instantiate(cubes[3], spawnPoint[Random.Range(0, 1)]);
                    cube.transform.localPosition = Vector3.zero;
                    cube.transform.Rotate(transform.forward, 0f);
                    break;
                }
        }
    }
    
  
}




/*
public GameObject[] cubes;
public Transform[] spawnPoint;
public AudioSource audioSource;
public SongData song;
public States states;
private float beat, timer, countDown;
public int randomRange, maxRange;
public enum States { State1, State2, State3, State4 };
// Start is called before the first frame update
void Start()
{
beat = (60 / (float)song.bpm) * song.speed * Time.deltaTime;
countDown = 0;
Invoke("StartSong", GameManager.instance.startTime - song.startTime);
}
void StartSong()
{
audioSource.PlayOneShot(song.song);

}

// Update is called once per frame
void Update()
{
countDown += Time.deltaTime;
if (randomRange == 1)
{
    states = States.State2;
}
else if (randomRange == 2)
{
    states = States.State3;
}
else if (randomRange == 0)
{
    states = States.State1;
}else if(randomRange == 3)
{
    states = States.State4;
}

if (timer > beat)
{
    randomRange = Random.Range(0, maxRange);
    timer -= beat;
}
    timer += Time.deltaTime;

    switch (states)
    {
        case States.State1:
            {
                //RedCubes
                if (countDown >= 1)
                {
                    GameObject cube = Instantiate(cubes[0], spawnPoint[0]);
                    cube.transform.localPosition = Vector3.zero;
                    cube.transform.Rotate(transform.forward, Random.Range(-90, -180));

                countDown = 0;
            }

                break;

        }
        case States.State2:
            {
                //greenCubes
                if (countDown >= 1)
                {
                    GameObject cube = Instantiate(cubes[1], spawnPoint[1]);
                    cube.transform.localPosition = Vector3.zero;
                    cube.transform.Rotate(transform.forward, Random.Range(90, 180));
                countDown = 0;

            }
                break;

        }
        case States.State3:
            {
                //OgraneCubes
                if (countDown >= 1)
                {
                    GameObject cube = Instantiate(cubes[2], spawnPoint[2]);
                    cube.transform.localPosition = Vector3.zero;
                    cube.transform.Rotate(transform.forward, 0f);
                countDown = 0;

                 }
                break;

            }
    case States.State4:
        {
            if (countDown >= 1)
            {
                GameObject cube = Instantiate(cubes[3], spawnPoint[Random.Range(0,1)]);
                cube.transform.localPosition = Vector3.zero;
                cube.transform.Rotate(transform.forward, 0f);
                countDown = 0;

            }
            break;
        }
    }
}
}

 */

