using UnityEngine;

//Input Systemの管理シングルトン

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
