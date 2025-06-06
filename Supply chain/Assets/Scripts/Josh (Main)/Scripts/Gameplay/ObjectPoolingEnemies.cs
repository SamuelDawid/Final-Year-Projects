using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingEnemies : MonoBehaviour
{
    [SerializeField] List<GameObject> robots = new List<GameObject>();

    static public ObjectPoolingEnemies opEnemyInst { get; private set; }

    int index = 3;

    void Start()
    {
        opEnemyInst = this;
    }

    public GameObject AccquireEnemy()
    {
        if (robots.Count == 0) return null;

        GameObject enemy;

        enemy = robots[index];
        enemy.GetComponent<AIController>().Respawn();
        index++;
        if (index == robots.Count) index = 0;

        return enemy;
    }

    public void ReturnEnemy(GameObject gm)
    {
        gm.SetActive(false);
    }
}