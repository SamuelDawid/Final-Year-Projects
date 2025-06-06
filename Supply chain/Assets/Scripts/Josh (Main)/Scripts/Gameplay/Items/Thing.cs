using UnityEngine;
using UnityEngine.Events;

public class Thing : MonoBehaviour
{
    [SerializeField] int openUI;
    [SerializeField] UnityEvent action;

    public void Effect(GameObject go)
    {
        action.Invoke();
    }

    // LIST OF ACTIONS

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == GlobalScript.playerRef.gameObject)
        {
            GameplayEventSystem.ges.PlayerWins();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GlobalScript.playerRef.gameObject)
        {
         
        }
    }
}