using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{
    public AudioSource[] SFX;
    public AudioSource[] recording;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        EventSystem.ES.onPlaySFX += OnPlaySFX;
        EventSystem.ES.onPlayRecording += OnPlayRecording;
    }
    private void OnDisable()
    {
        EventSystem.ES.onPlaySFX -= OnPlaySFX;
        EventSystem.ES.onPlayRecording -= OnPlayRecording;
    }

    void OnPlaySFX(int soundToPlay)
    {
        if(soundToPlay < SFX.Length) { SFX[soundToPlay].Play();}
    }
    void OnPlayRecording(int soundToPlay)
    {
        if(soundToPlay < recording.Length) { recording[soundToPlay].Play(); }
    }

   
}
