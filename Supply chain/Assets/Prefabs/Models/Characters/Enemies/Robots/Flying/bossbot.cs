using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bossbot : AIController
{
    //Vector3 rand;
    //Vector3 startingPosition;
    float patrolRange = 4f;     // How far you want the enemy to patrol
    float movingTime = 0f;      // Activates when the enemy gets to a point within the patrol range
    int stayFor;                // How long the enemy should stay at its designated point before it moves to another position

    //RaycastHit obj;
    //NavMeshAgent NMA;
    new bossState status;
    //private Animator animState;
    int animnum = 1;
    int idle = 11;
    float find = -10;
    [SerializeField]
    Component healthbar;

    //int health2 = 100;
    //int minhealth = 0;
    //int maxhealth = 100;

    int playtrigger = 0;
    float gothit = 0.0f;

    void Start()
    {
        animState = GetComponent<Animator>();
        startingPosition = transform.position;
        NMA = GetComponent<NavMeshAgent>();
        RandomLocation();

        // Getting References
        NMA = GetComponent<NavMeshAgent>();
        animState = GetComponent<Animator>();

        // Setting AI's HP
        currentHPAmount = maxHPAmount;
        (enemyIdentity = GetComponentInChildren<EnemyIdentification>()).CanvasParameters(enemyName, enemyTypeSprite);
        enemyIdentity.UpdateHP(currentHPAmount, maxHPAmount);

        stayFor = Random.Range(5, 11);
        InflictDamage = Random.Range(2, 7);
    }
    enum bossState
    {
        Patrol,
        PlayerSighted,
        Dead
    }
    void Update()
    {
        if (status != bossState.Dead)
        {
            //raycast serach for player
            status = bossState.Patrol;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 16.0f);
            foreach (Collider col in hitColliders)
            {
                if (col.transform.tag == "Player" && Vector3.Distance(col.transform.position, transform.position + transform.forward * 16) < 17)
                {
                    status = bossState.PlayerSighted;
                }
            }
            if (Vector3.Distance(transform.position, GlobalScript.playerRef.transform.position) < 30 && find + 3 > Time.time) status = bossState.PlayerSighted;
        }
        
        // ENEMY STATE ACTIONS
        switch(status)
        {
            case bossState.Patrol:
                if (NMA.velocity == Vector3.zero)
                    animState.SetInteger("boss", idle);
                else
                    animState.SetInteger("boss", 21);
                StartCoroutine(MoveAround());
                playtrigger = 0;
                break;
            case bossState.PlayerSighted:
                playtrigger = playtrigger + 1;
                if (Physics.Raycast(GlobalScript.playerRef.transform.position, -transform.forward * 14, out obj, 3))
                {
                    //Debug.Log(obj.transform.name);
                    if (obj.transform.name == transform.name)
                    {
                        animState.SetInteger("boss", animnum);
                    }
                    else
                    {
                        if (transform.position != GlobalScript.playerRef.transform.position)
                        {
                            if (animnum <= 3)
                            {
                                NMA.destination = GlobalScript.playerRef.transform.position;
                                animState.SetInteger("boss", 22);
                            }
                            else
                                animState.SetInteger("boss", animnum);
                        }
                    }
                }
                else
                {
                    if (transform.position != GlobalScript.playerRef.transform.position)
                    {
                        if (animnum <= 3)
                        {
                            NMA.destination = GlobalScript.playerRef.transform.position;
                            animState.SetInteger("boss", 22);
                        }
                        else
                            animState.SetInteger("boss", animnum);
                    }
                }
                break;
            case bossState.Dead:
                animState.SetInteger("boss", 666);
                enemyIdentity.gameObject.SetActive(false);
                gameObject.GetComponent<Collider>().enabled = false;
                NMA.speed = 0;
                //updatehealth();
                break;
        }
        if (playtrigger == 1)
            animState.SetInteger("boss", 31);
    }

    void RandomLocation()
    {
        rand = new Vector3(UnityEngine.Random.Range(startingPosition.x - patrolRange, startingPosition.x + patrolRange), transform.position.y, UnityEngine.Random.Range(startingPosition.z - patrolRange, startingPosition.z + patrolRange));
    }
    IEnumerator MoveAround()
    {
        Vector3 distance = startingPosition - transform.position;

        if (Vector3.Distance(transform.position, rand) > 0.4f)
        {
            NMA.destination = rand;
            movingTime += 0;
            yield return null;
        }
        else
        {
            if (movingTime > stayFor)
            {
                movingTime = 0;
                stayFor = UnityEngine.Random.Range(5, 11);
                RandomLocation();
            }
            movingTime += Time.deltaTime;
        }
    }

    void attackadd()
    {
        animnum = animnum + 1;
        if (animnum > 6)
            animnum = 1;
    }
    void idleadd()
    {
        idle = idle + 1;
        if (idle > 12)
            idle = 11;
    }

    /*public void addhealth(int amount)
    {
        if (status == bossState.Patrol) amount = amount * 2;
        if (status == bossState.Dead) amount = 0;
        if (Time.time > gothit + 0.5f && amount != 0)
        {
            health2 += amount;
            if (health2 < minhealth) health2 = minhealth;
            if (health2 > maxhealth) health2 = maxhealth;
            updatehealth();
            gothit = Time.time;
            transform.position = transform.position + GlobalScript.playerRef.transform.forward * 2;
        }
    }*/

    public override float hpModification(float modifier)
    {
        if (status == bossState.Dead) return 0;

        if (status == bossState.Patrol) modifier = modifier * 2;

        if (Time.time > gothit + 0.5f && modifier != 0)
        {
            currentHPAmount += modifier;
            if (currentHPAmount < 0) currentHPAmount = 0;
            if (currentHPAmount > maxHPAmount) currentHPAmount = maxHPAmount;
            //updatehealth();
            enemyIdentity.UpdateHP(currentHPAmount, maxHPAmount);
            gothit = Time.time;
            transform.position = transform.position + GlobalScript.playerRef.transform.forward * 2;
        }

        if (currentHPAmount <= 0)
        {
            status = bossState.Dead;
            return 20;
        }

        return 0;
    }

    public void FinishGame()
    {
        GameplayEventSystem.ges.PlayerWins();
    }
}
