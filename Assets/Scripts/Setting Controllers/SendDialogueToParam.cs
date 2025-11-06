using UnityEngine;

public class SendDialogueToParam : MonoBehaviour
{
    void Awake()
    {
        Parameters.singleton.bossDialoguePre = transform.parent.gameObject;
    }
}
