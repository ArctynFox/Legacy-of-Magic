using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessParamImmediate : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 1);
        Parameters.singleton.fadeTransition = GetComponent<Image>();
    }
}
