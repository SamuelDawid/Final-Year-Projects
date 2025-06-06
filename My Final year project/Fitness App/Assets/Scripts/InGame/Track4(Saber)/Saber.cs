using UnityEngine;

public class Saber : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] spawnPoints;
    void OnEnable()
    {
        EventSystem.ES.onSaber += OnSaber;
    }
    void OnDisable()
    {
        EventSystem.ES.onSaber -= OnSaber;
    }
    public void OnSaber(GlobalStaticScript.SaberState saberState)
    {
        switch(saberState)
        {
            case GlobalStaticScript.SaberState.LeftRed:
                {
                    GameObject cube = Instantiate(cubes[1], spawnPoints[Random.Range(2, 3)]);
                    cube.transform.localPosition = Vector3.zero;
                    cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
                    break;
                }
            case GlobalStaticScript.SaberState.RightBlue:
                {
                    GameObject cube = Instantiate(cubes[0], spawnPoints[Random.Range(0, 1)]);
                    cube.transform.localPosition = Vector3.zero;
                    cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
                    break;
                }
        }
        
    }
    /*
     * public class Saber : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] spawnPoints;
    public SongData songData;
    public float beat;
    private float timer;
    public AudioSource audioSource;
    void Start()
    {
        beat = (60 / (float)songData.bpm) * songData.speed;
        
    }

    void StartSong()
    {
        audioSource.PlayOneShot(songData.song);

    }
    private void Update()
    {
        if (timer > beat)
        {
            GameObject cube = Instantiate(cubes[Random.Range(0, 2)], spawnPoints[Random.Range(0, 4)]);
            cube.transform.localPosition = Vector3.zero;
            cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            timer -= beat;
        }
        timer += Time.deltaTime;
    }
}
     * 
     */


}
