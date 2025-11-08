using UnityEngine;

public class SendGameOverMenu : MonoBehaviour
{
    private void Awake()
    {
        Parameters.singleton.gameOverScreen = gameObject;
    }
}
