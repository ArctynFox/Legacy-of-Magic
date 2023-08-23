using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendContButton : MonoBehaviour
{
    GameObject paramObject;
    Parameters param;
    private void Awake()
    {
        paramObject = GameObject.Find("gameParameters");
        param = paramObject.GetComponent<Parameters>();
        param.continueFirstButton = gameObject;
        param.continueScreen = transform.parent.gameObject;
        transform.parent.gameObject.SetActive(false);
    }
}
