using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    public VideoPlayer tutorial;
    public GameObject Canvas;
    public GameObject arrow, ParticleSystem;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GlobalStaticScript.tutorialvideo = true;
            Canvas.gameObject.SetActive(true);
            tutorial.Play();
            Destroy(arrow);
            Destroy(ParticleSystem);
        }
    }
}
