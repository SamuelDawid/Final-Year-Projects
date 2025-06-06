using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotWheeled : AIController
{
    void Start()
    {
        SetParameters(new int[] { 300, 301, 302, 303, 304, 305, 306 });

        // Begin Coroutines
        StartCoroutine(ExecuteColliderChecking());
        StartCoroutine(MoveAround());

        InflictDamage = 10;
    }

    void Update()
    {
        EnemyStates();
    }

    void ExtraFunctionalities()
    {
        
    }

    void ReactivationState()
    {

    }
}
