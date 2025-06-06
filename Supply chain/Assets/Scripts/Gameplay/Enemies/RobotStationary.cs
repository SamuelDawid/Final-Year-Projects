using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStationary : AIController
{
    void Start()
    {
        SetParameters(new int[] { 307, 308});
        InflictDamage = 10;
    }

    void Update()
    {
        EnemyStates();
    }
}
