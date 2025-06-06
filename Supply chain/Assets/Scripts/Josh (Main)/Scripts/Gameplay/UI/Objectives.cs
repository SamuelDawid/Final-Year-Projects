using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    [SerializeField] string newObjective;

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject == GlobalScript.playerRef.gameObject)
        {
            GameplayEventSystem.ges.UpdateObjective(newObjective);
        }*/

        if (collision.gameObject == GlobalScript.playerRef.gameObject)
        {
            GameplayEventSystem.ges.PlayerWins();
        }
    }
}
