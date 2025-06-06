using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            EventSystem.ES.AddScore(GlobalStaticScript.ScoreValue);
            EventSystem.ES.UpdateScoreText();
            Destroy(other.gameObject);
        }
    }
}
