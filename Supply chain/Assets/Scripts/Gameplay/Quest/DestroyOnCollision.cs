using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public string weponTag;
    [SerializeField]
    private GameObject fire;
    public int questNumber;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(weponTag))
        {
           Debug.Log("Hit");
            QuestManager.instance.quest[questNumber].AddAmmount();
            Destroy(gameObject);
            Destroy(fire);
        }
    }
    
}
   
