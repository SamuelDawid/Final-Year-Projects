using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUp
{
    bool CanRotate();
    float LaunchSpeed();
    GameObject ThisObject();
    // One for picking up
    // One for dropping
}
