using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBack : MonoBehaviour
{
    public Material matchMaterial;
    public Material misMatchMaterial;

    private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void ChangeMaterialWithMatch(bool IsCorrectMatch)
    {
        if(IsCorrectMatch)
        {
            renderer.material = matchMaterial;
        }else
        {
            renderer.material = misMatchMaterial;
        }
    }
    
   
}
