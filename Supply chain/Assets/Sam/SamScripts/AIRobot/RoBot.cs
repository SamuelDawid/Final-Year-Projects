using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoBot : MonoBehaviour
{
    public enum States { Idle, Run, Die, }
    public States state;
    Animator anim;
    Transform movePoint;
    public Transform[] wayPoints;
    public int pointIndex;
    public float speed;
    private GameObject player;
    private void Start()
    {
        anim = GetComponent<Animator>();
        pointIndex = 0;
        movePoint = wayPoints[pointIndex];
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
      
        if(Vector3.Distance(transform.position, player.transform.position) <= 10f)
        {
            state = States.Run;
        }
        if(Vector3.Distance(transform.position, wayPoints[1].position) < 3f)
        {
            state = States.Die;
        }
        switch (state)
        {
            case States.Idle:
                {
                    anim.SetBool("Running", false);
                    anim.SetBool("Destroyed", false);
                    break;
                }

            case States.Run:
                {
                    pointIndex = 1;
                    anim.SetBool("Running", true);
                    if (transform.position != wayPoints[pointIndex].position)
                    {
                        Vector3 pos = Vector3.MoveTowards(transform.position, wayPoints[pointIndex].position, speed * Time.deltaTime);
                        transform.LookAt(wayPoints[1].transform.position);
                        GetComponent<Rigidbody>().MovePosition(pos);
                    }

                    break;
                }
            case States.Die:

                {
                    anim.SetBool("Running", false);
                    anim.SetBool("Destroyed", true);
                    RobotEventSystem.instance.active = true;
                    break;
                }

        }

    }
   
}
