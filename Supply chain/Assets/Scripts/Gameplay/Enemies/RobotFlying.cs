using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFlying : AIController
{
    void Start()
    {
        SetParameters(new int[] { 304, 305, 306});
        InflictDamage = 10;
    }

    void Update()
    {
        EnemyStates();
    }
}
