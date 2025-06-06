using System.Collections;
using UnityEngine;
using UnityEngine.AI;

abstract public class AIController : MonoBehaviour, IDamage, IInteractWith
{
    [Header("-----> Canvas Identidication")]
    public string enemyName;
    public Sprite enemyTypeSprite;
    [Space]
    [HideInInspector] public EnemyIdentification enemyIdentity;

    [Header("-----> Hold Position")]
    public GameObject holdPosition;
    [Space]

    [Header("-----> Enemy HP")]
    public float maxHPAmount;
    [HideInInspector] public float currentHPAmount;
    [Space]

    int minWaitingTime = 5;
    int maxWaitingTime = 12;
    bool moving;

    float patrolRange = 20f;
    [HideInInspector] public int waitTime;

    [Header("-----> Hit Detection Ranges")]
    float playerDistDetection = 10;
    float triggerAttack = 5;
    float pickupDistDetection = 1;

    [Header("-----> Hit Detection")]
    [SerializeField] GameObject hitDetection;
    [Space]

    [Header("-----> Damage Infliction")]
    [HideInInspector] public float InflictDamage;

    int[] droppableCraftableItems;

    [HideInInspector] public bool justSighted;
    float colliderCheckTime = 1;

    [HideInInspector] public Animator animState;
    [HideInInspector] public NavMeshAgent NMA;
    Transform player;
    [HideInInspector] public Vector3 startingPosition;
    [HideInInspector] public Vector3 rand;

    [HideInInspector] public RaycastHit obj;
    [HideInInspector] public NormalObjects pickupObject;

    // Enemy State
    public enum enemyStatus
    {
        Patrol,
        PickedUpObject,
        PlayerSighted,
        Dead
    }
    [HideInInspector] public enemyStatus status;

    void OnDisable()
    {
        StopCoroutine(ExecuteColliderChecking());
        StopCoroutine(MoveAround());
    }

    // Checking the colliders
    void ColliderChecking()
    {
        float sqrLen = Vector3.SqrMagnitude(GlobalScript.playerRef.transform.position - transform.position);

        // Detection: Player
        if ((sqrLen / 100) < playerDistDetection && status != enemyStatus.PlayerSighted)
            status = enemyStatus.PlayerSighted;
        else
        {
            if (!pickupObject && status != enemyStatus.PlayerSighted)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupDistDetection);

                foreach (Collider col in hitColliders)
                {
                    // Detection: Pickup Object
                    if (col.GetComponent<NormalObjects>())
                    {
                        NormalObjects no = col?.GetComponent<NormalObjects>();

                        if (no.isPickedUp == false && no.pickUpByRobot)
                        {
                            no.isPickedUp = true;
                            pickupObject = col.GetComponent<NormalObjects>();
                            status = enemyStatus.PickedUpObject;
                        }
                    }
                }
            }
        }
    }

    // (IInteractWith) Inflicts damage/Adds Health to player
    public void Effect(GameObject go)
    {
        if (go == GlobalScript.playerRef.gameObject)
        {
            float damageDealt = InflictDamage - ((GlobalScript.playerDefense + GlobalScript.playerRef.currentHeldWeapon.GetComponent<Weapon>().Defense) / 2);
            GlobalScript.playerRef.hpModification(-damageDealt);
            //GlobalScript.playerRef.GetComponent<Rigidbody>().AddForce(transform.forward * 4, ForceMode.VelocityChange);
            GameplayEventSystem.ges.PlayerAttacked(transform);
            DisableHit();
        }
    }

    // Enemy State Actions
    public void EnemyStates()
    {
        switch (status)
        {
            case enemyStatus.Patrol:
                justSighted = false;
                if (NMA.velocity != Vector3.zero && animState.GetInteger("Animstate") != 6) animState.SetInteger("Animstate", 6);
                else if (NMA.velocity == Vector3.zero && animState.GetInteger("Animstate") != 1) animState.SetInteger("Animstate", 1);
                break;

            case enemyStatus.PickedUpObject:
                moving = false;
                justSighted = false;
                if (Vector3.SqrMagnitude(transform.position - pickupObject.transform.position) > 12)
                    NMA.destination = pickupObject.transform.position;
                else
                {
                    PickupObjectStatus(true);
                    NMA.destination = pickupObject.destination.transform.position;
                    if (Vector3.SqrMagnitude(transform.position - pickupObject.destination.transform.position) < 0.5f)
                    {
                        pickupObject.ExecuteAction(pickupObject.transform);
                        PickupObjectStatus(false);
                        status = enemyStatus.Patrol;
                    }
                }
                break;

            case enemyStatus.PlayerSighted:
                moving = false;
                if (pickupObject) PickupObjectStatus(false);
                if (!justSighted)
                {
                    animState.SetInteger("Animstate", 2);
                    justSighted = true;
                }
                else
                {
                    if (Physics.Raycast(player.position, -transform.forward, out obj, triggerAttack))
                    {
                        if (obj.transform == transform) animState.SetInteger("Animstate", 3);
                        else GoToPlayer();
                    }
                    else GoToPlayer();
                }
                float sqrLen = Vector3.SqrMagnitude(GlobalScript.playerRef.transform.position - transform.position);
                if ((sqrLen / 100) > playerDistDetection) status = enemyStatus.Patrol;
                break;

            case enemyStatus.Dead:
                moving = false;
                justSighted = false;
                animState.SetInteger("Animstate", 5);
                StopMoving();
                break;
        }
    }

    // Checks colliders every 'x' seconds (Performant-Wise)
    public IEnumerator ExecuteColliderChecking()
    {
        while (true)
        {
            if (status != enemyStatus.Dead) ColliderChecking();
            yield return new WaitForSeconds(colliderCheckTime);
        }
    }

    // Enemy goes to player
    void GoToPlayer()
    {
        if (transform.position != player.position)
        {
            NMA.destination = player.position;
            animState.SetInteger("Animstate", 6);
        }
    }

    // (IDamage) Inflicts damage/Adds Health to enemy
    
    virtual public float hpModification(float modifier)
    {
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

        transform.position += GlobalScript.playerRef.transform.forward * 6;
        enemyIdentity.UpdateHP(currentHPAmount, maxHPAmount);
        return 0;
    }

    // Moving around periodically
    public IEnumerator MoveAround()
    {
        while (true)
        {
            if (status == enemyStatus.Patrol)
            {
                if (!moving)
                {
                    RandomLocation();
                    NMA.destination = rand;
                    waitTime = UnityEngine.Random.Range(minWaitingTime, maxWaitingTime);
                    moving = true;
                }

                else if (Vector3.SqrMagnitude(rand - transform.position) < 10)
                {
                    moving = false;
                    yield return new WaitForSeconds(waitTime);
                }
            }
            yield return null;
        }
    }

    // Pickup object: Robot holding an item or not?
    void PickupObjectStatus(bool holding)
    {
        if (holding)
        {
            pickupObject.GrabDrop(true);
            pickupObject.SetParent(holdPosition.transform);
        }
        else if (!holding)
        {
            pickupObject.GrabDrop(false);
            pickupObject.DetachFromParent();
            pickupObject.isPickedUp = false;
            pickupObject = null;
        }
    }

    // Generates a random location within an area
    void RandomLocation()
    { rand = new Vector3(Random.Range(startingPosition.x - patrolRange, startingPosition.x + patrolRange), transform.position.y, Random.Range(startingPosition.z - patrolRange, startingPosition.z + patrolRange)); }

    // Sets enemies' parameters
    public void SetParameters(int[] materials)
    {
        // Enemy starts off patrolling
        status = enemyStatus.Patrol;

        // Getting References
        NMA = GetComponent<NavMeshAgent>();
        animState = GetComponent<Animator>();

        // Setting AI's HP
        currentHPAmount = maxHPAmount;
        (enemyIdentity = GetComponentInChildren<EnemyIdentification>()).CanvasParameters(enemyName, enemyTypeSprite);
        enemyIdentity.UpdateHP(currentHPAmount, maxHPAmount);

        // Sets starting position
        startingPosition = transform.position;
        RandomLocation();
        waitTime = Random.Range(minWaitingTime, maxWaitingTime);

        // Player Reference
        player = GlobalScript.playerRef.transform;

        // Stores crafting material IDs in which enemies drop
        droppableCraftableItems = materials;
    }

    #region (Animation Events)

    public void StopMoving() { NMA.isStopped = true; }
    public void ContinueMoving() { NMA.isStopped = false; }
    public void EnableHit() { hitDetection.GetComponent<Collider>().enabled = true; }
    public void DisableHit() { hitDetection.GetComponent<Collider>().enabled = false; }

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

        ObjectPoolingEnemies.opEnemyInst.ReturnEnemy(gameObject);
        ObjectPoolingEnemies.opEnemyInst.AccquireEnemy();
    }
    #endregion

    public void Respawn()
    {
        currentHPAmount = maxHPAmount;
        status = enemyStatus.Patrol;
        gameObject.SetActive(true);
    }
}