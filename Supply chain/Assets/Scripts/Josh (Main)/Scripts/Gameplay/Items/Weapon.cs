using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IInteractWith
{
    Animator animator;

    bool interaction;

    [HideInInspector] public int ID;
    float Attack;
    [HideInInspector] public float Defense;

    void OnEnable()
    {
        if (gameObject.GetComponent<Animator>()) animator = GetComponent<Animator>();
    }

    public void SetParameters(int id, float atk, float def)
    {
        ID = id;
        Attack = atk; 
        Defense = def;
    }

    public void Effect(GameObject go)
    {
        if (interaction) return;
        if (go.GetComponent<AIController>() || go.GetComponent<stillbot>())
        {
            // Attack Factors: (Weapon) Attack Value; (Player) AttackValue 
            float damage = Attack + (GlobalScript.playerAttack);
            if (go.GetComponent<AIController>())go.GetComponent<AIController>().hpModification(-damage);
            else go.GetComponent<stillbot>().hpModification(-damage);
            //go.transform.position += Vector3.Lerp(go.transform.localPosition, go.transform.forward * 4 , 5 * Time.deltaTime);
            interaction = true;
        }
    }

    // (Animation Event)
    void ResetInteraction()
    {
        interaction = false;
    }

    // If the player doesn't continue the attack combo
    public void BackToIdle()
    {
        animator.SetInteger("WeaponState", -1);
    }
}
