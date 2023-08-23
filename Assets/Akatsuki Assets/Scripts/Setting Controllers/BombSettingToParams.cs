using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombSettingToParams : MonoBehaviour
{
    public GameObject paramObject;
    public Slider bombSlider;
    Parameters param;

    private void Start()
    {
        param = paramObject.GetComponent<Parameters>();
    }

    // Update is called once per frame
    void Update()
    {
        param.bombSetting = (int)bombSlider.value;
    }
}