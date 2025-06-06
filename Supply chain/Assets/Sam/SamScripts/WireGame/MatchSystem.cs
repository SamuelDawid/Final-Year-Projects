using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class MatchSystem : MonoBehaviour
{
    public List<Material> colorMat;
    private List<MatchCables> matchCables;
    private int targetMatchCount;
    private int currentMatchCount = 0;

    private void Start()
    {
        matchCables = transform.GetComponentsInChildren<MatchCables>().ToList();
        
        targetMatchCount = matchCables.Count;
        SetCablesColors();
        RandomizePairPlacment();
    }
    void SetCablesColors()
    {
        Shuffle(colorMat);

        for(int i =0; i < matchCables.Count; i++)
        {
            matchCables[i].SetMaterialToPairs(colorMat[i]);
        }
    }
    void RandomizePairPlacment()
    {
        List<Vector3> movablePairPositions = new List<Vector3>();
        for(int i = 0; i<matchCables.Count; i++)
        {
            movablePairPositions.Add(matchCables[i].GetPairPosition());
        }
        Shuffle(movablePairPositions);

        for (int i = 0; i < matchCables.Count; i++)
        {
            matchCables[i].SetPairPosition(movablePairPositions[i]);
        }
    }
    public void NewMatchRecord(bool MatchConnected)
    {
        if(MatchConnected)
        {
            currentMatchCount++;
        }else
        {
            currentMatchCount--;
        }
        if(currentMatchCount == targetMatchCount)
        {
            Debug.Log("Done");
            QuestManager.instance.quest[3].AddAmmount();
        }
    }

    public static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
