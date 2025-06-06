using UnityEditor.UI;
using UnityEngine;
using TMPro;
using System.Collections;
public class CheckIfObjectActive : MonoBehaviour
{
    public string name;
    public TextMeshProUGUI message;
    public Canvas messageCanvas;
    public GameObject[] tankObjects;
    
     void Start()
    {
        message.gameObject.SetActive(false);
        messageCanvas.gameObject.SetActive(false);
        for (int i = 0; i < tankObjects.Length; i++)
        {
            tankObjects[i].SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameObject.Find(name) != null)
        {
            ActiveTankObjects(true);
            messageCanvas.gameObject.SetActive(false);
            message.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player") && GameObject.Find(name) == null)
        {
            ActiveTankObjects(false);
            message.gameObject.SetActive(true);
            message.text = "To fix the machine you need" + "\n" + name;
            messageCanvas.gameObject.SetActive(true);

        }
    }
    void OnTriggerExit()
    {
         messageCanvas.gameObject.SetActive(false);
    }
    public void ActiveTankObjects(bool activeTanks )
    {
        if(activeTanks)
        {
            for (int i = 0; i < tankObjects.Length; i++)
            {
                tankObjects[i].SetActive(true);
            }
        }
        
    }
   
}
