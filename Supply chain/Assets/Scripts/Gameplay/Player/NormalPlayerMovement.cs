using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlayerMovement : PlayerMovement
{
    void Awake()
    {
        AwakeConditions();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Start()
    {StartConditions();}

    void Update()
    {
        GeneralActions();
        if (Input.GetKey(KeyCode.F))
        {
            // Spawn the crafting material (Use object pooling)
            for (int a = 0; a < 20; a++)
            {
                // Spawn the crafting material at index 'whichOne'
                int whichOne = Random.Range(300, 306);

                GameObject craftingMaterial = ObjectPoolCM.opCMInst.AccquireMaterial(whichOne);
                craftingMaterial.transform.position = transform.position;
            }
        }
    }

    void FixedUpdate()
    {MovementStates();}

    public override void Movement()
    { 
        movement = orientation.forward * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");
        base.Movement();
    }
}