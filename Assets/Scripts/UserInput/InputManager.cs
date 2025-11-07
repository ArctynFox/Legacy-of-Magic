using UnityEngine;

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
