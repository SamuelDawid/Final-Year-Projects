using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockColor
{
    Green,
    Red
}
public class Blocks : MonoBehaviour
{
    public BlockColor color;
    public static string RIGHTHAND = "RightHand";
    public static string LEFTHAND = "LeftHand";
    public LayerMask layer;
    void OnEnable()
    {
        EventSystem.ES.onHitWrongBlock += OnHitWrongBlock;
        EventSystem.ES.onAddScore += OnAddScore;
    }
    public void OnAddScore(int hitScore)
    {
        GlobalStaticScript.playerScore += hitScore;
    }
    public void OnHitWrongBlock(int missBlock)
    {
        GlobalStaticScript.playerScore -= missBlock;
    }
    public void Hit()
    {
        Destroy(gameObject);
        EventSystem.ES.UpdateScoreText();
        EventSystem.ES.AddScore(GlobalStaticScript.ScoreValue);
        EventSystem.ES.PlaySFX(4);
    }
    void MissHit()
    {
        Destroy(gameObject);
        EventSystem.ES.HitWrongBlock(GlobalStaticScript.missHitValue);
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, 0.1f, transform.forward,out hit,0.3f, layer))
        {
            if(hit.collider.CompareTag(RIGHTHAND))
            {
                if (color == BlockColor.Green)
                {
                    Hit();

                }
                else
                {
                    MissHit();

                }
            }
            else if (hit.collider.CompareTag(LEFTHAND))
            {
                if (color == BlockColor.Red)
                {
                    Hit();

                }
                else
                {
                    MissHit();

                }
            }
        }
        transform.position += Time.deltaTime * transform.forward * 2;
    }


}
