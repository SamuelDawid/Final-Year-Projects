using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

[CreateAssetMenu(fileName = "NewButtonHandler")]
public class ButtonHandler : InputHandler
{
    public  InputHelpers.Button button = InputHelpers.Button.None;
    public delegate void StateChange(XRController controller);
    public event StateChange OnButtonDown;
    public event StateChange OnbuttonUp;
    private bool previousPress = false;
    public override void HandleState(XRController controller)
    {
       if(controller.inputDevice.IsPressed(button, out bool pressed, controller.axisToPressThreshold))
        {
            if(previousPress != pressed)
            {
                previousPress = pressed;

                if(pressed)
                {
                    OnButtonDown?.Invoke(controller);
                }else
                {
                    OnbuttonUp?.Invoke(controller);
                }
            }
        }
    }



}
