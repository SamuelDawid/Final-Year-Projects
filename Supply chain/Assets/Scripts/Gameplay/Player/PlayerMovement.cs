using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
abstract public class PlayerMovement : MonoBehaviour, IDamage
{
    [HideInInspector] public HUD playerHUD;

    [HideInInspector] public Rigidbody rb;
    [Header("(Children of Player)")]
    public Camera playerCamera;
    public Transform orientation;
    public Transform weaponPosition;
    [HideInInspector] public Vector3 movement;

    [HideInInspector] public List<Weapon> spawnedWeapons = new List<Weapon>();
    [HideInInspector] public GameObject currentHeldWeapon;

    //  Player Movement
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float airMultiplier = 0.4f;
    [HideInInspector] public float movementMultiplier = 10f;

    [Header("Movement Speeds")]
    public float walkSpeed;
    public float sprintSpeed;
    [HideInInspector] public float acceleration = 4f;
    [HideInInspector] public float sprintFOV = 100f;

    [Header("Jumping")]
    public float jumpForce;

    // Sprint Activation
    [HideInInspector] public float sprintActivation = 0f;
    [HideInInspector] public float sprintLimit = 0.1f;
    [HideInInspector] public int sprintPressed;

    // Drag
    [HideInInspector] public float groundDrag = 4f;
    [HideInInspector] public float airDrag = 1.5f;

    // Ground Checkking
    [HideInInspector] public int groundLayer = 7;
    [HideInInspector] public bool isOnGround = true;

    [HideInInspector] public enum moveStatus
    {
        Walking,
        Sprinting
    }
    [HideInInspector] public moveStatus moving;

    // Awake conditions
    public void AwakeConditions()
    {
        rb = GetComponent<Rigidbody>();
        GlobalScript.playerRef = this;
        GlobalAudio.gAud.BGM.PlayOneShot(GlobalAudio.gAud.bgmGameplay);
    }

    // Start conditions
    public void StartConditions()
    {
        playerHUD = FindObjectOfType<HUD>();
        playerHUD.gameObject.SetActive(true);
        SwitchWeapons(GlobalScript.weapons[playerHUD.selectedWeaponSlot]);
    }

    void OnEnable()
    {
        GameplayEventSystem.ges.onDropWeapon += DroppingWeapon;
        GameplayEventSystem.ges.onPlayerAttacked += KnockBack;
        GameplayEventSystem.ges.onSwitchHUDWeapons += SwitchWeapons;
        GameplayEventSystem.ges.onPickupItem += WeaponInHand;
    }

    void OnDisable()
    {
        GameplayEventSystem.ges.onDropWeapon -= DroppingWeapon;
        GameplayEventSystem.ges.onPlayerAttacked -= KnockBack;
        GameplayEventSystem.ges.onSwitchHUDWeapons -= SwitchWeapons;
        GameplayEventSystem.ges.onPickupItem -= WeaponInHand;
    }

    // General Movement + Other Stuff
    public void GeneralActions()
    {
        Movement();
        DragControl();
        CraftingMaterialPickup();

        if (Input.GetMouseButton(0)) Attack();
        if (Input.GetKey(GlobalScript.jumpControl) && isOnGround) Jump();
    }

    void FixedUpdate()
    { MovementStates(); }

    void OnCollisionEnter(Collision col)
    {
        // Player's on the ground? 'IsOnGround' set to true
        if (col.collider.gameObject.layer == groundLayer) isOnGround = true;

        // Collides with a crafting material? Collects it through Object-Pooling
        if (col.gameObject.GetComponent<CraftingMaterial>() != null)
        {
            playerHUD.inventory.ItemModification(col.gameObject.GetComponent<CraftingMaterial>().info.GivenID, 0);
            ObjectPoolCM.opCMInst.ReturnMaterial(col.gameObject);
        }
    }

    #region Player Movement
    public virtual void Movement()
    {
        // Activates sprinting when desired
        SprintActivation();

        switch (moving)
        {
            // Player walking
            case moveStatus.Walking:
                moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 90f, 8f * Time.deltaTime);
                break;

            // Player sprinting
            case moveStatus.Sprinting:
                moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, 8f * Time.deltaTime);

                if (movement == Vector3.zero)
                {
                    sprintPressed = 0;
                    moving = moveStatus.Walking;
                }
                break;
        }
    }

    // Double Tap 'Walk' key to sprint
    void SprintActivation()
    {
        if (moving == moveStatus.Walking)
        {
            switch (sprintPressed)
            {
                case 0:
                    if (movement != Vector3.zero)
                    {
                        sprintPressed = 1;
                        if (sprintActivation != 0) sprintActivation = 0;
                    }
                    break;

                case 1:
                    sprintActivation += Time.deltaTime;
                    if (movement == Vector3.zero)
                    {
                        if (sprintActivation < sprintLimit) sprintPressed = 2;
                        else if (sprintActivation > sprintLimit) sprintPressed = 0;

                        sprintActivation = 0;
                    }
                    break;

                case 2:
                    if (movement != Vector3.zero && sprintActivation < (sprintLimit)) moving = moveStatus.Sprinting;
                    else if (sprintActivation >= sprintLimit) sprintPressed = 0;
                    break;
            }
        }
    }

    // Player jumping
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
    }

    // Player's drag on ground and air (sliding)
    void DragControl()
    {
        if (isOnGround) rb.drag = groundDrag;
        else rb.drag = airDrag;
    }

    public void MovementStates()
    {
        if (isOnGround) rb.AddForce(movement.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        else if (!isOnGround) rb.AddForce(movement.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
    }
    #endregion

    #region Weapons
    void WeaponInHand(bool weaponVisible)
    { if (currentHeldWeapon.activeSelf != !weaponVisible) currentHeldWeapon.SetActive(!weaponVisible); }

    void DroppingWeapon(int num)
    {
        GameplayEventSystem.ges.SwitchHUDWeapons(GlobalScript.weapons[num]);
    }

    // Switch HUD Weapons
    void SwitchWeapons(WeaponData wd)
    {
        if (wd == null) return;
        
        if (currentHeldWeapon != null) currentHeldWeapon.SetActive(false);

        // Instantiates weapon if not in 'spawnedWeapons' list
        if (!spawnedWeapons.Contains(wd.weaponRef))
        {
            currentHeldWeapon = wd.SpawnWeapon().gameObject;
            spawnedWeapons.Add(currentHeldWeapon.GetComponent<Weapon>());
            currentHeldWeapon.transform.SetParent(weaponPosition, false);
        }
        else currentHeldWeapon = spawnedWeapons[spawnedWeapons.IndexOf(wd.weaponRef)].gameObject;

        currentHeldWeapon.SetActive(true);
    }
    #endregion

    // Near a crafting material
    void CraftingMaterialPickup()
    {
        Collider[] hits = Physics.OverlapBox(gameObject.transform.position, transform.localScale * 4, Quaternion.identity);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.GetComponent<CraftingMaterial>() != null && hit.gameObject.GetComponent<CraftingMaterial>().isPickupable)
            {
                hit.transform.position = Vector3.MoveTowards(hit.transform.position, transform.position, 3);
            }
        }
    }

    #region --- Player Attacking ---
    // General player attacking
    void Attack()
    {
        if (currentHeldWeapon.GetComponent<Animator>())
            currentHeldWeapon.GetComponent<Animator>()?.SetInteger("WeaponState", 0);
    }

    void KnockBack(Transform tm)
    {
        rb.AddForce(tm.forward * 7 + tm.up * 12, ForceMode.Impulse);
    }

    // If the player doesn't continue the attack combo
    void BackToIdle()
    {
        currentHeldWeapon.GetComponent<Animator>()?.SetInteger("WeaponState", -1);
    }

    // HP Modification
    public float hpModification(float Modifier)
    {
        playerHUD.ModifyHP(Modifier);
        return 0;
    }
    #endregion
}