using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRobotPickup
{
    void GrabDrop(bool isKinematic);
    void SetParent(Transform pos);
    void DetachFromParent();
    void ExecuteAction(Transform pickup);
}