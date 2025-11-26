using UnityEngine;
using UnityEngine.EventSystems;

//UIボタンを押すと、次のステージに進む

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
