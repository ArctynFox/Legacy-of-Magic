using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendFadeToParam : MonoBehaviour
{
    GameObject paramObject;
    Parameters param;
    private void Start()
    {
        paramObject = GameObject.Find("gameParameters");
        param = paramObject.GetComponent<Parameters>();
        param.fadeTransition = GetComponent<Image>();
    }
}
