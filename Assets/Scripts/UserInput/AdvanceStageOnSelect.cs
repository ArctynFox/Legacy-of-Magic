using UnityEngine;
using UnityEngine.InputSystem;

public class AdvanceStageOnSelect : MonoBehaviour
{
    InputAction select;

    void OnEnable()
    {
        select = InputManager.inputActions.UI.Select;
        select.Enable();
        select.performed += OnSelect;
    }

    void OnDisable()
    {
        if (select != null)
        {
            select.Disable();
            select.performed -= OnSelect;
        }
    }

    void OnSelect(InputAction.CallbackContext callbackContext)
    {
        Parameters.singleton.NextStage();
    }
}
