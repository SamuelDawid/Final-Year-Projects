using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SINGLETON
public class GlobalAudio : MonoBehaviour
{
    static public GlobalAudio gAud { get; private set; }
    public AudioSource BGM;
    public AudioSource soundEffects;
    [Space]
    [Space]

    [Header("AUDIO CLIPS (BGM)")]
    public AudioClip bgmMainMenu;
    public AudioClip bgmGameplay;
    public AudioClip bgmVictory;

    [Header("AUDIO CLIPS (SOUND EFFECTS)")]
    public AudioClip radioPressed;
    public AudioClip overButton;
    public AudioClip idleRobotSwing;

    void Awake()
    {
        if (gAud == null)
        {
            gAud = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}
