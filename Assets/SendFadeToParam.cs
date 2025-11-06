using UnityEngine;
using UnityEngine.UI;

public class SendFadeToParam : MonoBehaviour
{
    private void Start()
    {
        Parameters.singleton.fadeTransition = GetComponent<Image>();
    }
}
