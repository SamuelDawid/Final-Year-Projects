using UnityEngine;

public class SaberHitDetector : MonoBehaviour
{
    public LayerMask layer;
    private Vector3 previosPos;
    public GameObject VFX;
    private int SFXindex;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1, layer))
        {
            if (Vector3.Angle(transform.position - previosPos, hit.transform.up) > 130)
            {
                Destroy(hit.transform.gameObject);
                EventSystem.ES.PlaySFX(SFXindex);
                EventSystem.ES.AddScore(GlobalStaticScript.ScoreValue);
                GlobalStaticScript.comboCount++;
            }
        }
        previosPos = transform.position;
        if (GlobalStaticScript.comboCount >= 10)
        {
            VFX.SetActive(true);
            GlobalStaticScript.ScoreValue = GlobalStaticScript.ScoreValue * GlobalStaticScript.Modifier;
            if (GlobalStaticScript.gps == GlobalStaticScript.GameplayStatus.Track2 || GlobalStaticScript.gps == GlobalStaticScript.GameplayStatus.Track3) SFXindex = 3;
            else if (GlobalStaticScript.gps == GlobalStaticScript.GameplayStatus.Track4) SFXindex = 1;
        }
        else if (GlobalStaticScript.comboCount < 10)
        {
            SFXindex = 0;
            GlobalStaticScript.ScoreValue = GlobalStaticScript.ScoreValueRef;
            VFX.SetActive(false);
        }
    }
}


