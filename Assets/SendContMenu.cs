using UnityEngine;

public class SendContMenu : MonoBehaviour
{
    private void Awake()
    {
        Parameters.singleton.continueScreen = gameObject;
    }
}
