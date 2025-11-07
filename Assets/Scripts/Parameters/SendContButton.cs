using UnityEngine;

public class SendContButton : MonoBehaviour
{
    private void Awake()
    {
        Parameters.singleton.continueFirstButton = gameObject;
        Parameters.singleton.continueScreen = transform.parent.gameObject;
        transform.parent.gameObject.SetActive(false);
    }
}
