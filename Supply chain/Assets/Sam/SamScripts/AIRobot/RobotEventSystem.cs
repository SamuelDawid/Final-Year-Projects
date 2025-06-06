using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEventSystem : MonoBehaviour
{
    public Animator Spillage, Barrels;
    public bool active;
    public static RobotEventSystem instance;
    private bool eventRobot;
    [SerializeField]
    private GameObject[] particle;
    [SerializeField]
    private GameObject[] spillageObj;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        eventRobot = false;
        active = false;
        for(int i =0; i < particle.Length; i++)
        {
            particle[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < spillageObj.Length; i++)
        {
            spillageObj[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if(!eventRobot)
            {
                StartCoroutine(Animation());
                eventRobot = true;
            }
        }
    }

    IEnumerator Animation()
    {
        Barrels.SetBool("Falling", true);
        yield return new WaitForSeconds(2);
        for (int i = 0; i < spillageObj.Length; i++)
        {
            spillageObj[i].gameObject.SetActive(true);
        }
        Spillage.SetTrigger("Filling");
        for (int i = 0; i < particle.Length; i++)
        {
            particle[i].gameObject.SetActive(true);
        }
    }

    
}
