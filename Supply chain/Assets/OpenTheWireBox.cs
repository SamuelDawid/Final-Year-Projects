using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheWireBox : MonoBehaviour
{
    public Animator anim;
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player") && QuestManager.instance.quest[2].isReached())
        {
            anim.SetTrigger("OpenWireBox");
        }
    }
}
