using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendGameOverMenu : MonoBehaviour
{
    GameObject paramObject;
    Parameters param;
    private void Awake()
    {
        paramObject = GameObject.Find("gameParameters");
        param = paramObject.GetComponent<Parameters>();
        param.gameOverScreen = gameObject;
    }
}
