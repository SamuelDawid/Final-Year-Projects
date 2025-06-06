using UnityEngine;

public class ScaleWithPivots : MonoBehaviour
{
    public Transform startObj;
    public Transform endObj;
    private Vector3 InitialScale;
    public Renderer render, thisrender;
    private void Start()
    {
        InitialScale = transform.localScale;
    }
    private void Update()
    {
        UpdateTransformScale();
    }
    private void LateUpdate()
    {
        thisrender.material = render.material;
    }

    void UpdateTransformScale()
    {
        float distance = Vector3.Distance(startObj.position, endObj.position); // Change Scale
        transform.localScale = new Vector3(InitialScale.x, distance * 1.7f, InitialScale.z); // transform.localScale = new Vector3(InitialScale.x, distance/2f, InitialScale.z); // if its culinder

        Vector3 nPoint = (startObj.position + endObj.position) / 2f; // Changing position
        transform.position = nPoint;

        Vector3 rotationDirection = (endObj.position - startObj.position); // Changing rotation
        transform.up = rotationDirection;
        //endObj.up = rotationDirection;
    }
}
