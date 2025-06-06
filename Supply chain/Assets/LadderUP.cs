using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderUP : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    /*private void Update()
    {
       // var distance = Vector3.Distance(transform.position, player.transform.position);
       // Debug.Log("" + distance);
        if (Vector3.Distance(transform.position, player.transform.position) < 2.1f)
        {
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
        }else if (Vector3.Distance(transform.position, player.transform.position) > 2.5f)
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GlobalScript.playerRef.gameObject)
        {
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == GlobalScript.playerRef.gameObject)
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
        }
    }
}
   