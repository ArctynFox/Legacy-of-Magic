using UnityEngine;
using UnityEngine.EventSystems;

public class AdvanceStageOnSelect : MonoBehaviour
{
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnSelect()
    {
        Parameters.singleton.NextStage();
    }
}
