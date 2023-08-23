using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendContMenu : MonoBehaviour
{
    GameObject paramObject;
    Parameters param;
    private void Awake()
    {
        paramObject = GameObject.Find("gameParameters");
        param = paramObject.GetComponent<Parameters>();
        param.continueScreen = gameObject;
    }
}
