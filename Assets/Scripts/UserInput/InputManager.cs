using UnityEngine;

//Input System‚ÌŠÇ—ƒVƒ“ƒOƒ‹ƒgƒ“

public class InputManager : MonoBehaviour
{
    public static InputActions inputActions;

    void Awake()
    {
        if (inputActions == null)
        {
            inputActions = new InputActions();
        }
    }
}
