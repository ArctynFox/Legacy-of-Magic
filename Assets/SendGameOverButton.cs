using UnityEngine;

public class SendGameOverButton : MonoBehaviour
{
    private void Awake()
    {
        Parameters.singleton.gameOverFirstButton = gameObject;
        Parameters.singleton.gameOverScreen = transform.parent.gameObject;
        transform.parent.gameObject.SetActive(false);
    }
}
