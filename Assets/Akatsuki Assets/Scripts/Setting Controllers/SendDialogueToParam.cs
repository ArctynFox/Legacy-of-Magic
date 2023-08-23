using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDialogueToParam : MonoBehaviour
{
    Parameters param;
    void Awake()
    {
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
        param.bossDialoguePre = transform.parent.gameObject;
    }
}
