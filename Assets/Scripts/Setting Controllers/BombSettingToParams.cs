using UnityEngine;
using UnityEngine.UI;

public class BombSettingToParams : MonoBehaviour
{
    public GameObject paramObject;
    public Slider bombSlider;

    // Update is called once per frame
    void Update()
    {
        Parameters.singleton.bombSetting = (int)bombSlider.value;
    }
}