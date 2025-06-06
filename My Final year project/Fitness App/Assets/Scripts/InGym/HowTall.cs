using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowTall : MonoBehaviour
{
    public GameObject canvas;

   public void LessThen()
    {
        EventSystem.ES.PlayerSpawnPosition();
        Destroy(canvas);
        GlobalStaticScript.firstTimePauseMenu = true;
    }
    public void Above()
    {
        GlobalStaticScript.firstTimePauseMenu = true;
        Destroy(canvas);
    }
}
