using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SprinklesButton : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    bool canvasOn;

    public GameObject[] sprinklers;
    [SerializeField]
    private Material green, red, off;
    public GameObject feedback;
    public static SprinklesButton instance;
    public bool sprinklersON;
    private GameObject[] boxColliders;
    private bool justOnce;
    [SerializeField] TextMeshProUGUI indicator;
    private void Awake()
    {
        sprinklersON = false;
    }

    private void Start()
    {
        instance = this;
        boxColliders = GameObject.FindGameObjectsWithTag("Fire");
        justOnce = false;
    }

    void Update()
    {
        SetLights();
    }

    void SetLights()
    {
        if (canvas.activeSelf && !canvasOn)
        {
            if (!sprinklersON) feedback.GetComponent<Renderer>().material = red;
            else if (sprinklersON) feedback.GetComponent<Renderer>().material = green;
            canvasOn = true;
        }
        else if (!canvas.activeSelf && canvasOn)
        {
            feedback.GetComponent<Renderer>().material = off;
            canvasOn = false;
        }
    }

    public void TurnOn()
    {
        if(!justOnce)
        {
            QuestManager.instance.quest[0].AddAmmount();
            justOnce = true;
        }
        feedback.GetComponent<Renderer>().material = green;
        sprinklersON = true;
        GetBoxColliders();
        for (int i =0; i < sprinklers.Length; i++)
        {
            sprinklers[i].SetActive(true);
        }

        indicator.text = "CURRENTLY: ON";
    }

    public void TurnOff()
    {
        feedback.GetComponent<Renderer>().material = red;
        sprinklersON = false;
        for (int i = 0; i < sprinklers.Length; i++)
        {
            sprinklers[i].SetActive(false);
        }

        indicator.text = "CURRENTLY: OFF";
    }

    void GetBoxColliders()
    {
        for (int i = 0; i < boxColliders.Length; i++)
        {
            boxColliders[i].GetComponentInParent<BoxCollider>().enabled = true;
        }
    }
}
