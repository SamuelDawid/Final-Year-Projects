using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stillbot : AIController//, IDamage, IInteractWith
{
    RaycastHit obj;
    private Animator animState;
    enemyStatus state;
    Transform player;

    int playtrigger = 0;
    int playlostint = 0;

    [SerializeField] Sprite robotImg;

    void Start()
    {
        animState = GetComponent<Animator>();
        player = FindObjectOfType<NormalPlayerMovement>().transform;

        InflictDamage = 7;

        // Setting AI's HP
        currentHPAmount = maxHPAmount;
        (enemyIdentity = GetComponentInChildren<EnemyIdentification>()).CanvasParameters("Stationary Bot", robotImg);
        enemyIdentity.UpdateHP(currentHPAmount, maxHPAmount);

        SetParameters(new int[] { 300, 301, 302, 303, 304, 305, 306 });

        // Begin Coroutines
        StartCoroutine(ExecuteColliderChecking());
    }

    enum enemyStatus
    {
        Patrol,
        PlayerSighted,
        Dead
    }

    void Update()
    {
        if (state != enemyStatus.Dead)
        {
            state = enemyStatus.Patrol;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 16.0f);
            foreach (Collider col in hitColliders)
            {
                if (col.transform.gameObject == GlobalScript.playerRef.gameObject && Vector3.Distance(col.transform.position, transform.position - transform.forward * 16) < 17)
                {
                    state = enemyStatus.PlayerSighted;
                }
            }

            switch (state)
            {
                case enemyStatus.Patrol:
                    animState.SetInteger("station", 1);
                    if (playlostint == 1)
                        animState.SetInteger("station", 4);
                    playtrigger = 0;
                    playlostint = playlostint + 1;
                    break;
                case enemyStatus.PlayerSighted:
                    playlostint = 0;
                    playtrigger = playtrigger + 1;
                    animState.SetInteger("station", 3);
                    if (playtrigger == 1)
                        animState.SetInteger("station", 2);
                    if (Vector3.Distance(transform.position, player.position) < 14)
                        animState.SetInteger("station", 12);
                    if (Physics.Raycast(player.position, transform.forward * 40, out obj, 3))
                    {
                        if (obj.transform.name == transform.name)
                        {
                            animState.SetInteger("station", 11);
                        }
                    }
                    break;

                case enemyStatus.Dead:
                    animState.SetInteger("station", 666);
                    Debug.Log(3);
                    break;
            }
        }
        else {
            animState.SetInteger("station", 666);
            Debug.Log(3);
        }
    }

    public override float hpModification(float modifier)
    {

        if (state == enemyStatus.Dead) return 0;

        currentHPAmount += modifier;

        if (currentHPAmount <= 0)
        {
            currentHPAmount = 0;
            state = enemyStatus.Dead;
            enemyIdentity.UpdateHP(currentHPAmount, maxHPAmount);
            return 20; // EXP Amount returned
        }

        enemyIdentity.UpdateHP(currentHPAmount, maxHPAmount);
        return 0;
    }

    //public void EnableHit() { hitDetection.GetComponent<Collider>().enabled = true; }
    //public void DisableHit() { hitDetection.GetComponent<Collider>().enabled = false; }
    /*
    public void Effect(GameObject go)
    {
        Debug.Log(1);
        if (go == GlobalScript.playerRef.gameObject)
        {
            Debug.Log(2);
            float damageDealt = inflictDamage - ((GlobalScript.playerDefense + GlobalScript.playerRef.currentHeldWeapon.GetComponent<Weapon>().Defense) / 2);
            GlobalScript.playerRef.hpModification(-damageDealt);
            //GlobalScript.playerRef.GetComponent<Rigidbody>().AddForce(transform.forward * 4, ForceMode.VelocityChange);
            GameplayEventSystem.ges.PlayerAttacked(transform);
            DisableHit();
        }
    }

    public float hpModification(float modifier)
    {
        Debug.Log(2);

        if (status == enemyStatus.Dead) return 0;

        if (modifier < 0) animState.SetInteger("Animstate", 4);
        currentHPAmount += modifier;

        if (currentHPAmount <= 0)
        {
            currentHPAmount = 0;
            status = enemyStatus.Dead;
            enemyIdentity.UpdateHP(currentHPAmount, maxHPAmount);
            return 20; // EXP Amount returned
        }

        enemyIdentity.UpdateHP(currentHPAmount, maxHPAmount);
        return 0;
    }

    // Upon being defeated...
    public void DeactivationState()
    {
        GlobalScript.playerEXP += 10;

        // Spawn A random number of random crafting materials 
        int howMany = Random.Range(2, 5);

        // Spawn the crafting material (Use object pooling)
        for (int a = 0; a < howMany; a++)
        {
            // Spawn the crafting material at index 'whichOne'
            int whichOne = Random.Range(0, droppableCraftableItems.Length);

            GameObject craftingMaterial = ObjectPoolCM.opCMInst.AccquireMaterial(droppableCraftableItems[whichOne]);
            craftingMaterial.transform.position = transform.position;
        }

        gameObject.SetActive(false);
        StartCoroutine(Respawn());
    }*/
}
