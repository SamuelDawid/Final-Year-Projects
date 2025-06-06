using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public List<ButtonHandler> allButtonHandlers = new List<ButtonHandler>();

    private XRController controller = null;

    private void Awake()
    {
        controller = GetComponent<XRController>();
    }
    private void Update()
    {
        HandleButtonEvents();
    }
    void HandleButtonEvents()
    {
        foreach (ButtonHandler handler in allButtonHandlers)
            handler.HandleState(controller);
    }
    void HandleAxis2DEvents()
    {
        //Futer Development
    }
    void HandleAxisEvents()
    {
        // Fill up later if have time 
    }
}
