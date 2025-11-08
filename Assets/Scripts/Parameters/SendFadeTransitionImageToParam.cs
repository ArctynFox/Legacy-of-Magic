using UnityEngine;
using UnityEngine.UI;

public class SendFadeTransitionImageToParam : MonoBehaviour
{
    void Start()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 1);
        Parameters.singleton.fadeTransition = GetComponent<Image>();
    }
}
