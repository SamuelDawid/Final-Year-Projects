using UnityEngine;

public class MatchCables : MonoBehaviour
{
    public FeedBack feedback;
    public MouseMove movablePair;
    public Renderer fixedPairRenderer;
    public MatchSystem matchSystemManager;
    private bool matched;

    public Vector3 GetPairPosition()
    {
        return movablePair.GetPosition();
    }
   
    public void SetPairPosition(Vector3 NewPairPosition)
    {
        movablePair.SetInitialPosition(NewPairPosition);
    }
    public void SetMaterialToPairs(Material PairMaterial)
    {
        movablePair.GetComponent<Renderer>().material = PairMaterial;
        fixedPairRenderer.material = PairMaterial;
    }

    public void PairObjectInteraction(bool isEnter, MouseMove movable)
    {
        if(isEnter && !matched)
        {
            matched = (movable == movablePair);
            if(matched)
            {
                matchSystemManager.NewMatchRecord(matched);
                feedback.ChangeMaterialWithMatch(matched);
            }
        }else if(!isEnter && matched)
        {
            matched = !(movable == movablePair);
            if(!matched)
            {
                matchSystemManager.NewMatchRecord(matched);
                feedback.ChangeMaterialWithMatch(matched);
            }
        }
    }
}
