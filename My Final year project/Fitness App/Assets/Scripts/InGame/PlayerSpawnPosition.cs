using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPosition : MonoBehaviour
{
    void OnEnable() => EventSystem.ES.onPlayerSpawnPosition += ChangePlatformPosition;
    void OnDisable() => EventSystem.ES.onPlayerSpawnPosition -= ChangePlatformPosition;
    public void ChangePlatformPosition()
    {
        transform.position = new Vector3(0f, -0.25f, 0f);
    }
}
