using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendGameOverButton : MonoBehaviour
{
    GameObject paramObject;
    Parameters param;
    private void Awake()
    {
        paramObject = GameObject.Find("gameParameters");
        param = paramObject.GetComponent<Parameters>();
        param.gameOverFirstButton = gameObject;
        param.gameOverScreen = transform.parent.gameObject;
        transform.parent.gameObject.SetActive(false);
    }
}
