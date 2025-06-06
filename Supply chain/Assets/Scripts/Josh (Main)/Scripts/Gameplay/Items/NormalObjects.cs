using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalObjects : CentralPickupStructure
{
    [Tooltip("Can it be picked up by the Player?")] public bool pickUpByPlayer;
    [Tooltip("Can it be picked up by a robot?")] public bool pickUpByRobot;
    [HideInInspector] public bool isPickedUp;
    public GameObject destination;

    void Start()
    {
        SetParameters();
    }

    public override void ExecuteAction(Transform pickup)
    {
        base.ExecuteAction(pickup);
        pickUpByRobot = false;
    }
}