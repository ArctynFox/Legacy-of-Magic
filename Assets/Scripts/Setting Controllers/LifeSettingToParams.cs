using UnityEngine;
using UnityEngine.UI;

public class LifeSettingToParams : MonoBehaviour
{
    public GameObject paramObject;
    public Slider lifeSlider;

    // Update is called once per frame
    void Update()
    {
        Parameters.singleton.lifeSetting = (int)lifeSlider.value;
    }
}
